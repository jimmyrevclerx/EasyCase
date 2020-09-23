using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLCaseStatus : IDisposable
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> Show { get; set; }

        public List<BLCaseStatus> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casestatus = DB.CaseStatus.Select(c => new BLCaseStatus
                    {
                        ID = c.ID,
                        Name = c.Name,
                        CreatedOn = c.CreatedOn,
                        Show=c.Show
                    }).OrderByDescending(d => d.CreatedOn).ToList();
                    return casestatus;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCaseStatus>();
            }
        }
        public Tuple<bool, string> Save(string name, string createdBy, long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = new DataModel.CaseStatu
                    {
                        Name = name,
                        CreatedOn = DateTime.Now,
                        CreatedBy = createdBy,
                        Show=true
                    };
                    if (id > 0)
                    {
                        _case.ID = id;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        DB.CaseStatus.Add(_case);
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
        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CaseStatus.Where(d => d.ID == id).FirstOrDefault();
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

        public List<BLCaseStatus> GetByStatusAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casestatus = DB.CaseStatus.Where(s=>s.Show==true).Select(c => new BLCaseStatus
                    {
                        ID = c.ID,
                        Name = c.Name,
                        CreatedOn = c.CreatedOn,
                        Show = c.Show
                    }).OrderByDescending(d => d.CreatedOn).ToList();
                    return casestatus;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCaseStatus>();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
