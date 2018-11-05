using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Citrusbyte.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        #region Properties

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        #endregion
    }

    public class ExternalLoginListViewModel
    {
        #region Properties

        public string ReturnUrl { get; set; }

        #endregion
    }

    public class SendCodeViewModel
    {
        #region Properties

        public ICollection<SelectListItem> Providers { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string SelectedProvider { get; set; }

        #endregion
    }

    public class VerifyCodeViewModel
    {
        #region Properties

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        public string Provider { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        #endregion
    }

    public class ForgotViewModel
    {
        #region Properties

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        #endregion
    }

    public class LoginViewModel
    {
        #region Properties

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        #endregion
    }

    public class RegisterViewModel
    {
        #region Properties

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        #endregion
    }

    public class ResetPasswordViewModel
    {
        #region Properties

        public string Code { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        #endregion
    }

    public class ForgotPasswordViewModel
    {
        #region Properties

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        #endregion
    }
}