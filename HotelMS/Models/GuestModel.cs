using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelMS.Models
{
    public class GuestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Login is not valid")]
        [Display(Name = "Login")]
        public string GuestMail { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(40, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(40, MinimumLength = 1)]
        public string Name { get; set; }
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(2, MinimumLength = 2)]
        [Display(Name = "Passport Serial Number")]
        public string PassportSerialNumber { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [Range(0, 999999, ErrorMessage = "Passport Number must be 6 digits long")]
        [Display(Name = "Passport Number")]
        public int PassportNumber { get; set; }

        public int PhoneNumberTypeCode { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Phone number must be written in format +38(0__)-___-__-__")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual PhoneNumbersTypes PhoneNumbersTypes { get; set; }
    }
}