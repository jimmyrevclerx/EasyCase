//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ProfileInformation
    {
        public long ID { get; set; }
        public long ProfileId { get; set; }
        public string RelationToPrimaryApplicant { get; set; }
        public string Address { get; set; }
        public string HearFromAboutus { get; set; }
        public string LikeToDoInCanada { get; set; }
        public string HaveYouAcceptedSchoolCollege { get; set; }
        public string VisitPrevious { get; set; }
        public string HaveVisitorOrStudentVisa { get; set; }
        public string HaveYourSpouseVisitorOrStudentVisa { get; set; }
        public string SpuseRefusedAnyApplication { get; set; }
        public string PreferedDestination { get; set; }
        public string SpuseAppliedPermanentResidence { get; set; }
        public string SpuseAnyIllegalCase { get; set; }
        public string SpuseAppliedAnyVisa { get; set; }
        public string FurtherInformation { get; set; }
        public string PrimaryLanguage { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Case Case { get; set; }
    }
}
