using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Setting
{
    public class BLServiceFee : IDisposable
    {
        public long ID { get; set; }
        public string ServiceName { get; set; }
        public string IsExpense { get; set; }
        public string Description { get; set; }
        public string AdditionalInfo { get; set; }
        public Nullable<double> Fee { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public List<BLServiceFee> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ServiceFees.Where(d=>d.Deleted==false).Select(c => new BLServiceFee
                    {
                        ID = c.ID,
                        CreatedOn = c.CreatedOn,
                        AdditionalInfo=c.AdditionalInfo,
                        CreatedBy=c.CreatedBy,
                        Deleted=c.Deleted,
                        Description=c.Description,
                        Fee=c.Fee,
                        IsExpense=c.IsExpense,
                        ServiceName=c.ServiceName
                    }).OrderByDescending(d => d.CreatedOn).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLServiceFee>();
            }
        }

        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ServiceFees.Where(d => d.ID == id).FirstOrDefault();
                    client.Deleted = true;
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
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
