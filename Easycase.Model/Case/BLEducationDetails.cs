using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLEducationDetails : IDisposable
    {
        public long ID { get; set; }
        public long CaseId { get; set; }
        public string EducationHistory { get; set; }
        public string ECACompleted { get; set; }
        public string CICEnglishExam { get; set; }
        public string SpouseEducation { get; set; }
        public string CICEnglishExamSpouse { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.EducationDetails.Where(d => d.ID == this.ID).FirstOrDefault();
                    if (_case != null)
                    {
                        _case.CICEnglishExam = this.CICEnglishExam;
                        _case.CICEnglishExamSpouse = this.CICEnglishExamSpouse;
                        _case.ECACompleted = this.ECACompleted;
                        _case.UpdatedOn = DateTime.Now;
                        _case.EducationHistory = this.EducationHistory;
                        _case.SpouseEducation = this.SpouseEducation;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        var _educationDetail = new DataModel.EducationDetail
                        {
                            CaseId = this.CaseId,
                            CICEnglishExam = this.CICEnglishExam,
                            CICEnglishExamSpouse = this.CICEnglishExamSpouse,
                            ECACompleted = this.ECACompleted,
                            UpdatedOn = DateTime.Now,
                            EducationHistory = this.EducationHistory,
                            SpouseEducation = this.SpouseEducation,
                        };
                        DB.EducationDetails.Add(_educationDetail);
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

        public BLEducationDetails GetByCaseId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.EducationDetails.Where(d => d.CaseId == id).Select(c => new BLEducationDetails
                    {
                        ID = c.ID,
                        CaseId=c.CaseId,
                        CICEnglishExam=c.CICEnglishExam,
                        CICEnglishExamSpouse=c.CICEnglishExamSpouse,
                        ECACompleted=c.ECACompleted,
                        EducationHistory=c.EducationHistory,
                        SpouseEducation=c.SpouseEducation,
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLEducationDetails();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
