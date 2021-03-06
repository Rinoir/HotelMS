//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class GuestsPhoneNumbers
    {
        public int Id { get; set; }
        public string GuestMail { get; set; }
        public int PhoneNumberTypeCode { get; set; }
        [Display(Name = "Phone Number")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Phone number must be written in format +38(0__)-___-__-__")]
        public string PhoneNumber { get; set; }
    
        public virtual HotelGuests HotelGuests { get; set; }
        public virtual PhoneNumbersTypes PhoneNumbersTypes { get; set; }
    }
}
