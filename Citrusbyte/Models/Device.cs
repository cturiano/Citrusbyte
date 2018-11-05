using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Citrusbyte.Models
{
    /// <summary>
    ///     Represents a physical device
    /// </summary>
    [Table("devices")]
    public class Device
    {
        #region Constructors

        /// <summary>
        ///     default constructor
        /// </summary>
        public Device() => RegistrationDate = DateTime.UtcNow.Ticks;

        /// <summary>
        ///     parameterized constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serialNumber"></param>
        /// <param name="firmwareVersion"></param>
        public Device(int id, Guid serialNumber, string firmwareVersion)
        {
            Id = id;
            Serial = serialNumber;
            Firmware_Version = firmwareVersion;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The version of the firmware for this <see cref="Device" />
        /// </summary>
        [Display(Name = "Firmware Version")]
        [Required]
        public string Firmware_Version { get; set; }

        /// <summary>
        ///     The database id of this <see cref="Device" />
        /// </summary>
        [Key]
        [Display(Name = "Device Id")]
        public int Id { get; set; }

        /// <summary>
        ///     The date and time this <see cref="Device" /> was registered
        /// </summary>
        [Display(Name = "UTC Registration Date")]
        public long RegistrationDate { get; set; }

        /// <summary>
        ///     The serial number of this <see cref="Device" />
        /// </summary>
        [Display(Name = "Serial #")]
        [DataMember(Name = "serial")]
        [Required]
        [Index(IsUnique = true)]
        public Guid Serial { get; set; }

        #endregion
    }
}