using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Corporate
{
    public class BLCasesNote : IDisposable
    {
        public long ID { get; set; }
        public Nullable<long> CorporateProfileId { get; set; }
        public Nullable<long> CaseId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        /// <summary>
        /// Save Case note
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = new DataModel.CasesNote
                    {
                        Notes = this.Notes,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Date=this.Date,
                        Subject=this.Subject,
                        CorporateProfileId=this.CorporateProfileId,
                        CaseId=this.CaseId
                    };
                    DB.CasesNotes.Add(casenote);
                    DB.SaveChanges();
                    id = casenote.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }

        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = new DataModel.CasesNote
                    {
                        Notes = this.Notes,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Date = this.Date,
                        Subject = this.Subject,
                        CorporateProfileId = this.CorporateProfileId,
                        ID=this.ID,
                        CaseId=this.CaseId
                    };
                    DB.Entry(casenote).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = casenote.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }

        /// <summary>
        /// Get Case note by corporate id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BLCasesNote> GetByCorporateProfileId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = DB.CasesNotes.Where(d => d.CorporateProfileId == id).Select(c => new BLCasesNote
                    {
                        ID = c.ID,
                        Notes = c.Notes,
                        CorporateProfileId=c.CorporateProfileId,
                        Subject=c.Subject,
                        Date=c.Date,
                        CreatedOn=c.CreatedOn,
                        CreatedBy = c.CreatedBy,
                    }).ToList();
                    return casenote;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCasesNote>();
            }
        }

        public List<BLCasesNote> GetByCaseId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = DB.CasesNotes.Where(d => d.CaseId == id).Select(c => new BLCasesNote
                    {
                        ID = c.ID,
                        Notes = c.Notes,
                        CaseId = c.CaseId,
                        Subject = c.Subject,
                        Date = c.Date,
                        CreatedOn = c.CreatedOn,
                        CreatedBy = c.CreatedBy,
                    }).ToList();
                    return casenote;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCasesNote>();
            }
        }

        /// <summary>
        /// Delete case note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CasesNotes.Where(d => d.ID == id).FirstOrDefault();
                    DB.Entry(client).State = System.Data.Entity.EntityState.Deleted;
                    DB.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
