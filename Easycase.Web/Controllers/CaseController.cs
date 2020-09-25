using Easycase.Extension;
using Easycase.Extension.Enums;
using Easycase.Model.Case;
using Easycase.Model.Client;
using Easycase.Model.Corporate;
using Easycase.Model.Task;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    [HandleError]
    public class CaseController : BaseController
    {
        BLCases casesbl = new BLCases();
        BLCaseviewModal caseviewmodal = new BLCaseviewModal();
        BLClientModel clientModel = new BLClientModel();
        BLCaseType BLCaseType = new BLCaseType();
        BLCaseDetails bLCaseDetails = new BLCaseDetails();
        BLMartialStatus bLMartialStatus = new BLMartialStatus();
        BLAddCaseDetailModal addCaseDetailModal = new BLAddCaseDetailModal();
        BLCasesNote db_CasesNote = new BLCasesNote();
        // GET: Case
        public ActionResult Index()
        {
            var modal = caseviewmodal.GetAllCases();
            return View(modal);
        }

        // GET: Case/Create
        public ActionResult Create(long? id)
        {
            ViewData["SuccessCase"] = TempData["SuccessCase"];
            ViewData["FailedCase"] = TempData["FailedCase"];

            var modal = new BLCases();
            if (id > 0)
                modal = modal.GetCaseByClientId(id.Value);

            var cases = BLCaseType.GetAll();
            ViewBag.CaseType = new SelectList(cases, "ID", "Name");
            return View(modal);
        }

        // POST: Case/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BLCases blCases)
        {
            if (ModelState.IsValid)
            {
                string lastName = string.Empty;
                var userId = User.Identity.GetUserId();
                if (blCases.ClientId > 0)
                {
                    var clientbl = clientModel.GetById(blCases.ClientId.Value);
                    clientbl.CreatedBy = userId;
                    clientbl.FirstName = blCases.Name;
                    clientbl.LastName = blCases.FamilyName;
                    clientbl.DOB = blCases.DOB.Value;
                    clientbl.Email = blCases.Email;
                    lastName = clientbl.LastName;
                    var result_client = clientbl.Update();
                }
                else
                {
                    var client = new BLClientModel()
                    {
                        FirstName = blCases.Name,
                        LastName = blCases.FamilyName,
                        DOB = blCases.DOB.Value,
                        Email = blCases.Email,
                        CreatedBy = userId
                    };
                    var result_client = client.Save();
                    blCases.ClientId = result_client.Item3;
                }
                blCases.CreatedBy = userId;
                blCases.CaseNumber = Common.GenerateRandom(6) + lastName;
                var result = blCases.Save();
                if (result.Item1)
                    TempData["SuccessCase"] = result.Item2;
                else
                    TempData["FailedCase"] = result.Item2;
                return RedirectToAction("Create");
            }
            var cases = BLCaseType.GetAll();
            ViewBag.CaseType = new SelectList(cases, "ID", "Name");
            return View(blCases);
        }

        // GET: Case/Edit
        public ActionResult Edit(long? id)
        {
            ViewData["SuccessCase"] = TempData["SuccessCase"];
            ViewData["FailedCase"] = TempData["FailedCase"];

            var modal = new BLCases();
            if (id > 0)
                modal = modal.GetCaseById(id.Value);

            var cases = BLCaseType.GetAll();
            ViewBag.CaseType = new SelectList(cases, "ID", "Name");
            return View(modal);
        }

        // POST: Case/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BLCases blCases)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                if (blCases.ClientId > 0)
                {
                    var clientbl = clientModel.GetById(blCases.ClientId.Value);
                    clientbl.FirstName = blCases.Name;
                    clientbl.LastName = blCases.FamilyName;
                    clientbl.DOB = blCases.DOB.Value;
                    clientbl.Email = blCases.Email;
                    var result_client = clientbl.Update();
                }
                blCases.CreatedBy = userId;
                var result = blCases.Update();
                if (result.Item1)
                    TempData["SuccessCase"] = result.Item2;
                else
                    TempData["FailedCase"] = result.Item2;
                return RedirectToAction("Edit", "Case", blCases.CaseID);
            }
            var cases = BLCaseType.GetAll();
            ViewBag.CaseType = new SelectList(cases, "ID", "Name");
            return View(blCases);
        }

        /// <summary>
        /// Delete Case
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(long id)
        {
            bool status = false;
            if (id > 0)
                status = casesbl.UpdateDeleteStatus(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(long id)
        {
            var result = bLCaseDetails.GetCaseDetailById(id);
            if (result != null)
                result.Age = Common.CalculateAge(result.DOB);
            if (result == null)
                result = new BLCaseDetails();

            ViewBag.CaseId = id;
            return View(result);
        }

        public ActionResult UpdateCaseNumber(long id, string casenumber)
        {
            var result = casesbl.UpdateCaseNumber(id, casenumber);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDetail(long id)
        {
            if (id <= 0)
                return RedirectToAction("index");

            var martialStatus = bLMartialStatus.GetAll();
            var prefixList = Enum.GetValues(typeof(ClientPrefix))
                .OfType<ClientPrefix>()
                .Select(m => new { Text = m.ToString(), Value = (int)m })
                .ToList();// Get Prefix enum list
            var bLAddCaseDetailModal = addCaseDetailModal.GetById(id);
            if (bLAddCaseDetailModal == null)
                bLAddCaseDetailModal = new BLAddCaseDetailModal();
            ViewBag.Prefix = new SelectList(prefixList, "Value", "Text", bLAddCaseDetailModal.Prefix);
            ViewBag.MartialStatus = new SelectList(martialStatus, "ID", "Name", bLAddCaseDetailModal.MartialStatusId);
            return View(bLAddCaseDetailModal);
        }
        [HttpPost]
        public ActionResult EditDetail(BLAddCaseDetailModal bLAddCaseDetailModal)
        {
            try
            {
                // update client other info
                var clientInfo = clientModel.GetById(bLAddCaseDetailModal.ClientId.Value);
                clientInfo.FirstName = bLAddCaseDetailModal.FirstName;
                clientInfo.LastName = bLAddCaseDetailModal.LastName;
                clientInfo.Email = bLAddCaseDetailModal.Email;
                clientInfo.Prefix = bLAddCaseDetailModal.Prefix;
                clientInfo.PhoneNo = bLAddCaseDetailModal.Phone;
                clientInfo.Country = bLAddCaseDetailModal.CountryOfResidence;
                clientInfo.Citizenship = bLAddCaseDetailModal.CountryOfCitizenship;
                clientInfo.Update();

                // save update details
                if (bLAddCaseDetailModal.ProfileInformationID > 0)
                {
                    var result = bLAddCaseDetailModal.Update();
                }
                else
                {
                    var result = bLAddCaseDetailModal.Save();
                }
                return RedirectToAction("educationdetail", new { id = bLAddCaseDetailModal.CaseID });
            }
            catch (Exception)
            {
                var martialStatus = bLMartialStatus.GetAll();
                var prefixList = Enum.GetValues(typeof(ClientPrefix))
                    .OfType<ClientPrefix>()
                    .Select(m => new { Text = m.ToString(), Value = (int)m })
                    .ToList();// Get Prefix enum list
                ViewBag.Prefix = new SelectList(prefixList, "Value", "Text", bLAddCaseDetailModal.Prefix);
                ViewBag.MartialStatus = new SelectList(martialStatus, "ID", "Name", bLAddCaseDetailModal.MartialStatusId);
                return View(bLAddCaseDetailModal);
            }
        }

        public ActionResult EducationDetail(long id)
        {

            var educationDetails = new BLEducationDetails();
            var modal = educationDetails.GetByCaseId(id);
            var addEducationModal = new BLAddEducationModal();
            if (modal != null)
            {
                addEducationModal.EducationHistory = new EducationHistory();
                addEducationModal.EducationHistory = JsonConvert.DeserializeObject<EducationHistory>(modal.EducationHistory);
                addEducationModal.CaseId = modal.CaseId;
                addEducationModal.SpouseEducation = new BLSpouseEducation();
                addEducationModal.SpouseEducation = JsonConvert.DeserializeObject<BLSpouseEducation>(modal.SpouseEducation);
                addEducationModal.CICEnglishExam = modal.CICEnglishExam;
                addEducationModal.CICEnglishExamSpouse = modal.CICEnglishExamSpouse;
                addEducationModal.ECACompleted = modal.ECACompleted;
                addEducationModal.EducationId = modal.ID;
            }
            else
            {
                addEducationModal.CaseId = id;
            }
            return View(addEducationModal);
        }
        [HttpPost]
        public ActionResult EducationDetail(BLAddEducationModal bLAddEducationModal)
        {
            BLEducationDetails bLEducationDetails = new BLEducationDetails()
            {
                CaseId = bLAddEducationModal.CaseId,
                SpouseEducation = JsonConvert.SerializeObject(bLAddEducationModal.SpouseEducation),
                EducationHistory = JsonConvert.SerializeObject(bLAddEducationModal.EducationHistory),
                CICEnglishExam = bLAddEducationModal.CICEnglishExam,
                CICEnglishExamSpouse = bLAddEducationModal.CICEnglishExamSpouse,
                ECACompleted = bLAddEducationModal.ECACompleted,
                ID = bLAddEducationModal.EducationId
            };
            var result = bLEducationDetails.Save();
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OtherDetails(long id)
        {
            BLOtherDetail bLOtherDetail = new BLOtherDetail();
            var modal = bLOtherDetail.GetByCaseId(id);
            if (modal == null)
            {
                modal = new BLOtherDetail();
                modal.CaseId = id;
            }
            else
            {
                modal.OtherEmployementHistory= JsonConvert.DeserializeObject<OtherEmployementHistory>(modal.EmployementHistory);
                modal.SpouseEmployementHistoryModal = JsonConvert.DeserializeObject<OtherEmployementHistory>(modal.SpouseEmployementHistory);
            }
            return View(modal);
        }
        [HttpPost]
        public ActionResult OtherDetails(BLOtherDetail bLOtherDetail)
        {
            bLOtherDetail.EmployementHistory = JsonConvert.SerializeObject(bLOtherDetail.OtherEmployementHistory);
            bLOtherDetail.SpouseEmployementHistory = JsonConvert.SerializeObject(bLOtherDetail.SpouseEmployementHistoryModal);
            var result = bLOtherDetail.Save();
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RelatedCase(long id)
        {
            BLLinkDetail bLLinkDetail = new BLLinkDetail();
            var result = bLCaseDetails.GetCaseDetailById(id);
            if (result == null)
                result = new BLCaseDetails();

            var userId = User.Identity.GetUserId();
            var cases = casesbl.GetCaseByCurrentLogin(userId);
            ViewBag.CaseList = new SelectList(cases, "CaseID", "Name");
            ViewBag.CaseId = id;
            var linkcase = casesbl.GetLinkCaseByCurrentUser(userId);
            bLLinkDetail.CaseId = id;
            bLLinkDetail.CaseNumber = result.CaseNumber;
            bLLinkDetail.bLCases = linkcase;
            return View(bLLinkDetail);
        }

        public ActionResult LinkCase(long caseid) 
        {
            var userId = User.Identity.GetUserId();
            casesbl.LinkCase(caseid, userId);
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRelatedCase(long caseid)
        {
            casesbl.DeleteLinkCase(caseid);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialRelatedCase(long id)
        {
            BLLinkDetail bLLinkDetail = new BLLinkDetail();
            var result = bLCaseDetails.GetCaseDetailById(id);
            if (result == null)
                result = new BLCaseDetails();

            var userId = User.Identity.GetUserId();
            ViewBag.CaseId = id;
            var linkcase = casesbl.GetLinkCaseByCurrentUser(userId);
            bLLinkDetail.CaseId = id;
            bLLinkDetail.CaseNumber = result.CaseNumber;
            bLLinkDetail.bLCases = linkcase;
            return PartialView("_partialRelatedCase", bLLinkDetail);
        }

        public ActionResult OpenCaseNote(long id)
        {
            var casenote = db_CasesNote.GetByCaseId(id);
            return PartialView("_partialCaseNote", casenote);
        }
        public ActionResult OpenTaskNote(long id)
        {
            BLTaskNote taskNote = new BLTaskNote();
            var Clients = clientModel.GetAll();
            ViewBag.Clients = new SelectList(Clients, "ID", "Name");
            var casenote = taskNote.GetByCaseId(id);
            return PartialView("_partialTaskNote", casenote);
        }
    }
}
