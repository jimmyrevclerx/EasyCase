using Easycase.Extension;
using Easycase.Extension.Enums;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Client
{
    public class BLClientModel : IDisposable
    {
        public long ID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }
        public int? Prefix { get; set; }
        [DisplayName("Date of birth")]
        public DateTime DOB { get; set; }
        [DisplayName("Martial Status")]
        public long? MartialStatusId { get; set; }
        public string Country { get; set; }
        [DisplayName("Purpose")]
        public long? PurposeId { get; set; }
        public string Citizenship { get; set; }
        public Nullable<long> ImageId { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }

        public string CreatedBy { get; set; }

        /// <summary>
        /// Save data in database
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string,long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.Client
                    {
                        FirstName = this.FirstName,
                        Citizenship = this.Citizenship,
                        Country = this.Country,
                        DOB = this.DOB,
                        Email = this.Email,
                        ImageId = this.ImageId,
                        LastName = this.LastName,
                        MartialStatusId = this.MartialStatusId,
                        Notes = this.Notes,
                        PhoneNo = this.PhoneNo,
                        Prefix = this.Prefix,
                        PurposeId = this.PurposeId,
                        Deleted=false,
                        CreatedBy=this.CreatedBy
                    };
                    DB.Clients.Add(client);
                    DB.SaveChanges();
                    id = client.ID;
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message,0);
            }
        }

        /// <summary>
        /// Update Client
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Update()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.Client
                    {
                        FirstName = this.FirstName,
                        Citizenship = this.Citizenship,
                        Country = this.Country,
                        DOB = this.DOB,
                        Email = this.Email,
                        ImageId = this.ImageId,
                        LastName = this.LastName,
                        MartialStatusId = this.MartialStatusId,
                        Notes = this.Notes,
                        PhoneNo = this.PhoneNo,
                        Prefix = this.Prefix,
                        PurposeId = this.PurposeId,
                        ID = this.ID,
                        Deleted=false,
                        CreatedBy=this.CreatedBy
                    };
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Get all from database
        /// </summary>
        /// <returns></returns>
        public List<BLClientViewModal> GetAll()
        {
            try
            {
                string defaultprofilepath = ConfigurationManager.AppSettings["DefaultProfile"];
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Clients.Where(s=>s.Deleted==false).Select(c => new BLClientViewModal
                    {
                        FirstName = c.FirstName,
                        Citizenship = c.Citizenship,
                        Country = c.Country,
                        DOB = c.DOB==null?DateTime.Now: c.DOB,
                        Email = c.Email,
                        ImageId = c.ImageId,
                        LastName = c.LastName,
                        MartialStatus = c.MartialStatu.Name,
                        Notes = c.Notes,
                        PhoneNo = c.PhoneNo,
                        Prefix = c.Prefix,
                        Purpose = c.PurposeId,
                        ID = c.ID,
                        Name = c.FirstName + " " + c.LastName,
                        ImagePath = c.Image != null ? c.Image.ImagePath : defaultprofilepath
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLClientViewModal>();
            }
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BLClientModel GetById(long id)
        {
            try
            {
                string defaultprofilepath = ConfigurationManager.AppSettings["DefaultProfile"];
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Clients.Where(d => d.ID == id).Select(c => new BLClientModel
                    {
                        FirstName = c.FirstName,
                        Citizenship = c.Citizenship,
                        Country = c.Country,
                        DOB = c.DOB.Value,
                        Email = c.Email,
                        ImageId = c.ImageId,
                        LastName = c.LastName,
                        MartialStatusId = c.MartialStatu.ID,
                        Notes = c.Notes,
                        PhoneNo = c.PhoneNo,
                        Prefix = c.Prefix,
                        PurposeId = c.PurposeId,
                        ID = c.ID,
                        ImagePath = c.Image != null ? c.Image.ImagePath : defaultprofilepath,
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLClientModel();
            }
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Clients.Where(d => d.ID == id).FirstOrDefault();
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

        public bool UpdateDeleteStatus(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Clients.Where(d => d.ID == id).FirstOrDefault();
                    client.Deleted = true;
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
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
