using Easycase.Extension;
using Easycase.Model.Client;
using Easycase.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    public class TaskController : Controller
    {
        BLClientModel clientModel = new BLClientModel();
        BLTask bLTask = new BLTask();
        BLTaskNote taskNote = new BLTaskNote();
        // GET: Task
        public ActionResult Index()
        {
            var data = bLTask.GetAll();
            var Clients = clientModel.GetAll();
            ViewBag.Clients = new SelectList(Clients, "ID", "Name");
            return View(data);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            var timeList = Common.GetTimeIntervals();
            var Clients = clientModel.GetAll();
            ViewBag.Clients = new SelectList(Clients, "ID", "Name");
            ViewBag.TimeInterval = new SelectList(timeList);
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(BLTask bLTask)
        {
            if (ModelState.IsValid)
            {
                var result = bLTask.Save();
                return Json(result.Item2,JsonRequestBehavior.AllowGet);
            }
            return View(bLTask);
        }
        [HttpPost]
        public ActionResult ChangeAssignee(long[] mentionIds, long taskId) 
        {
            if (mentionIds != null)
            {
                bLTask.UpdateAssignee(mentionIds, taskId);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePriority(string priority, long taskId)
        {
            bLTask.UpdatePriority(priority, taskId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeStatus(string status, long taskId)
        {
            bLTask.UpdateStatus(status, taskId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeTaskName(string name, long taskId)
        {
            bLTask.UpdateTaskName(name, taskId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(long id,string action)
        {
            bool status = false;
            if (id > 0)
                status = bLTask.UpdateDeleteStatus(id,action);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OpenCaseNote(long id)
        {
            var Clients = clientModel.GetAll();
            ViewBag.Clients = new SelectList(Clients, "ID", "Name");
            var casenote = taskNote.GetByCorporateProfileId(id);
            return PartialView("_partialTaskNote", casenote);
        }

        [HttpPost]
        public ActionResult SaveCaseNote(BLTaskNote bLTaskNote)
        {
            bool status = false;
            if (bLTaskNote.ID > 0)
            {
                var result = bLTaskNote.Update();
                status = result.Item1;
            }
            else
            {
                var result = bLTaskNote.Save();
                status = result.Item1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTaskNotes(long id)
        {
            bool status = false;
            if (id > 0)
                status = taskNote.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
