﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Easycase.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EasyCaseDBEntities : DbContext
    {
        public EasyCaseDBEntities()
            : base("name=EasyCaseDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MartialStatu> MartialStatus { get; set; }
        public virtual DbSet<Purpose> Purposes { get; set; }
        public virtual DbSet<CorporatePosition> CorporatePositions { get; set; }
        public virtual DbSet<CorporateProfileInformation> CorporateProfileInformations { get; set; }
        public virtual DbSet<CasesNote> CasesNotes { get; set; }
        public virtual DbSet<Assigned> Assigneds { get; set; }
        public virtual DbSet<Mention> Mentions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskNote> TaskNotes { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<BusinessHour> BusinessHours { get; set; }
        public virtual DbSet<ManageHour> ManageHours { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<CompanyDetail> CompanyDetails { get; set; }
        public virtual DbSet<Support> Supports { get; set; }
        public virtual DbSet<CaseType> CaseTypes { get; set; }
        public virtual DbSet<CaseStatu> CaseStatus { get; set; }
        public virtual DbSet<CorporateJobDetail> CorporateJobDetails { get; set; }
        public virtual DbSet<ServiceFee> ServiceFees { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<ProfileInformation> ProfileInformations { get; set; }
        public virtual DbSet<EducationDetail> EducationDetails { get; set; }
        public virtual DbSet<OtherDetail> OtherDetails { get; set; }
        public virtual DbSet<LinkCas> LinkCases { get; set; }
    }
}
