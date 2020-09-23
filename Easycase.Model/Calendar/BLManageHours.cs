using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Calendar
{
    public class BLManageHours: IDisposable
    {
        public long ID { get; set; }
        public string Day { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LunchStartTime { get; set; }
        public string LunchEndTime { get; set; }
        

        public List<BLManageHours> GetAll(string createdby)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var managehours = DB.ManageHours.Where(d => d.CreatedBy == createdby).ToList();
                    if (managehours.Count == 0)
                    {
                        var _manageHours= new List<BLManageHours>();
                        for (int i = 1; i <= 7; i++)
                        {
                            string day = "";
                            if (i == 1)
                                day = "Sunday";
                            else if (i == 2)
                                day = "Monday";
                            else if (i == 3)
                                day = "Tuesday";
                            else if (i == 4)
                                day = "Wednesday";
                            else if (i == 5)
                                day = "Thursday";
                            else if (i == 6)
                                day = "Friday";
                            else if (i == 7)
                                day = "Saturday";
                            var _manageHour = new BLManageHours() {
                                Day = day,
                                EndTime = "12:00 AM",
                                IsActive = false,
                                LunchEndTime = "12:00 AM",
                                LunchStartTime = "12:00 AM",
                                StartTime = "12:00 AM",
                            };
                            _manageHours.Add(_manageHour);
                        }
                        return _manageHours;
                    }
                    else
                    {
                        var client = managehours.Select(c => new BLManageHours
                        {
                            ID = c.ID,
                            Day = c.Day,
                            EndTime = string.IsNullOrEmpty(c.EndTime) ? "" : c.EndTime,
                            IsActive = c.IsActive,
                            LunchEndTime = string.IsNullOrEmpty(c.LunchEndTime) ? "" : c.LunchEndTime,
                            LunchStartTime = string.IsNullOrEmpty(c.LunchStartTime) ? "" : c.LunchStartTime,
                            StartTime = string.IsNullOrEmpty(c.StartTime) ? "" : c.StartTime,
                        }).ToList();
                        return client;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLManageHours>();
            }
        }
        
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
