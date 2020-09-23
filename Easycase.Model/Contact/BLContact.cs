using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Contact
{
    public class BLContact : IDisposable
    {
        public long ID { get; set; }

        [Required]
        [DisplayName("Contact Type")]
        public Nullable<long> ContactTypeId { get; set; }
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        [DisplayName("Work No")]
        public string WorkNo { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        public string Prefix { get; set; }
        [DisplayName("Other No")]
        public string OtherNo { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Status { get; set; }
        public string FaxNo { get; set; }
        [DisplayName("Mailing Address")]
        public string MailingAddress { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.Contact
                    {
                        FirstName = this.FirstName,
                        Email = this.Email,
                        LastName = this.LastName,
                        Notes = this.Notes,
                        Prefix = this.Prefix,
                        CreatedBy = this.CreatedBy,
                        ContactTypeId=this.ContactTypeId,
                        CreatedOn=DateTime.Now,
                        Deleted=false,
                        FaxNo=this.FaxNo,
                        MailingAddress=this.MailingAddress,
                        MobileNo=this.MobileNo,
                        WorkNo=this.WorkNo,
                        OtherNo=this.OtherNo
                    };
                    DB.Contacts.Add(client);
                    DB.SaveChanges();
                    id = client.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public List<BLContactViewModal> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Contacts.Where(s => s.Deleted == false).Select(c => new BLContactViewModal
                    {
                        Name = c.FirstName+" "+c.LastName,
                        Email = c.Email,
                        Notes = c.Notes,
                        Prefix = c.Prefix,
                        ID = c.ID,
                        ContactTypeName=c.ContactType.Name,
                        FaxNo=c.FaxNo,
                        MailingAddress=c.MailingAddress,
                        MobileNo=c.MobileNo,
                        OtherNo=c.OtherNo,
                        WorkNo=c.WorkNo,
                        Status=c.Status
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLContactViewModal>();
            }
        }
        public bool UpdateStatus(string status, long contactId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    //Update Priority
                    var task = DB.Contacts.Where(t => t.ID == contactId).FirstOrDefault();
                    task.Status = status;
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
        public BLContact GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Contacts.Where(d => d.ID == id).Select(c => new BLContact
                    {
                        FirstName = c.FirstName,
                        Email = c.Email,
                        LastName = c.LastName,
                        Notes = c.Notes,
                        Prefix = c.Prefix,
                        ID = c.ID,
                        ContactTypeId=c.ContactTypeId,
                        CreatedBy=c.CreatedBy,
                        CreatedOn=c.CreatedOn,
                        Status=c.Status,
                        Deleted=c.Deleted,
                        FaxNo=c.FaxNo,
                        MailingAddress=c.MailingAddress,
                        MobileNo=c.MobileNo,
                        OtherNo=c.OtherNo,
                        WorkNo=c.WorkNo
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLContact();
            }
        }
        public Tuple<bool, string> Update()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var contact = new DataModel.Contact
                    {
                        FirstName = this.FirstName,
                        Email = this.Email,
                        LastName = this.LastName,
                        Notes = this.Notes,
                        Prefix = this.Prefix,
                        CreatedBy = this.CreatedBy,
                        ContactTypeId = this.ContactTypeId,
                        CreatedOn = DateTime.Now,
                        Deleted = false,
                        FaxNo = this.FaxNo,
                        Status=this.Status,
                        MailingAddress = this.MailingAddress,
                        MobileNo = this.MobileNo,
                        WorkNo = this.WorkNo,
                        OtherNo = this.OtherNo,
                        ID = this.ID,
                    };
                    DB.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                }
                return new Tuple<bool, string>(true, Messages.SUCCESS);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
