﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelDataBaseEntities : DbContext
    {
        public HotelDataBaseEntities()
            : base("name=HotelDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmployeesPositions> EmployeesPositions { get; set; }
        public virtual DbSet<GuestPassports> GuestPassports { get; set; }
        public virtual DbSet<GuestsPhoneNumbers> GuestsPhoneNumbers { get; set; }
        public virtual DbSet<HotelGuests> HotelGuests { get; set; }
        public virtual DbSet<HotelRooms> HotelRooms { get; set; }
        public virtual DbSet<HotelsRoomRegistration> HotelsRoomRegistration { get; set; }
        public virtual DbSet<HotelStaff> HotelStaff { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<PhoneNumbersTypes> PhoneNumbersTypes { get; set; }
        public virtual DbSet<RoomClasses> RoomClasses { get; set; }
        public virtual DbSet<StaffsPassports> StaffsPassports { get; set; }
        public virtual DbSet<StaffsPhoneNumbers> StaffsPhoneNumbers { get; set; }
    }
}
