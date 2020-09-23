using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Document
{
    public class BLDocument : IDisposable
    {
        public long ID { get; set; }
        public Nullable<long> LinkId { get; set; }
        public string LinkTable { get; set; }
        public string OriginalName { get; set; }
        public string FileName { get; set; }
        public string DocPath { get; set; }
        public string FolderName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CretedBy { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string Status { get; set; }

        public long Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var image = new DataModel.Document
                    {
                        OriginalName = this.OriginalName,
                        CreatedOn = DateTime.Now,
                        CretedBy=this.CretedBy,
                        Deleted=false,
                        DocPath=this.DocPath,
                        FileName=this.FileName,
                        FolderName=this.FolderName,
                        LinkId=this.LinkId,
                        LinkTable=this.LinkTable,
                        Status="In Review"
                    };
                    DB.Documents.Add(image);
                    DB.SaveChanges();
                    id = image.ID;
                }
                return id;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return 0;
            }
        }

        public BLDocument GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Documents.Where(d => d.ID == id).Select(c => new BLDocument
                    {
                        ID = c.ID,
                        OriginalName = c.OriginalName,
                        LinkId=c.LinkId,
                        LinkTable=c.LinkTable,
                        FolderName=c.FolderName,
                        FileName=c.FileName,
                        DocPath=c.DocPath,
                        CreatedOn=c.CreatedOn,
                        CretedBy=c.CretedBy,
                        Deleted=c.Deleted,
                        Status=c.Status
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLDocument();
            }
        }

        public List<BLDocument> GetAll(string createdby)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Documents.Where(d => d.CretedBy == createdby && d.Deleted==false).Select(c => new BLDocument
                    {
                        ID = c.ID,
                        OriginalName = c.OriginalName,
                        LinkId = c.LinkId,
                        LinkTable = c.LinkTable,
                        FolderName = c.FolderName,
                        FileName = c.FileName,
                        DocPath = c.DocPath,
                        CreatedOn = c.CreatedOn,
                        CretedBy = c.CretedBy,
                        Deleted = c.Deleted,
                        Status=c.Status
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLDocument>();
            }
        }

        public bool UpdateFileName(string fileName, long docId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    //Update FileName
                    var task = DB.Documents.Where(t => t.ID == docId).FirstOrDefault();
                    task.OriginalName = fileName;
                    DB.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    //
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }

        public bool UpdateDeleteStatus(long docId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var task = DB.Documents.Where(t => t.ID == docId).FirstOrDefault();
                    task.Deleted = true;
                    DB.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
