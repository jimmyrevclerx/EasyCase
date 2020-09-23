using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Easycase.Model;
using Easycase.Model.Client;
using System.IO;
using System.Web.Helpers;
using System.Configuration;
using Easycase.Extension.Enums;
using Microsoft.AspNet.Identity;

namespace Easycase.Web.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        BLClientModel db = new BLClientModel();
        BLPurpose BLPurpose = new BLPurpose();
        BLMartialStatus BLMartialStatus = new BLMartialStatus();
        // GET: Client
        public ActionResult Index()
        {
            BLClientModel client = new BLClientModel();
            var clients = client.GetAll();
            return View(clients);
        }

        // GET: Client/Create
        public ActionResult Create(long? id)
        {
            var clientbl = new BLClientModel();
            ViewData["SuccessClient"] = TempData["SuccessClient"];
            ViewData["FailedClient"] = TempData["FailedClient"];

            var martialStatus = BLMartialStatus.GetAll();

            var prefixList = Enum.GetValues(typeof(ClientPrefix))
                .OfType<ClientPrefix>()
                .Select(m => new { Text = m.ToString(), Value = (int)m })
                .ToList();// Get Prefix enum list

            var purpose = BLPurpose.GetAll();

            if (id != null)
                clientbl = db.GetById(id.Value);
            
            ViewBag.MartialStatus = new SelectList(martialStatus, "ID", "Name", clientbl.MartialStatusId);
            ViewBag.Prefix = new SelectList(prefixList, "Value", "Text", clientbl.Prefix);
            ViewBag.Purpose = new SelectList(purpose, "ID", "Name", clientbl.PurposeId);
            return View(clientbl);
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BLClientModel client)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                client.CreatedBy = userId;
                if (client.ID > 0)
                {
                    var result = client.Update();
                    if (result.Item1)
                        TempData["SuccessClient"] = result.Item2;
                    else
                        TempData["FailedClient"] = result.Item2;
                }
                else
                {
                    var result = client.Save();
                    if (result.Item1)
                        TempData["SuccessClient"] = result.Item2;
                    else
                        TempData["FailedClient"] = result.Item2;
                }
                return RedirectToAction("Create");
            }
            var martialStatus = BLMartialStatus.GetAll();

            var prefixList = Enum.GetValues(typeof(ClientPrefix))
                .OfType<ClientPrefix>()
                .Select(m => new { Text = m.ToString(), Value = (int)m })
                .ToList();// Get Prefix enum list

            var purpose = BLPurpose.GetAll();

            ViewBag.MartialStatus = new SelectList(martialStatus, "ID", "Name", client.MartialStatusId);
            ViewBag.Prefix = new SelectList(prefixList, "Value", "Text", client.Prefix);
            ViewBag.Purpose = new SelectList(purpose, "ID", "Name", client.PurposeId);
            return View(client);
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            long returnimageid = 0;
            string imagePath = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["clientimage"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    _imgname = "profile" + _imgname + _ext;
                    var getfolder = "/" + ConfigurationManager.AppSettings["ContentFolder"]+"/"+ ConfigurationManager.AppSettings["ProfileFolder"]+"/";
                    var _comPath = Server.MapPath(getfolder) + _imgname;
                    var path = _comPath;
                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // Save Image
                    var BLImage = new BLImage()
                    {
                        ImageName= _imgname,
                        OriginalName= fileName,
                        ImagePath= getfolder + _imgname
                    };
                    imagePath = getfolder + _imgname;
                    returnimageid = BLImage.Save();
                }
            }
            return Json(new {id= returnimageid,path= imagePath }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(long id)
        {
            bool status = false;
            if (id>0)
                status = db.UpdateDeleteStatus(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
