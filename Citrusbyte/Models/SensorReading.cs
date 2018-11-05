using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Citrusbyte.Models
{
    /// <summary>
    ///     A sensor reading for a device
    /// </summary>
    [Table("readings")]
    public class SensorReading
    {
        #region Constructors

        /// <summary>
        ///     Default constructor
        /// </summary>
        public SensorReading() => ReadingTime = DateTime.UtcNow.Ticks;

        /// <summary>
        ///     Parameterized constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temp"></param>
        /// <param name="humidity"></param>
        /// <param name="coPercent"></param>
        /// <param name="status"></param>
        /// <param name="ownerId"></param>
        public SensorReading(int id, double temp, double humidity, double coPercent, string status, int ownerId)
        {
            Id = id;
            Temp = temp;
            Humidity = humidity;
            CO = coPercent;
            Status = status;
            OwnerId = ownerId;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The carbon monoxide level of this <see cref="SensorReading" /> in parts per million
        /// </summary>
        [Display(Name = "CO (PPM)")]
        [Required]
        public double CO { get; set; }

        /// <summary>
        ///     The percent humidity of this <see cref="SensorReading" />
        /// </summary>
        [Display(Name = "Humidity (%)")]
        [Required]
        public double Humidity { get; set; }

        /// <summary>
        ///     The database id of this <see cref="SensorReading" />
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     The <see cref="Device" /> that generated this <see cref="SensorReading" />
        /// </summary>
        [ForeignKey("OwnerId")]
        [HiddenInput(DisplayValue = false)]
        internal Device Owner { get; set; }

        /// <summary>
        ///     The the id of the <see cref="Device" /> that generated this <see cref="SensorReading" />
        /// </summary>
        [Display(Name = "Device Id")]
        [Required]
        public int OwnerId { get; set; }

        /// <summary>
        ///     The time of this <see cref="SensorReading" />
        /// </summary>
        [Display(Name = "UTC Time")]
        public long ReadingTime { get; set; }

        /// <summary>
        ///     The status of this <see cref="SensorReading" />
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        ///     The temperature in degrees Celcius of this <see cref="SensorReading" />
        /// </summary>
        [Display(Name = "Temp (°C)")]
        [Required]
        public double Temp { get; set; }

        #endregion
    }
}