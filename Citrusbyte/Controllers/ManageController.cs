using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Citrusbyte.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for actions related to managing users
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        #region Fields

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        ///     Default constructor
        /// </summary>
        public ManageController()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Parameterized constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The <see cref="ApplicationSignInManager" /> for this instance
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        /// <summary>
        ///     The <see cref="ApplicationUserManager" /> for this instance
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the view for adding a phone numer
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: /Manage/AddPhoneNumber</remarks>
        public ActionResult AddPhoneNumber() => View();

        /// <summary>
        ///     Adds the given <see cref="AddPhoneNumberViewModel" /> phone number to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/AddPhoneNumber</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                              {
                                  Destination = model.Number,
                                  Body = "Your security code is: " + code
                              };

                await UserManager.SmsService.SendAsync(message);
            }

            return RedirectToAction("VerifyPhoneNumber", new {PhoneNumber = model.Number});
        }

        /// <summary>
        ///     Gets the view to change passwords
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: /Manage/ChangePassword</remarks>
        public ActionResult ChangePassword() => View();

        /// <summary>
        ///     Changes the password
        /// </summary>
        /// <param name="model">The <see cref="ChangePasswordViewModel" /> to change the password</param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/ChangePassword</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }

            AddErrors(result);
            return View(model);
        }

        /// <summary>
        ///     Disables two factor authentication
        /// </summary>
        /// <returns></returns>
        /// <remarks>POST: /Manage/DisableTwoFactorAuthentication</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, false, false);
            }

            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        ///     Enables two factor authentication
        /// </summary>
        /// <returns></returns>
        /// <remarks>POST: /Manage/EnableTwoFactorAuthentication</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, false, false);
            }

            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        ///     Gets the index view
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>GET: /Manage/Index</remarks>
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed." :
                                    message == ManageMessageId.SetPasswordSuccess ? "Your password has been set." :
                                    message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set." :
                                    message == ManageMessageId.Error ? "An error has occurred." :
                                    message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added." :
                                    message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed." : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
                        {
                            HasPassword = HasPassword(),
                            PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                            TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                            Logins = await UserManager.GetLoginsAsync(userId),
                            BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                        };

            return View(model);
        }

        /// <summary>
        ///     Gets the Link login
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/LinkLogin</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider) => new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());

        /// <summary>
        ///     Gets the link login callback
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: /Manage/LinkLoginCallback</remarks>
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
        }

        /// <summary>
        ///     Gets the view to manage logins
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>GET: /Manage/ManageLogins</remarks>
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed." :
                                    message == ManageMessageId.Error ? "An error has occurred." : "";

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
                        {
                            CurrentLogins = userLogins,
                            OtherLogins = otherLogins
                        });
        }

        /// <summary>
        ///     Removes the login with the given credentials
        /// </summary>
        /// <param name="loginProvider"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/RemoveLogin</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return RedirectToAction("ManageLogins", new {Message = message});
        }

        /// <summary>
        ///     Removes the phone number for this user
        /// </summary>
        /// <returns></returns>
        /// <remarks>POST: /Manage/RemovePhoneNumber</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new {Message = ManageMessageId.Error});
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, false, false);
            }

            return RedirectToAction("Index", new {Message = ManageMessageId.RemovePhoneSuccess});
        }

        /// <summary>
        ///     Gets the set password view
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: /Manage/SetPassword</remarks>
        public ActionResult SetPassword() => View();

        /// <summary>
        ///     Sets the password to the given <see cref="SetPasswordViewModel" />
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/SetPassword</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                    }

                    return RedirectToAction("Index", new {Message = ManageMessageId.SetPasswordSuccess});
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        ///     Gets the verify phone number view
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <remarks>GET: /Manage/VerifyPhoneNumber</remarks>
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel {PhoneNumber = phoneNumber});
        }

        /// <summary>
        ///     Verifies the given phone number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>POST: /Manage/VerifyPhoneNumber</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                return RedirectToAction("Index", new {Message = ManageMessageId.AddPhoneSuccess});
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        /// <summary>
        ///     disposes of the <see cref="T:Citrusbyte.ApplicationUserManager" /> object
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return user?.PasswordHash != null;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return user?.PhoneNumber != null;
        }

        /// <summary>
        /// The message ids for the management UI
        /// </summary>
        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}