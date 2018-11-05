using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Citrusbyte.Models;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for actions related to <see cref="T:Citrusbyte.Models.Device" />
    /// </summary>
    public class DevicesController : MvcControllerBase
    {
        #region Public Methods

        /// <summary>
        ///     Gets the create <see cref="Device" /> view
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: Devices/Create</remarks>
        [AllowAnonymous]
        public ActionResult Create() => View();

        /// <summary>
        ///     Create the given <see cref="Device" /> in the database
        /// </summary>
        /// <param name="device">The <see cref="Device" /> to add to the database</param>
        /// <returns></returns>
        /// <remarks>
        ///     POST: Devices/Create
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see https://go.microsoft.com/fwlink/?LinkId=317598
        /// </remarks>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create([Bind(Include = "firmware_version,serial")]
                                               Device device)
        {
            if (ModelState.IsValid)
            {
                device.RegistrationDate = DateTime.UtcNow.Ticks;

                DB.Devices.Add(device);
                await DB.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(device);
        }

        /// <summary>
        ///     Gets the delete <see cref="Device" /> view
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: Devices/Delete/5</remarks>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = await DB.Devices.FindAsync(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        /// <summary>
        ///     Deletes the <see cref="Device" /> with the given id
        /// </summary>
        /// <param name="id">The id of the <see cref="Device" /> to delete</param>
        /// <returns></returns>
        /// <remarks>POST: Devices/Delete/5</remarks>
        [HttpPost]
        [ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var device = await DB.Devices.FindAsync(id);
            if (device != null)
            {
                DB.Devices.Remove(device);
                await DB.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Gets the details <see cref="Device" /> view
        /// </summary>
        /// <param name="id">The id of the <see cref="Device" /> for which to show details</param>
        /// <returns></returns>
        /// <remarks>GET: Devices/Details/5</remarks>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = await DB.Devices.FindAsync(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        /// <summary>
        ///     Gets the details <see cref="Device" /> view
        /// </summary>
        /// <param name="id">The <see cref="Guid" /> of the <see cref="Device" /> for which to show details</param>
        /// <returns></returns>
        /// <remarks>GET: Devices/DetailsGuid/92b2aec4-edb4-44d5-ad2b-8ad47134545b</remarks>
        public async Task<ActionResult> DetailsGuid(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = await DB.Devices.FirstOrDefaultAsync(d => d.Serial == id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        /// <summary>
        ///     Gets the edit <see cref="Device" /> view
        /// </summary>
        /// <param name="id">The id of the <see cref="Device" /> to edit</param>
        /// <returns></returns>
        /// <remarks>GET: Devices/Edit/5</remarks>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var device = await DB.Devices.FindAsync(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return View(device);
        }

        /// <summary>
        ///     Edits the given <see cref="Device" />
        /// </summary>
        /// <param name="device">The <see cref="Device" /> to edit</param>
        /// <returns></returns>
        /// <remarks>
        ///     POST: Devices/Edit/5
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see https://go.microsoft.com/fwlink/?LinkId=317598
        /// </remarks>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,firmware_version,serial")]
                                             Device device)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(device).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(device);
        }

        /// <summary>
        ///     Gets the main <see cref="Device" /> view
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// GET: Devices
        [AllowAnonymous]
        public async Task<ActionResult> Index() => View(await DB.Devices.ToListAsync());

        #endregion
    }
}