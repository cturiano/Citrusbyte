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
    ///     Class for actions related to <see cref="T:Citrusbyte.Models.Device" />
    /// </summary>
    public class WebApiDevicesController : WebApiControllerBase
    {
        #region Public Methods

        /// <summary>
        ///     Deletes the <see cref="Device" /> with the given id
        /// </summary>
        /// <param name="id">The id of the <see cref="Device" /> to delete</param>
        /// <returns><see cref="IHttpActionResult" /> the <see cref="Device" /> that was deleted or Not Found</returns>
        /// <remarks>DELETE: api/WebApiDevices/DeleteDevice/5</remarks>
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> DeleteDevice(int id)
        {
            var device = await DB.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            DB.Devices.Remove(device);
            await DB.SaveChangesAsync();

            return Ok(device);
        }

        /// <summary>
        ///     Gets the <see cref="Device" /> with the given id
        /// </summary>
        /// <param name="id">The id of the device to get</param>
        /// <returns><see cref="IHttpActionResult" /> containing the <see cref="Device" /> or not found</returns>
        /// <remarks>GET: api/WebApiDevices/GetDevice/5</remarks>
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(int id)
        {
            var device = await DB.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        /// <summary>
        ///     Gets the <see cref="Device" /> with the given GUID
        /// </summary>
        /// <param name="serial">The id of the device to get</param>
        /// <returns><see cref="IHttpActionResult" /> containing the <see cref="Device" /> or not found</returns>
        /// <remarks>GET: api/WebApiDevices?serial=92b2aec4-edb4-44d5-ad2b-8ad47134545b</remarks>
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(Guid serial)
        {
            var device = await DB.Devices.FirstOrDefaultAsync(d => d.Serial == serial);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        /// <summary>
        ///     Gets all the devices
        /// </summary>
        /// <returns>A <see cref="IQueryable{Device}" /></returns>
        /// <remarks>GET: api/WebApiDevices/GetDevices</remarks>
        public IQueryable<Device> GetDevices() => DB.Devices;

        /// <summary>
        ///     Adds the given<see cref="Device" /> to the database
        /// </summary>
        /// <param name="device">The <see cref="Device" /> to add</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the index of the created device.</returns>
        /// <remarks>POST: api/WebApiDevices/PostDevice</remarks>
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            device.RegistrationDate = DateTime.UtcNow.Ticks;

            DB.Devices.Add(device);
            await DB.SaveChangesAsync();

            return CreatedAtRoute("API Default", new {id = device.Id}, device);
        }
        
        /// <summary>
        ///     Adds the given <see cref="IEnumerable{Device}"/> to the database
        /// </summary>
        /// <param name="devices">The <see cref="IEnumerable{Device}"/> to add</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the index of the created device.</returns>
        /// <remarks>POST: api/WebApiDevices/PostDevices</remarks>
        [ResponseType(typeof(int))]
        [ActionName("Batch")]
        public async Task<IHttpActionResult> PostDevices([FromBody] IEnumerable<Device> devices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var count = 0;
            var ds = devices.ToList();
            foreach (var device in ds)
            {
                device.RegistrationDate = DateTime.UtcNow.Ticks;

                DB.Devices.Add(device);
                count += await DB.SaveChangesAsync();
            }

            return Created("API Default", count);
        }

        /// <summary>
        ///     Updates a <see cref="Device" /> with the given id to the properties of the given devices
        /// </summary>
        /// <param name="id">The id of the <see cref="Device" /> to update.</param>
        /// <param name="device">The properties the new <see cref="Device" /> should contain.</param>
        /// <returns>An <see cref="IHttpActionResult" /> containing the status of the update.</returns>
        /// <remarks>PUT: api/WebApiDevices/PutDevice/5</remarks>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevice(int id, Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.Id)
            {
                return BadRequest();
            }

            DB.Entry(device).State = EntityState.Modified;

            try
            {
                await DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion
        
        #region Private Methods

        private bool DeviceExists(int id)
        {
            return DB.Devices.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}