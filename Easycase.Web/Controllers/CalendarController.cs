using Easycase.Extension;
using Easycase.Model.Calendar;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        BLManageHours manageHours = new BLManageHours();
        BLBusinessHours businessHours = new BLBusinessHours();
        BLHolidays holidays = new BLHolidays();
        // GET: Calendar
        public ActionResult Index()
        {
            BLCalendar bLCalendar = new BLCalendar();
            var userId = User.Identity.GetUserId();
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday != null)
            {
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
                holiday.HolidayDateList = JsonConvert.DeserializeObject<List<DateTime>>(holiday.HolidayDates);
            }
            bLCalendar.bLHolidays = holiday;
            return View(bLCalendar);
        }

        public PartialViewResult CalendarPartial()
        {
            var userId = User.Identity.GetUserId();
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday != null)
            {
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
                holiday.HolidayDateList = JsonConvert.DeserializeObject<List<DateTime>>(holiday.HolidayDates);
                List<BLCalendarBindJsonModal> bLCalendarBindJsonModals = new List<BLCalendarBindJsonModal>();
                foreach (var item in holiday.HolidayDateList)
                {
                    BLCalendarBindJsonModal bLCalendarBindJsonModal = new BLCalendarBindJsonModal();
                    bLCalendarBindJsonModal.title = "Holiday";
                    bLCalendarBindJsonModal.start = item.ToString("yyyy-MM-dd");
                    bLCalendarBindJsonModals.Add(bLCalendarBindJsonModal);
                }
                holiday.BLCalendarBindJson = bLCalendarBindJsonModals;
            }
            return PartialView("_partialCalendar", holiday);
        }

        public ActionResult Manage()
        {
            var userId = User.Identity.GetUserId();
            BLBusinessHoursModal bLBusinessHoursModal = new BLBusinessHoursModal();
            var result = manageHours.GetAll(userId);
            var timeList = Common.GetTimeIntervals();
            ViewBag.TimeInterval = new SelectList(timeList);
            var ConsultingSession = Common.GetConsultinghours();
            ViewBag.ConsultingSessionList = new SelectList(ConsultingSession, "Value", "Text");
            var WaitTimes = Common.GetWaitingHours();
            ViewBag.WaitTimeList = new SelectList(WaitTimes, "Value", "Text");
            var LeadHours = Common.GetLeadHours();
            ViewBag.LeadHoursList = new SelectList(LeadHours);
            // Get business hours
            var businessHour = businessHours.GetById(userId);
            bLBusinessHoursModal.BusinessHourId = businessHour != null ? businessHour.ID : 0;
            bLBusinessHoursModal.ConsultingSession = businessHour != null ? businessHour.ConsultingDuration : 0;
            bLBusinessHoursModal.WaitTime = businessHour != null ? businessHour.WaitTime : 0;
            bLBusinessHoursModal.LeadDays = businessHour != null ? businessHour.LeadDays : 0;
            bLBusinessHoursModal.bLManageHours = result;

            // get holidays
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday != null)
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
            else
                holiday = new BLHolidays();
            bLBusinessHoursModal.bLHolidays = holiday;
            return View(bLBusinessHoursModal);
        }
        [HttpPost]
        public ActionResult SaveManageHours(BLManageSaveModal modal)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                // Save Manage Hours
                var result = modal.Update(userId);
                // Save Business Hours
                BLBusinessHours bLBusinessHours = new BLBusinessHours()
                {
                    ConsultingDuration = modal.ConsultingSession,
                    LeadDays = modal.LeadDays,
                    WaitTime = modal.WaitTime,
                    CreatedBy = userId,
                    ID = modal.BusinessHourId
                };
                if (bLBusinessHours.ID > 0)
                {
                    bLBusinessHours.Update();
                }
                else
                {
                    bLBusinessHours.Save();
                }
                return Json(result.Item2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveHolidays(int day, int month)
        {
            List<string> holidaymonths = new List<string>();
            List<DateTime> holidaydates = new List<DateTime>();
            string smonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + " " + day;
            DateTime dt = new DateTime(DateTime.Now.Year, month, day);
            holidaymonths.Add(smonth);
            holidaydates.Add(dt);
            var userId = User.Identity.GetUserId();
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday == null)
            {
                holiday = new BLHolidays()
                {
                    CreatedBy = userId,
                    HolidayOfYear = JsonConvert.SerializeObject(holidaymonths),
                    Year = DateTime.Now.Year,
                    HolidayDates= JsonConvert.SerializeObject(holidaydates),
                };
                holiday.Save();
            }
            else
            {
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
                holiday.HolidayList.Add(smonth);
                holiday.HolidayOfYear = JsonConvert.SerializeObject(holiday.HolidayList);

                holiday.HolidayDateList = JsonConvert.DeserializeObject<List<DateTime>>(holiday.HolidayDates);
                holiday.HolidayDateList.Add(dt);
                holiday.HolidayDates = JsonConvert.SerializeObject(holiday.HolidayDateList);
                var anyDuplicate = holiday.HolidayDateList.GroupBy(x => x).Any(g => g.Count() > 1);
                if (!anyDuplicate)
                {
                    holiday.Update();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HolidayPartial()
        {
            var userId = User.Identity.GetUserId();
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday != null)
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
            return PartialView("_partialHolidays", holiday);
        }
        //public ActionResult GetCalendarData()
        //{
        //    // Initialization.  
        //    JsonResult result = new JsonResult();

        //    try
        //    {
        //        // Loading.  
        //        var userId = User.Identity.GetUserId();
        //        var holiday = holidays.GetById(userId, DateTime.Now.Year);
        //        if (holiday != null)
        //        {
        //            holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
        //            holiday.HolidayDateList = JsonConvert.DeserializeObject<List<DateTime>>(holiday.HolidayDates);
        //        }

        //        // Processing.  
        //        result = this.Json(holiday, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Info  
        //        Console.Write(ex);
        //    }

        //    // Return info.  
        //    return result;
        //}

        public ActionResult RemoveHolidays(string date) 
        {
            List<string> holidaymonths = new List<string>();
            var userId = User.Identity.GetUserId();
            var holiday = holidays.GetById(userId, DateTime.Now.Year);
            if (holiday != null)
            {
                holiday.HolidayList = JsonConvert.DeserializeObject<List<string>>(holiday.HolidayOfYear);
                int index = holiday.HolidayList.IndexOf(date);
                holiday.HolidayList.Remove(date);
                holiday.HolidayOfYear = JsonConvert.SerializeObject(holiday.HolidayList);

                holiday.HolidayDateList = JsonConvert.DeserializeObject<List<DateTime>>(holiday.HolidayDates);
                holiday.HolidayDateList.RemoveAt(index);
                holiday.HolidayDates = JsonConvert.SerializeObject(holiday.HolidayDateList);
                holiday.Update();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}