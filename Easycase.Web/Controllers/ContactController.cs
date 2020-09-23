using Easycase.Model.Contact;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        BLContactType contactType = new BLContactType();
        BLContact contact = new BLContact();
        // GET: Contact
        public ActionResult Index()
        {
            var contacts = contact.GetAll();
            return View(contacts);
        }

        // GET: Contact/Create
        public ActionResult Create(long? id)
        {
            var contactbl = new BLContact();
            ViewData["SuccessContact"] = TempData["SuccessContact"];
            ViewData["FailedContact"] = TempData["FailedContact"];
            if (id != null)
                contactbl = contact.GetById(id.Value);

            var contactTypes = contactType.GetAll();
            ViewBag.ContactTypes = new SelectList(contactTypes, "ID", "Name");
            return View(contactbl);
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BLContact bLContact)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                bLContact.CreatedBy = userId;
                if (bLContact.ID > 0)
                {
                    var result = bLContact.Update();
                    if (result.Item1)
                        TempData["SuccessContact"] = result.Item2;
                    else
                        TempData["FailedContact"] = result.Item2;
                }
                else
                {
                    var result = bLContact.Save();
                    if (result.Item1)
                        TempData["SuccessContact"] = result.Item2;
                    else
                        TempData["FailedContact"] = result.Item2;
                }
                return RedirectToAction("Create");
            }
            var contactTypes = contactType.GetAll();
            ViewBag.ContactTypes = new SelectList(contactTypes, "ID", "Name");
            return View(bLContact);
        }

        [HttpPost]
        public ActionResult ChangeStatus(string status, long contactId)
        {
            contact.UpdateStatus(status, contactId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
