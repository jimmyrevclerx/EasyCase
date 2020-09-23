using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Task
{
    public class BLTaskNote
    {
        public long ID { get; set; }
        public long TaskId { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string NotifyUser { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var tasknote = new DataModel.TaskNote
                    {
                        Notes = this.Notes,
                        CreatedOn = DateTime.Now,
                        Date = this.Date,
                        Subject = this.Subject,
                        TaskId = this.TaskId,
                        NotifyUser=this.NotifyUser
                    };
                    DB.TaskNotes.Add(tasknote);
                    DB.SaveChanges();
                    id = tasknote.ID;
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
                    var tasknote = new DataModel.TaskNote
                    {
                        Notes = this.Notes,
                        CreatedOn = DateTime.Now,
                        Date = this.Date,
                        Subject = this.Subject,
                        TaskId = this.TaskId,
                        ID = this.ID,
                        NotifyUser=this.NotifyUser
                    };
                    DB.Entry(tasknote).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = tasknote.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public List<BLTaskNote> GetByCorporateProfileId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = DB.TaskNotes.Where(d => d.TaskId == id).Select(c => new BLTaskNote
                    {
                        ID = c.ID,
                        Notes = c.Notes,
                        TaskId = c.TaskId,
                        Subject = c.Subject,
                        Date = c.Date==null?DateTime.Now: c.Date,
                        CreatedOn = c.CreatedOn,
                        NotifyUser=c.NotifyUser
                    }).ToList();
                    return casenote;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLTaskNote>();
            }
        }
        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.TaskNotes.Where(d => d.ID == id).FirstOrDefault();
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
    }
}
