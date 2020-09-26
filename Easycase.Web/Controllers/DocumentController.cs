using Easycase.Extension;
using Easycase.Model.Case;
using Easycase.Model.Document;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Easycase.Web.Controllers
{
    [Authorize]
    [HandleError]
    public class DocumentController : BaseController
    {
        BLCaseDetails bLCaseDetails = new BLCaseDetails();
        BLDocument documents = new BLDocument();
        BLDocumentNote documentNote = new BLDocumentNote();
        // GET: Document
        public ActionResult Index(long id)
        {
            var userId = User.Identity.GetUserId();
            BLDocumentViewModal modal = new BLDocumentViewModal();
            var result = bLCaseDetails.GetCaseDetailById(id);
            if (result != null)
                result.Age = Common.CalculateAge(result.DOB);
            if (result == null)
                result = new BLCaseDetails();

            ViewBag.CaseId = id;
            modal.CaseID = id;
            modal.CaseNumber = result.CaseNumber;
            modal.bLDocuments = documents.GetAll(userId);
            return View(modal);
        }

        [HttpPost]
        public JsonResult UploadFile(BLDocument modal)
        {
            string _imgname = string.Empty;
            long returnimageid = 0;
            string imagePath = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["uploadDocument"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    _imgname = "docuemnt" + _imgname + _ext;
                    var getfolder = string.Empty;
                    if (string.IsNullOrEmpty(modal.FolderName))
                        getfolder = "/" + ConfigurationManager.AppSettings["ContentFolder"] + "/Documents/";
                    else
                    {
                        getfolder = "/" + ConfigurationManager.AppSettings["ContentFolder"] + "/Documents/" + modal.FolderName + "/";
                        if (!Directory.Exists(Server.MapPath(getfolder))){
                            Directory.CreateDirectory(Server.MapPath(getfolder));
                        }
                    }
                    var _comPath = Server.MapPath(getfolder) + _imgname;
                    var path = _comPath;
                    // Saving document in Original Mode
                    pic.SaveAs(path);

                    var userId = User.Identity.GetUserId();
                    // Save Document
                    var bLDocument = new BLDocument()
                    {
                        OriginalName = fileName,
                        CretedBy = userId,
                        DocPath = getfolder + _imgname,
                        FileName = _imgname,
                        FolderName = modal.FolderName == null ? "Documents" : modal.FolderName,
                        LinkId = modal.LinkId,
                        LinkTable = modal.LinkTable,
                    };
                    imagePath = getfolder + _imgname;
                    returnimageid = bLDocument.Save();
                }
            }
            return Json(new { id = returnimageid, path = imagePath }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialDocument()
        {
            var userId = User.Identity.GetUserId();
            BLDocumentViewModal modal = new BLDocumentViewModal();
            modal.bLDocuments = documents.GetAll(userId);
            return PartialView("_partialDocuments", modal);
        }

        public ActionResult ChangeFileName(long id,string fileName) 
        {
            var updateName = documents.UpdateFileName(fileName, id);
            return Json(updateName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(long id)
        {
            var updateName = documents.UpdateDeleteStatus(id);
            return Json(updateName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenDocumentNote(long id)
        {
            var docnote = documentNote.GetByDocumentId(id);
            return PartialView("_partialDocumentNote", docnote);
        }

        [HttpPost]
        public ActionResult SaveDocumentNote(BLDocumentNote bLDocumentNote)
        {
            bool status = false;
            if (bLDocumentNote.ID > 0)
            {
                var result = bLDocumentNote.Update();
                status = result.Item1;
            }
            else
            {
                var result = bLDocumentNote.Save();
                status = result.Item1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDocumentNotes(long id)
        {
            bool status = false;
            if (id > 0)
                status = documentNote.Delete(id);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocumentNoteById(long id)
        {
            var result = documentNote.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}