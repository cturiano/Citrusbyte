using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Citrusbyte.Models;

namespace Citrusbyte.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Class for actions related to <see cref="SensorReading" />
    /// </summary>
    public class WebApiSensorReadingsController : WebApiControllerBase
    {
        #region Public Methods

        /// <summary>
        ///     Deletes the <see cref="SensorReading" /> with the given id
        /// </summary>
        /// <param name="id">The id of the <see cref="SensorReading" /> to delete</param>
        /// <returns><see cref="IHttpActionResult" /> the <see cref="SensorReading" /> that was deleted or Not Found</returns>
        /// <remarks>DELETE: api/WebApiSensorReadings/DeleteSensorReading/5</remarks>
        // DELETE: api/SensorReadings/5
        [ResponseType(typeof(SensorReading))]
        public async Task<IHttpActionResult> DeleteSensorReading(int id)
        {
            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading == null)
            {
                return NotFound();
            }

            DB.SensorReadings.Remove(sensorReading);
            await DB.SaveChangesAsync();

            return Ok(sensorReading);
        }

        /// <summary>
        ///     Gets the <see cref="SensorReading" /> with the given id
        /// </summary>
        /// <param name="id">The id of the SensorReading to get</param>
        /// <returns><see cref="IHttpActionResult" /> containing the <see cref="SensorReading" /> or not found</returns>
        /// <remarks>GET: api/WebApiSensorReadings/GetSensorReading/5</remarks>
        // GET: api/SensorReadings/5
        [ResponseType(typeof(SensorReading))]
        public async Task<IHttpActionResult> GetSensorReading(int id)
        {
            var sensorReading = await DB.SensorReadings.FindAsync(id);
            if (sensorReading == null)
            {
                return NotFound();
            }

            return Ok(sensorReading);
        }

        /// <summary>
        ///     Gets all the <see cref="SensorReading" />, perhaps for a device with the given id
        /// </summary>
        /// <param name="deviceId">The id of <see cref="Device" /> whose <see cref="SensorReading" /> to get.</param>
        /// <returns>A <see cref="IQueryable{SensorReading}" /></returns>
        /// <remarks>GET: api/WebApiSensorReadings/GetSensorReading</remarks>
        // GET: api/SensorReadings
        public IQueryable<SensorReading> GetSensorReadings(int? deviceId = null)
        {
            return !deviceId.HasValue ? DB.SensorReadings : DB.SensorReadings.Where(sr => sr.OwnerId == deviceId);
        }

        /// <summary>
        ///     Adds the given <see cref="SensorReading" /> to the database
        /// </summary>
        /// <param name="sensorReading">The <see cref="SensorReading" /> to add</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the index of the created SensorReading.</returns>
        /// <remarks>POST: api/WebApiSensorReadings/PostSensorReading</remarks>
        // POST: api/SensorReadings
        [ResponseType(typeof(SensorReading))]
        public async Task<IHttpActionResult> PostSensorReading(SensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sensorReading.ReadingTime = DateTime.UtcNow.Ticks;

            DB.SensorReadings.Add(sensorReading);
            await DB.SaveChangesAsync();

            return CreatedAtRoute("API Default", new {id = sensorReading.Id}, sensorReading);
        }
        
        /// <summary>
        ///     Adds the given <see cref="IEnumerable{SensorReading}" /> to the database
        /// </summary>
        /// <param name="sensorReadings">The <see cref="IEnumerable{SensorReading}" /> to add</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the count of items created in the database.</returns>
        /// <remarks>POST: api/WebApiSensorReadings/PostSensorReadings</remarks>
        // POST: api/SensorReadings
        [ResponseType(typeof(int))]
        [ActionName("Batch")]
        public async Task<IHttpActionResult> PostSensorReadings([FromBody]IEnumerable<SensorReading> sensorReadings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var count = 0;
            var readings = sensorReadings.ToList();
            foreach (var sr in readings)
            {
                sr.ReadingTime = DateTime.UtcNow.Ticks;

                DB.SensorReadings.Add(sr);
                count += await DB.SaveChangesAsync();
            }

            return Created("API Default", count);
        }

        /// <summary>
        ///     Updates a <see cref="SensorReading" /> with the given id to the properties of the given SensorReadings
        /// </summary>
        /// <param name="id">The id of the <see cref="SensorReading" /> to update.</param>
        /// <param name="sensorReading">The properties the new <see cref="SensorReading" /> should contain.</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the status of the update.</returns>
        /// <remarks>PUT: api/WebApiSensorReadings/PutSensorReading/5</remarks>
        // PUT: api/SensorReadings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSensorReading(int id, SensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorReading.Id)
            {
                return BadRequest();
            }

            DB.Entry(sensorReading).State = EntityState.Modified;

            try
            {
                await DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorReadingExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region Private Methods

        private bool SensorReadingExists(int id)
        {
            return DB.SensorReadings.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}