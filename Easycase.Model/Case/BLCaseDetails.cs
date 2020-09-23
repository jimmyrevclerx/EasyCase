using Easycase.Extension;
using Easycase.Extension.Enums;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLCaseDetails : IDisposable
    {
        public string CaseNumber { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string MartialStatus { get; set; }
        public string PurposeInCanada { get; set; }
        public string RelationToPrimaryApplicant { get; set; }
        public string ContactInfo { get; set; }
        public string VisaType { get; set; }
        public string OtherTypeVisa { get; set; }
        public string HowLearnAbout { get; set; }
        public string Address { get; set; }
        public string CountryOfCitizenship{ get; set; }
        public string CountryOfResidence { get; set; }
        public long CaseID { get; set; }
        public Nullable<long> ClientId { get; set; }
        public Nullable<long> CaseTypeId { get; set; }
        public int Age { get; set; }
        public string ProfileImage { get; set; }
        public long? Prefix { get; set; }
        public long? MartialStatusId { get; set; }
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

        public BLCaseDetails GetCaseDetailById(long id)
        {
            try
            {
                string defaultprofilepath = ConfigurationManager.AppSettings["DefaultProfile"];
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ProfileInformations.Where(d => d.ProfileId == id).Select(c => new BLCaseDetails
                    {
                        Address = c.Address,
                        CaseID = c.ProfileId,
                        ClientId = c.Case.ClientId,
                        CountryOfCitizenship = c.Case.Client.Citizenship,
                        CountryOfResidence = c.Case.Client.Country,
                        DOB = c.Case.Client.DOB,
                        FirstName = c.Case.Client.FirstName,
                        LastName = c.Case.Client.LastName,
                        MartialStatusId = c.Case.Client.MartialStatusId,
                        Prefix = c.Case.Client.Prefix,
                        CaseNumber=c.Case.CaseNumber,
                        CaseTypeId=c.Case.CaseTypeId,
                        ContactInfo=c.Case.Client.Email,
                        FullName=c.Case.Client.FirstName+" "+c.Case.Client.LastName,
                        FurtherInformation=c.FurtherInformation,
                        HaveVisitorOrStudentVisa=c.HaveVisitorOrStudentVisa,
                        HaveYouAcceptedSchoolCollege=c.HaveYouAcceptedSchoolCollege,
                        HaveYourSpouseVisitorOrStudentVisa=c.HaveYourSpouseVisitorOrStudentVisa,
                        HearFromAboutus=c.HearFromAboutus,
                        HowLearnAbout=c.HearFromAboutus,
                        LikeToDoInCanada=c.LikeToDoInCanada,
                        MartialStatus= c.Case.Client.MartialStatu!=null? c.Case.Client.MartialStatu.Name:"",
                        OtherTypeVisa=c.LikeToDoInCanada,
                        Phone=c.Case.Client.PhoneNo,
                        PreferedDestination=c.PreferedDestination,
                        PrimaryLanguage=c.PrimaryLanguage,
                        PurposeInCanada=c.Case.Client.Purpose!=null? c.Case.Client.Purpose.Name:"",
                        RelationToPrimaryApplicant=c.RelationToPrimaryApplicant,
                        SpuseAnyIllegalCase=c.SpuseAnyIllegalCase,
                        SpuseAppliedAnyVisa=c.SpuseAppliedAnyVisa,
                        SpuseAppliedPermanentResidence=c.SpuseAppliedPermanentResidence,
                        SpuseRefusedAnyApplication=c.SpuseRefusedAnyApplication,
                        VisitPrevious=c.VisitPrevious,
                        ProfileImage= c.Case.Client.Image != null ? c.Case.Client.Image.ImagePath : defaultprofilepath,
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCaseDetails();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
