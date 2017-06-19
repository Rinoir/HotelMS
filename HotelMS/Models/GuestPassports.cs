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

    public partial class GuestPassports
    {
        public int Id { get; set; }
        public string GuestMail { get; set; }

        [Display(Name = "Passport Serial Number")]
        [StringLength(2, MinimumLength = 2)]
        public string PassportSerialNumber { get; set; }

        [Display(Name = "Passport Number")]
        public int PassportNumber { get; set; }
    
        public virtual HotelGuests HotelGuests { get; set; }
    }
}