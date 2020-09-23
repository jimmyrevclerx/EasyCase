using Easycase.DataModel;
using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Calendar
{
    public class BLManageSaveModal : IDisposable
    {
        public long[] ID { get; set; }
        public string[] Day { get; set; }
        public Nullable<bool>[] IsActive { get; set; }
        public string[] StartTime { get; set; }
        public string[] EndTime { get; set; }
        public string[] LunchStartTime { get; set; }
        public string[] LunchEndTime { get; set; }
        public long BusinessHourId { get; set; }
        public int ConsultingSession { get; set; }
        public int WaitTime { get; set; }
        public int LeadDays { get; set; }

        public Tuple<bool, string> Update(string userId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var manageHours = DB.ManageHours.Where(d=>d.CreatedBy== userId).ToList();
                    if (manageHours.Count == 0)
                    {
                        for (int i = 0; i < this.ID.Count(); i++)
                        {
                            var managerHour = new ManageHour();
                            managerHour.IsActive = this.IsActive[i];
                            managerHour.StartTime = this.StartTime[i];
                            managerHour.EndTime = this.EndTime[i];
                            managerHour.LunchStartTime = this.LunchStartTime[i];
                            managerHour.LunchEndTime = this.LunchEndTime[i];
                            managerHour.CreatedBy = userId;
                            DB.ManageHours.Add(managerHour);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < manageHours.Count; i++)
                        {
                            manageHours[i].ID = this.ID[i];
                            manageHours[i].IsActive = this.IsActive[i];
                            manageHours[i].StartTime = this.StartTime[i];
                            manageHours[i].EndTime = this.EndTime[i];
                            manageHours[i].LunchStartTime = this.LunchStartTime[i];
                            manageHours[i].LunchEndTime = this.LunchEndTime[i];
                            manageHours[i].CreatedBy = userId;
                            DB.Entry(manageHours[i]).State = System.Data.Entity.EntityState.Modified;
                        }
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
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
