using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Citrusbyte.Models;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for actions related to <see cref="T:Citrusbyte.Models.SensorReading" />
    /// </summary>
    public class SensorReadingsController : MvcControllerBase
    {
        #region Public Methods

        /// <summary>
        ///     Gets the view for the <see cref="SensorReading" /> table
        /// </summary>
        /// <param name="deviceId">The optional id of the device for which to fetch the readings.</param>
        /// <param name="start">The <see cref="DateTime" /> at which to begin filtering.</param>
        /// <param name="end">The <see cref="DateTime" /> at which to stop filtering.</param>
        /// <returns></returns>
        /// <remarks>GET: ReadingsTable</remarks>
        [AllowAnonymous]
        public async Task<ActionResult> BuildReadingsTable(int? deviceId = null, DateTime? start = null, DateTime? end = null) => PartialView("_ReadingsTable", await GetReadings(deviceId, start, end));

        /// <summary>
        ///     Gets the view to create a <see cref="SensorReading" />
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: SensorReadings/Create</remarks>
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create() => View();

        /// <summary>
        ///     Creates the <see cref="SensorReading" />
        /// </summary>
        /// <param name="sensorReading">The <see cref="SensorReading" /> to add to the database.</param>
        /// <returns></returns>
        /// <remarks>
        ///     POST: SensorReadings/Create
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create([Bind(Include = "ownerId,co,humidity,status,temp")]
                                               SensorReading sensorReading)
        {
            if (ModelState.IsValid)
            {
                sensorReading.ReadingTime = DateTime.UtcNow.Ticks;

                DB.SensorReadings.Add(sensorReading);
                await DB.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sensorReading);
        }

        /// <summary>
        ///     Gets the view to delete a <see cref="SensorReading" />
        /// </summary>
        /// <param name="id">The optional id of the <see cref="SensorReading" /> to delete.</param>
        /// <returns></returns>
        /// <remarks>GET: SensorReadings/Delete/5</remarks>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading == null)
            {
                return HttpNotFound();
            }

            return View(sensorReading);
        }

        /// <summary>
        ///     Deletes the <see cref="SensorReading" />
        /// </summary>
        /// <param name="id">The id of the <see cref="SensorReading" /> to delete.</param>
        /// <returns></returns>
        /// <remarks>POST: SensorReadings/Delete/5</remarks>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading != null)
            {
                DB.SensorReadings.Remove(sensorReading);
                await DB.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Gets the details view for the <see cref="SensorReading" />
        /// </summary>
        /// <param name="id">The optional id of the <see cref="SensorReading" /> for which to fetch the details.</param>
        /// <returns></returns>
        /// <remarks>GET: SensorReadings/Details/5</remarks>
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading == null)
            {
                return HttpNotFound();
            }

            return View(sensorReading);
        }

        /// <summary>
        ///     Gets the view for editing a <see cref="SensorReading" />
        /// </summary>
        /// <param name="id">The id of the <see cref="SensorReading" /> to edit.</param>
        /// <returns></returns>
        /// <remarks>GET: SensorReadings/Edit/5</remarks>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading == null)
            {
                return HttpNotFound();
            }

            return View(sensorReading);
        }

        /// <summary>
        ///     Updates the <see cref="SensorReading" />
        /// </summary>
        /// <param name="sensorReading">The <see cref="SensorReading" /> to update in the database</param>
        /// <returns></returns>
        /// <remarks>
        ///     POST: SensorReadings/Edit/5
        ///     To protect from overposting attacks, please enable the specific properties you want to bind to, for
        ///     more details see https://go.microsoft.com/fwlink/?LinkId=317598
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,ownerId,co,humidity,readingTime,status,temp")]
                                             SensorReading sensorReading)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(sensorReading).State = EntityState.Modified;
                await DB.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sensorReading);
        }

        /// <summary>
        ///     Gets the view for the <see cref="SensorReading" />
        /// </summary>
        /// <returns></returns>
        /// <remarks>GET: SensorReadings</remarks>
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index() => View();

        #endregion
        
        #region Private Methods

        private async Task<IEnumerable<SensorReading>> GetReadings(int? deviceId = null, DateTime? start = null, DateTime? end = null)
        {
            if (start == null)
            {
                start = DateTime.MinValue;
            }

            if (end == null)
            {
                end = DateTime.MaxValue;
            }

            if (start > end)
            {
                var tmp = start.Value;

                // make start the beginning of the end date
                start = end.Value.AddHours(-12).Date;

                // make end the end of the start date
                end = tmp.AddDays(1) - new TimeSpan(0, 0, 1);
            }

            if (deviceId == null)
            {
                return (await DB.SensorReadings.ToListAsync()).Where(r => r.ReadingTime > start?.Ticks && r.ReadingTime <= end?.Ticks).OrderBy(r => r.OwnerId).ThenBy(r => r.ReadingTime);
            }

            return (await DB.SensorReadings.ToListAsync()).Where(r => r.OwnerId == deviceId && r.ReadingTime > start?.Ticks && r.ReadingTime <= end?.Ticks).OrderBy(r => r.ReadingTime);
        }

        #endregion
    }
}