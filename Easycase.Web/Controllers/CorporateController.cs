using Easycase.Model.Case;
using Easycase.Model.Corporate;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    public class CorporateController : Controller
    {
        BLCorporateProfile db = new BLCorporateProfile();
        BLCorporateViewProfile db_corporateViewProfile = new BLCorporateViewProfile();
        BLCorporatePosition db_corporatePosition = new BLCorporatePosition();
        BLCorporateJobDetail db_corporateJobDetail = new BLCorporateJobDetail();
        BLCasesNote db_CasesNote = new BLCasesNote();
        BLCaseStatus caseStatus = new BLCaseStatus();
        // GET: Corporate
        public ActionResult Index()
        {
            var data = db_corporateViewProfile.GetAll();
            return View(data);
        }

        // GET: Corporate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Corporate/Create
        public ActionResult Create(long? id)
        {
            var clientbl = new BLCorporateProfile();
            ViewData["SuccessCorporate"] = TempData["SuccessCorporate"];
            ViewData["FailedCorporate"] = TempData["FailedCorporate"];

            if (id != null)
                clientbl = db.GetById(id.Value);
            return View(clientbl);
        }

        // POST: Corporate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BLCorporateProfile model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                model.CreatedBy = userId;
                if (model.ID > 0)
                {
                    var result = model.Update();
                    TempData["ProfileId"] = result.Item3;
                }
                else
                {
                    var result = model.Save();
                    TempData["ProfileId"] = result.Item3;
                }
                return RedirectToAction("JobDetail", "Corporate");
            }
            return View(model);
        }

        // GET: Corporate/JobDetail
        public ActionResult JobDetail()
        {
            var _caseStatus = caseStatus.GetByStatusAll();
            var clientbl = new BLCorporateJobDetail();
            clientbl.ProfileId = TempData["ProfileId"]!=null?(long)TempData["ProfileId"]:0;
            TempData.Keep("ProfileId");
            var corporateJobDetail = clientbl.GetById(clientbl.ProfileId.Value);
            clientbl.CorporatePositions = corporateJobDetail!=null? corporateJobDetail.CorporatePositions:new List<BLCorporatePosition>();
            ViewBag.CaseStatus = new SelectList(_caseStatus, "ID", "Name", clientbl.Status);
            return View(clientbl);
        }

        // POST: Corporate/JobDetail
        [HttpPost]
        public ActionResult JobDetail(BLCorporateJobDetail model)
        {
            string returnMessage = string.Empty;
            long profileId = model.ProfileId.Value;
            if (model.ID > 0)
            {
                // delete all entries from corporate table
                var result1 = db_corporatePosition.Delete(model.ID);
                //Update entries
                var result = model.Update();
                returnMessage = result.Item2;
            }
            else
            {
                var result = model.Save();
                returnMessage = result.Item2;
            }
            return Json(new {Message=returnMessage,ProfileId= profileId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialPositions(long? id)
        {
            var clientbl = db.GetById(id.Value);
            return PartialView("_partialPositions", clientbl);
        }

        [HttpPost]
        public ActionResult GetJobDetail(long Id) 
        {
            var clientbl = db_corporateJobDetail.GetById(Id);
            return Json(clientbl, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Delete Corporate case
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(long id)
        {
            bool status = false;
            if (id > 0)
                status = db.UpdateDeleteStatus(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Open Case Note modal popup by partial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OpenCaseNote(long id) 
        {
            var casenote = db_CasesNote.GetByCorporateProfileId(id);
            return PartialView("_partialCaseNote", casenote);
        }

        /// <summary>
        /// Save Case note
        /// </summary>
        /// <param name="bLCasesNote"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCaseNote(BLCasesNote bLCasesNote) 
        {
            bool status = false;
            if (bLCasesNote.ID > 0)
            {
                var result = bLCasesNote.Update();
                status = result.Item1;
            }
            else
            {
                var result = bLCasesNote.Save();
                status = result.Item1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCaseNotes(long id)
        {
            bool status = false;
            if (id > 0)
                status = db_CasesNote.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
