using Easycase.Model.Case;
using Easycase.Model.Company;
using Easycase.Model.Setting;
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
    public class SettingController : BaseController
    {
        BLCompanyDetail companyDetail = new BLCompanyDetail();
        BLSupport bLsupport = new BLSupport();
        BLCaseType bLCaseType = new BLCaseType();
        BLCaseStatus caseStatus = new BLCaseStatus();
        BLServiceFee serviceFee = new BLServiceFee();
        // GET: Setting
        public ActionResult Index()
        {
            ViewData["SuccessCompany"] = TempData["SuccessCompany"];
            ViewData["FailedCompany"] = TempData["FailedCompany"];
            var userId = User.Identity.GetUserId();
            var company = companyDetail.GetById(userId);
            if (company == null)
                company = new BLCompanyDetail();
            var support = bLsupport.GetById(userId);
            if (support == null)
                company.bLSupport = new BLSupport();
            else
                company.bLSupport = support;
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BLCompanyDetail client)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                client.CreatedBy = userId;
                if (client.ID > 0)
                {
                    var result = client.Update();
                    if (result.Item1)
                        TempData["SuccessCompany"] = result.Item2;
                    else
                        TempData["FailedCompany"] = result.Item2;
                }
                else
                {
                    var result = client.Save();
                    if (result.Item1)
                        TempData["SuccessCompany"] = result.Item2;
                    else
                        TempData["FailedCompany"] = result.Item2;
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Support(BLSupport bLSupport)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                bLSupport.CreatedBy = userId;
                if (bLSupport.ID > 0)
                {
                    var result = bLSupport.Update();
                    if (result.Item1)
                        TempData["SuccessCompany"] = result.Item2;
                    else
                        TempData["FailedCompany"] = result.Item2;
                }
                else
                {
                    var result = bLSupport.Save();
                    if (result.Item1)
                        TempData["SuccessCompany"] = result.Item2;
                    else
                        TempData["FailedCompany"] = result.Item2;
                }
                return RedirectToAction("Index");
            }
            return View(bLSupport);
        }
        #region Casetype    
        public ActionResult CaseTypeList()
        {
            var caseTypes = bLCaseType.GetAll();
            return View(caseTypes);
        }

        public ActionResult AddNewCaseType(string name,long id) 
        {
            var userId = User.Identity.GetUserId();
            var result = bLCaseType.Save(name, userId,id);
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialCaseTypeList() 
        {
            var caseTypes = bLCaseType.GetAll();
            return PartialView("_partialCaseTypeList", caseTypes);
        }
        public ActionResult DeleteCaseType(long id)
        {
            bool status = false;
            if (id > 0)
                status = bLCaseType.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CaseStatus    
        public ActionResult CaseStatusList()
        {
            var _caseStatus = caseStatus.GetAll();
            return View(_caseStatus);
        }

        public ActionResult AddNewCaseStatus(string name, long id)
        {
            var userId = User.Identity.GetUserId();
            var result = caseStatus.Save(name, userId, id);
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialCaseStatusList()
        {
            var _caseStatus = caseStatus.GetAll();
            return PartialView("_partialCaseStatusList", _caseStatus);
        }
        public ActionResult DeleteCaseStatus(long id)
        {
            bool status = false;
            if (id > 0)
                status = caseStatus.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Service Fee
        public ActionResult ServiceFee()
        {
            var serviceFees = serviceFee.GetAll();
            return View(serviceFees);
        }
        public ActionResult AddServiceFee(BLAddServiceFee bLAddServiceFee) 
        {
            var userId = User.Identity.GetUserId();
            bLAddServiceFee.CreatedBy = userId;
            var result = bLAddServiceFee.Save();
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialServiceFee()
        {
            var serviceFees = serviceFee.GetAll();
            return PartialView("_partialServiceFeeList", serviceFees);
        }
        public ActionResult DeleteServiceFee(long id)
        {
            bool status = false;
            if (id > 0)
                status = serviceFee.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Client Account
        public ActionResult Account() 
        {
            var userId = User.Identity.GetUserId();
            BLClientAccount bLClientAccount = new BLClientAccount();
            bLClientAccount = bLClientAccount.GetByUserId(userId);
            bLClientAccount.CurrencyArray = JsonConvert.DeserializeObject<string[]>(bLClientAccount.Currency);
            bLClientAccount.BankNameArray = JsonConvert.DeserializeObject<string[]>(bLClientAccount.BankName);
            return View(bLClientAccount);
        }
        [HttpPost]
        public ActionResult Account(BLClientAccount bLClientAccount)
        {
            var userId = User.Identity.GetUserId();
            bLClientAccount.CreatedBy = userId;
            bLClientAccount.BankName = JsonConvert.SerializeObject(bLClientAccount.BankNameArray);
            bLClientAccount.Currency= JsonConvert.SerializeObject(bLClientAccount.CurrencyArray);
            var result= bLClientAccount.Save();
            return Json(result.Item2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PartialClientAccount()
        {
            var userId = User.Identity.GetUserId();
            BLClientAccount bLClientAccount = new BLClientAccount();
            bLClientAccount = bLClientAccount.GetByUserId(userId);
            bLClientAccount.CurrencyArray = JsonConvert.DeserializeObject<string[]>(bLClientAccount.Currency);
            bLClientAccount.BankNameArray = JsonConvert.DeserializeObject<string[]>(bLClientAccount.BankName);
            return PartialView("_partialAccount", bLClientAccount);
        }
        #endregion
        #region TaxInformation
        public ActionResult Tax()
        {
            var userId = User.Identity.GetUserId();
            BLTaxInformation bLTaxInformation = new BLTaxInformation();
            bLTaxInformation = bLTaxInformation.GetByUserId(userId);
            return View(bLTaxInformation);
        }
        [HttpPost]
        public ActionResult Tax(BLTaxInformation bLTaxInformation)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                bLTaxInformation.UserId = userId;
                bLTaxInformation.Save();
            }
            return View(bLTaxInformation);
        }
        #endregion
    }
}