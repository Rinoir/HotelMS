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
    
    public partial class PaymentMethods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentMethods()
        {
            this.HotelsRoomRegistration = new HashSet<HotelsRoomRegistration>();
        }
    
        public int PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelsRoomRegistration> HotelsRoomRegistration { get; set; }
    }
}