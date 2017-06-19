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
    
    public partial class HotelStaff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelStaff()
        {
            this.HotelsRoomRegistration = new HashSet<HotelsRoomRegistration>();
            this.StaffsPassports = new HashSet<StaffsPassports>();
            this.StaffsPhoneNumbers = new HashSet<StaffsPhoneNumbers>();
        }
    
        public string StaffMail { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int PositionCode { get; set; }
        public decimal Salary { get; set; }
        public string Schedule { get; set; }
    
        public virtual EmployeesPositions EmployeesPositions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelsRoomRegistration> HotelsRoomRegistration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffsPassports> StaffsPassports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffsPhoneNumbers> StaffsPhoneNumbers { get; set; }
    }
}