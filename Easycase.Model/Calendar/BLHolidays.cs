using Easycase.Extension;
using Easycase.Model.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Calendar
{
    public class BLHolidays : IDisposable
    {
        public long ID { get; set; }
        public Nullable<int> Year { get; set; }
        public string HolidayOfYear { get; set; }
        public string CreatedBy { get; set; }
        public List<string> HolidayList { get; set; }
        public string HolidayDates { get; set; }
        public List<DateTime> HolidayDateList { get; set; }
        public List<BLCalendarBindJsonModal> BLCalendarBindJson { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var businessHours = new DataModel.Holiday
                    {
                        CreatedBy = this.CreatedBy,
                        HolidayOfYear=this.HolidayOfYear,
                        Year=this.Year,
                        HolidayDates=this.HolidayDates
                    };
                    DB.Holidays.Add(businessHours);
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
        public BLHolidays GetById(string id,int year)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var holiday = DB.Holidays.Where(d => d.CreatedBy == id&& d.Year==year).Select(c => new BLHolidays
                    {
                        ID = c.ID,
                        CreatedBy=c.CreatedBy,
                        Year=c.Year,
                        HolidayOfYear = c.HolidayOfYear,
                        HolidayDates=c.HolidayDates
                }).FirstOrDefault();
                    return holiday;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLHolidays();
            }
        }
        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var holiday = new DataModel.Holiday
                    {
                        CreatedBy = this.CreatedBy,
                        HolidayOfYear = this.HolidayOfYear,
                        Year = this.Year,
                        ID=this.ID,
                        HolidayDates=this.HolidayDates
                    };
                    DB.Entry(holiday).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = holiday.ID;
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
