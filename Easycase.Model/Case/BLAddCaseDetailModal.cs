using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLAddCaseDetailModal : IDisposable
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string RelationToPrimaryApplicant { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string CountryOfCitizenship { get; set; }
        public string CountryOfResidence { get; set; }
        public long CaseID { get; set; }
        public long ProfileInformationID { get; set; }
        public Nullable<long> ClientId { get; set; }
        public int? Prefix { get; set; }
        public string PrimaryLanguage { get; set; }
        public string Phone { get; set; }
        public string LikeToDoInCanada { get; set; }
        public string HaveYouAcceptedSchoolCollege { get; set; }
        public string VisitPrevious { get; set; }
        public string HaveVisitorOrStudentVisa { get; set; }
        public string HaveYourSpouseVisitorOrStudentVisa { get; set; }
        public string SpuseRefusedAnyApplication { get; set; }
        public string PreferedDestination { get; set; }
        public string SpuseAppliedPermanentResidence { get; set; }
        public string SpuseAppliedAnyVisa { get; set; }
        public string SpuseAnyIllegalCase { get; set; }
        public string FurtherInformation { get; set; }
        public string HearFromAboutus { get; set; }
        public long? MartialStatusId { get; set; }

        public Tuple<bool, string,long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.ProfileInformation
                    {
                        Address=this.Address,
                        ProfileId=this.CaseID,
                        FurtherInformation=this.FurtherInformation,
                        HaveVisitorOrStudentVisa=this.HaveVisitorOrStudentVisa,
                        HaveYourSpouseVisitorOrStudentVisa=this.HaveYourSpouseVisitorOrStudentVisa,
                        HaveYouAcceptedSchoolCollege=this.HaveYouAcceptedSchoolCollege,
                        HearFromAboutus=this.HearFromAboutus,
                        LikeToDoInCanada=this.LikeToDoInCanada,
                        PreferedDestination=this.PreferedDestination,
                        RelationToPrimaryApplicant=this.RelationToPrimaryApplicant,
                        SpuseAnyIllegalCase=this.SpuseAnyIllegalCase,
                        SpuseAppliedAnyVisa=this.SpuseAppliedAnyVisa,
                        SpuseAppliedPermanentResidence=this.SpuseAppliedPermanentResidence,
                        SpuseRefusedAnyApplication=this.SpuseRefusedAnyApplication,
                        UpdatedOn=DateTime.Now,
                        VisitPrevious=this.VisitPrevious,
                        PrimaryLanguage=this.PrimaryLanguage
                    };
                    DB.ProfileInformations.Add(client);
                    DB.SaveChanges();
                    id = client.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message,0);
            }
        }
        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var profileInfo = DB.ProfileInformations.Where(d => d.ID == this.ProfileInformationID).FirstOrDefault();
                    profileInfo.Address = this.Address;
                        profileInfo.ProfileId = this.CaseID;
                    profileInfo.FurtherInformation = this.FurtherInformation;
                    profileInfo.HaveVisitorOrStudentVisa = this.HaveVisitorOrStudentVisa;
                    profileInfo.HaveYourSpouseVisitorOrStudentVisa = this.HaveYourSpouseVisitorOrStudentVisa;
                    profileInfo.HaveYouAcceptedSchoolCollege = this.HaveYouAcceptedSchoolCollege;
                    profileInfo.HearFromAboutus = this.HearFromAboutus;
                    profileInfo.LikeToDoInCanada = this.LikeToDoInCanada;
                    profileInfo.PreferedDestination = this.PreferedDestination;
                    profileInfo.RelationToPrimaryApplicant = this.RelationToPrimaryApplicant;
                    profileInfo.SpuseAnyIllegalCase = this.SpuseAnyIllegalCase;
                    profileInfo.SpuseAppliedAnyVisa = this.SpuseAppliedAnyVisa;
                    profileInfo.SpuseAppliedPermanentResidence = this.SpuseAppliedPermanentResidence;
                    profileInfo.SpuseRefusedAnyApplication = this.SpuseRefusedAnyApplication;
                    profileInfo.UpdatedOn = DateTime.Now;
                    profileInfo.VisitPrevious = this.VisitPrevious;
                    profileInfo.PrimaryLanguage = this.PrimaryLanguage;

                    DB.Entry(profileInfo).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = profileInfo.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public BLAddCaseDetailModal GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ProfileInformations.Where(d => d.ProfileId == id).Select(c => new BLAddCaseDetailModal
                    {
                        Address = c.Address,
                        CaseID = c.ProfileId,
                        FurtherInformation = c.FurtherInformation,
                        HaveVisitorOrStudentVisa = c.HaveVisitorOrStudentVisa,
                        HaveYourSpouseVisitorOrStudentVisa = c.HaveYourSpouseVisitorOrStudentVisa,
                        HaveYouAcceptedSchoolCollege = c.HaveYouAcceptedSchoolCollege,
                        HearFromAboutus = c.HearFromAboutus,
                        LikeToDoInCanada = c.LikeToDoInCanada,
                        PreferedDestination = c.PreferedDestination,
                        RelationToPrimaryApplicant = c.RelationToPrimaryApplicant,
                        SpuseAnyIllegalCase = c.SpuseAnyIllegalCase,
                        SpuseAppliedAnyVisa = c.SpuseAppliedAnyVisa,
                        SpuseAppliedPermanentResidence = c.SpuseAppliedPermanentResidence,
                        SpuseRefusedAnyApplication = c.SpuseRefusedAnyApplication,
                        VisitPrevious = c.VisitPrevious,
                        ClientId=c.Case.ClientId,
                        CountryOfCitizenship=c.Case.Client.Citizenship,
                        CountryOfResidence=c.Case.Client.Country,
                        Phone=c.Case.Client.PhoneNo,
                        DOB=c.Case.Client.DOB,
                        Email=c.Case.Client.Email,
                        FirstName=c.Case.Client.FirstName,
                        ProfileInformationID = c.ID,
                        LastName=c.Case.Client.LastName,
                        MartialStatusId=c.Case.Client.MartialStatusId,
                        Prefix=c.Case.Client.Prefix,
                        PrimaryLanguage=c.PrimaryLanguage,
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLAddCaseDetailModal();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
