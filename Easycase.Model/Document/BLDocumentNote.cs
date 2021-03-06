﻿using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Document
{
    public class BLDocumentNote:IDisposable
    {
        public long ID { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<Boolean> Private { get; set; }

        public List<BLDocumentNote> GetByDocumentId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = DB.DocumentNotes.Where(d => d.DocumentId == id).Select(c => new BLDocumentNote
                    {
                        ID = c.ID,
                        Notes = c.Notes,
                        Subject = c.Subject,
                        CreatedOn = c.CreatedOn,
                        CreatedBy = c.CreatedBy,
                        DocumentId=c.DocumentId,
                        Private= c.Private
                    }).ToList();
                    return casenote;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLDocumentNote>();
            }
        }
        public BLDocumentNote GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = DB.DocumentNotes.Where(d => d.ID == id).Select(c => new BLDocumentNote
                    {
                        ID = c.ID,
                        Notes = c.Notes,
                        Subject = c.Subject,
                        CreatedOn = c.CreatedOn,
                        CreatedBy = c.CreatedBy,
                        DocumentId = c.DocumentId,
                        Private = c.Private
                    }).First();
                    return casenote;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLDocumentNote();
            }
        }
        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = new DataModel.DocumentNote
                    {
                        Notes = this.Notes,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Subject = this.Subject,
                        DocumentId=this.DocumentId,
                        Private=this.Private
                    };
                    DB.DocumentNotes.Add(casenote);
                    DB.SaveChanges();
                    id = casenote.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }

        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var casenote = new DataModel.DocumentNote
                    {
                        Notes = this.Notes,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Subject = this.Subject,
                        ID = this.ID,
                        DocumentId=this.DocumentId,
                        Private=this.Private
                    };
                    DB.Entry(casenote).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    id = casenote.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.DocumentNotes.Where(d => d.ID == id).FirstOrDefault();
                    DB.Entry(client).State = System.Data.Entity.EntityState.Deleted;
                    DB.SaveChanges();
                    return true;
                }
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
