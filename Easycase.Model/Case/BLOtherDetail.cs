using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLOtherDetail : IDisposable
    {
        public long OtherID { get; set; }
        public long CaseId { get; set; }
        public OtherEmployementHistory OtherEmployementHistory { get; set; }
        public OtherEmployementHistory SpouseEmployementHistoryModal { get; set; }
        public string TemporaryWorker { get; set; }
        public string SkilledWorker { get; set; }
        public string TradeCertificate { get; set; }
        public string ProvinceNominee { get; set; }
        public string JobOffer { get; set; }
        public string isBCEmployerOffer { get; set; }
        public Nullable<int> BCDistrict { get; set; }
        public string BCJobNOC { get; set; }
        public string YearsBCRelatedJobExperience { get; set; }
        public string BCCurrentEmployer { get; set; }
        public string IsPostSecondaryCredentialBC { get; set; }
        public Nullable<int> Wage { get; set; }
        public Nullable<int> CLBLevel { get; set; }
        public string IsECAObtained { get; set; }
        public string IsTradesCertificationTrainingAuthority { get; set; }
        public string EmployementHistory { get; set; }
        public string PaymentCycle { get; set; }
        public Nullable<int> FullTimeEmployees { get; set; }
        public Nullable<int> HourlyWage { get; set; }
        public string AppliedPNPFromEmployerPast { get; set; }
        public string IsEmployerWillingToSupportPNP { get; set; }
        public string PositionsApplied { get; set; }
        public string SpouseEmployementHistory { get; set; }
        public string FamilyLivingCanada { get; set; }
        public string Children { get; set; }
        public Nullable<int> ChildrenCount { get; set; }
        public string ChildrenAges { get; set; }
        public Nullable<int> Networth { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.OtherDetails.Where(d => d.ID == this.OtherID).FirstOrDefault();
                    if (_case != null)
                    {
                        _case.AppliedPNPFromEmployerPast = Convert.ToBoolean(this.AppliedPNPFromEmployerPast);
                        _case.BCCurrentEmployer = Convert.ToBoolean(this.BCCurrentEmployer);
                        _case.BCDistrict = this.BCDistrict;
                        _case.UpdatedOn = DateTime.Now;
                        _case.BCJobNOC = this.BCJobNOC;
                        _case.Children = Convert.ToBoolean(this.Children);
                        _case.ChildrenAges = this.ChildrenAges;
                        _case.ChildrenCount = this.ChildrenCount;
                        _case.CLBLevel = this.CLBLevel;
                        _case.EmployementHistory = this.EmployementHistory;
                        _case.FamilyLivingCanada = Convert.ToBoolean(this.FamilyLivingCanada);
                        _case.FullTimeEmployees = this.FullTimeEmployees;
                        _case.HourlyWage = this.HourlyWage;
                        _case.IsBCEmployerOffer = Convert.ToBoolean(this.isBCEmployerOffer);
                        _case.IsECAObtained = Convert.ToBoolean(this.IsECAObtained);
                        _case.IsEmployerWillingToSupportPNP = Convert.ToBoolean(this.IsEmployerWillingToSupportPNP);
                        _case.IsPostSecondaryCredentialBC = Convert.ToBoolean(this.IsPostSecondaryCredentialBC);
                        _case.IsTradesCertificationTrainingAuthority = Convert.ToBoolean(this.IsTradesCertificationTrainingAuthority);
                        _case.JobOffer = Convert.ToBoolean(this.JobOffer);
                        _case.Networth = this.Networth;
                        _case.PaymentCycle = this.PaymentCycle;
                        _case.PositionsApplied = this.PositionsApplied;
                        _case.ProvinceNominee = Convert.ToBoolean(this.ProvinceNominee);
                        _case.SkilledWorker = Convert.ToBoolean(this.SkilledWorker);
                        _case.SpouseEmployementHistory = this.SpouseEmployementHistory;
                        _case.TemporaryWorker = Convert.ToBoolean(this.TemporaryWorker);
                        _case.TradeCertificate = Convert.ToBoolean(this.TradeCertificate);
                        _case.Wage = this.Wage;
                        _case.YearsBCRelatedJobExperience = this.YearsBCRelatedJobExperience;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        var _educationDetail = new DataModel.OtherDetail
                        {
                            CaseId = this.CaseId,
                            YearsBCRelatedJobExperience = this.YearsBCRelatedJobExperience,
                            Wage = this.Wage,
                            TradeCertificate = Convert.ToBoolean(this.TradeCertificate),
                            TemporaryWorker = Convert.ToBoolean(this.TemporaryWorker),
                            SpouseEmployementHistory = this.SpouseEmployementHistory,
                            SkilledWorker = Convert.ToBoolean(this.SkilledWorker),
                            AppliedPNPFromEmployerPast = Convert.ToBoolean(this.AppliedPNPFromEmployerPast),
                            BCCurrentEmployer = Convert.ToBoolean(this.BCCurrentEmployer),
                            BCDistrict = this.BCDistrict,
                            BCJobNOC = this.BCJobNOC,
                            Children = Convert.ToBoolean(this.Children),
                            ChildrenAges = this.ChildrenAges,
                            ChildrenCount = this.ChildrenCount,
                            CLBLevel = this.CLBLevel,
                            EmployementHistory = this.EmployementHistory,
                            FamilyLivingCanada = Convert.ToBoolean(this.FamilyLivingCanada),
                            FullTimeEmployees = this.FullTimeEmployees,
                            HourlyWage = this.HourlyWage,
                            IsBCEmployerOffer = Convert.ToBoolean(this.isBCEmployerOffer),
                            IsECAObtained = Convert.ToBoolean(this.IsECAObtained),
                            IsEmployerWillingToSupportPNP = Convert.ToBoolean(this.IsEmployerWillingToSupportPNP),
                            IsPostSecondaryCredentialBC = Convert.ToBoolean(this.IsPostSecondaryCredentialBC),
                            IsTradesCertificationTrainingAuthority = Convert.ToBoolean(this.IsTradesCertificationTrainingAuthority),
                            JobOffer = Convert.ToBoolean(this.JobOffer),
                            Networth = this.Networth,
                            PaymentCycle = this.PaymentCycle,
                            PositionsApplied = this.PositionsApplied,
                            ProvinceNominee = Convert.ToBoolean(this.ProvinceNominee),
                            UpdatedOn = DateTime.Now,
                        };
                        DB.OtherDetails.Add(_educationDetail);
                    }
                    DB.SaveChanges();
                }
                return new Tuple<bool, string>(true, Messages.SUCCESS);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public BLOtherDetail GetByCaseId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _otherDetails = DB.OtherDetails.Where(d => d.CaseId == id).FirstOrDefault();
                    var client = new BLOtherDetail
                    {
                        OtherID = _otherDetails.ID,
                        CaseId = _otherDetails.CaseId,
                        AppliedPNPFromEmployerPast= ConvertBoolToString(_otherDetails.AppliedPNPFromEmployerPast),
                        TemporaryWorker= ConvertBoolToString(_otherDetails.TemporaryWorker),
                        TradeCertificate= ConvertBoolToString(_otherDetails.TradeCertificate),
                        Wage=_otherDetails.Wage,
                        YearsBCRelatedJobExperience=_otherDetails.YearsBCRelatedJobExperience,
                        BCCurrentEmployer= ConvertBoolToString(_otherDetails.BCCurrentEmployer),
                        SkilledWorker= ConvertBoolToString(_otherDetails.SkilledWorker),
                        ProvinceNominee= ConvertBoolToString(_otherDetails.ProvinceNominee),
                        PositionsApplied=_otherDetails.PositionsApplied,
                        PaymentCycle=_otherDetails.PaymentCycle,
                        Networth=_otherDetails.Networth,
                        JobOffer= ConvertBoolToString(_otherDetails.JobOffer),
                        BCDistrict=_otherDetails.BCDistrict,
                        BCJobNOC=_otherDetails.BCJobNOC,
                        Children= ConvertBoolToString(_otherDetails.Children),
                        ChildrenAges=_otherDetails.ChildrenAges,
                        ChildrenCount=_otherDetails.ChildrenCount,
                        CLBLevel=_otherDetails.CLBLevel,
                        EmployementHistory=_otherDetails.EmployementHistory,
                        FamilyLivingCanada= ConvertBoolToString(_otherDetails.FamilyLivingCanada),
                        FullTimeEmployees=_otherDetails.FullTimeEmployees,
                        HourlyWage=_otherDetails.HourlyWage,
                        isBCEmployerOffer= ConvertBoolToString(_otherDetails.IsBCEmployerOffer),
                        IsECAObtained= ConvertBoolToString(_otherDetails.IsECAObtained),
                        IsEmployerWillingToSupportPNP= ConvertBoolToString(_otherDetails.IsEmployerWillingToSupportPNP),
                        IsPostSecondaryCredentialBC= ConvertBoolToString(_otherDetails.IsPostSecondaryCredentialBC),
                        IsTradesCertificationTrainingAuthority= ConvertBoolToString(_otherDetails.IsTradesCertificationTrainingAuthority),
                        SpouseEmployementHistory=_otherDetails.SpouseEmployementHistory,
                    };
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLOtherDetail();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
        private string ConvertBoolToString(bool? value)
        {
            if (value != null)
            {
                if (value.Value)
                    return "true";
                else
                    return "false";
            }
            else
                return "";
        }
    }

    public class OtherEmployementHistory
    {
        public string[] Type { get; set; }
        public string[] JobTitle { get; set; }
        public string[] JobDuties { get; set; }
        public string[] CompanyEmployer { get; set; }
        public string[] Duration { get; set; }
        public int[] HourWeek { get; set; }
        public string[] Country { get; set; }
    }
}
