using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Calendar
{
    public class BLBusinessHours : IDisposable
    {
        public long ID { get; set; }
        public Nullable<int> ConsultingDuration { get; set; }
        public Nullable<int> WaitTime { get; set; }
        public Nullable<int> LeadDays { get; set; }
        public string CreatedBy { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var businessHours = new DataModel.BusinessHour
                    {
                        ConsultingDuration=this.ConsultingDuration,
                        LeadDays=this.LeadDays,
                        WaitTime=this.WaitTime,
                        CreatedBy=this.CreatedBy
                    };
                    DB.BusinessHours.Add(businessHours);
                    DB.SaveChanges();
                    id = businessHours.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public BLBusinessHours GetById(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.BusinessHours.Where(d => d.CreatedBy == id).Select(c => new BLBusinessHours
                    {
                        ID = c.ID,
                        ConsultingDuration=c.ConsultingDuration,
                        LeadDays=c.LeadDays,
                        WaitTime=c.WaitTime
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLBusinessHours();
            }
        }
        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var businessHours = new DataModel.BusinessHour
                    {
                        ConsultingDuration = this.ConsultingDuration,
                        LeadDays = this.LeadDays,
                        WaitTime = this.WaitTime,
                        CreatedBy = this.CreatedBy,
                        ID=this.ID
                    };
                    DB.Entry(businessHours).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = businessHours.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
