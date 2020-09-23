using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Corporate
{
    public class BLCorporateProfile : IDisposable
    {
        public long ID { get; set; }
        [Required]
        [DisplayName("Bussiness Name")]
        public string BussinessName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        public string CRA { get; set; }
        [Required]
        [DisplayName("Phone")]
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        [Required]
        [DisplayName("Key Contact Person")]
        public string KeyContactPerson { get; set; }
        [Required]
        [DisplayName("Key Contact Position")]
        public string KeyContactPosition { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public IList<BLCorporateJobDetail> CorporateJobDetails { get; set; }

        /// <summary>
        /// Save Corporate Profile Data
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.CorporateProfileInformation
                    {
                        Email = this.Email,
                        Notes = this.Notes,
                        Deleted = false,
                        CreatedBy = this.CreatedBy,
                        Address1=this.Address1,
                        Address2=this.Address2,
                        Address3=this.Address3,
                        BussinessName=this.BussinessName,
                        CRA=this.CRA,
                        CreatedOn=DateTime.Now,
                        Fax=this.Fax,
                        KeyContactPerson=this.KeyContactPerson,
                        KeyContactPosition=this.KeyContactPosition,
                        Phone=this.Phone,
                        Website=this.Website
                    };
                    DB.CorporateProfileInformations.Add(client);
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
        public bool UpdateDeleteStatus(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporateProfileInformations.Where(d => d.ID == id).FirstOrDefault();
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
        public BLCorporateProfile GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporateProfileInformations.Where(d => d.ID == id).Select(c => new BLCorporateProfile
                    {
                        ID = c.ID,
                        Email = c.Email,
                        Notes = c.Notes,
                        CreatedBy = c.CreatedBy,
                        Address1 = c.Address1,
                        Address2 = c.Address2,
                        Address3 = c.Address3,
                        BussinessName = c.BussinessName,
                        CRA = c.CRA,
                        Fax = c.Fax,
                        KeyContactPerson = c.KeyContactPerson,
                        KeyContactPosition = c.KeyContactPosition,
                        Phone = c.Phone,
                        Website = c.Website,
                        CorporateJobDetails = c.CorporateJobDetails.Select(s=>new BLCorporateJobDetail { 
                            ApprovedDate=s.ApprovedDate,
                            Expiry=s.Expiry,
                            LMIANo=s.LMIANo,
                            PositionAvailable=s.PositionAvailable,
                            PositionDescription=s.PositionDescription,
                            Status=s.Status,
                            StatusName=s.CaseStatu!=null? s.CaseStatu.Name:"",
                            ID=s.ID,
                            ProfileId=s.ProfileId,
                            CorporatePositions = s.CorporatePositions.Select(p=>new BLCorporatePosition { 
                            FirstName=p.FirstName,
                            LastName=p.LastName,
                            Position=p.Position,
                            ID=p.ID,
                            CorporateJobDetailId=p.CorporateJobDetailId
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCorporateProfile();
            }
        }
        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.CorporateProfileInformation
                    {
                        Email = this.Email,
                        Notes = this.Notes,
                        Deleted = false,
                        CreatedBy = this.CreatedBy,
                        Address1 = this.Address1,
                        Address2 = this.Address2,
                        Address3 = this.Address3,
                        BussinessName = this.BussinessName,
                        CRA = this.CRA,
                        CreatedOn = DateTime.Now,
                        Fax = this.Fax,
                        KeyContactPerson = this.KeyContactPerson,
                        KeyContactPosition = this.KeyContactPosition,
                        Phone = this.Phone,
                        Website = this.Website,
                        ID=this.ID
                    };
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
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
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
