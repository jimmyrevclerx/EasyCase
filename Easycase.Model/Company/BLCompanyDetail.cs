using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Company
{
    public class BLCompanyDetail : IDisposable
    {
        public long ID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [DisplayName("Company Email")]
        public string CompanyEmail { get; set; }
        [Required]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }
        [Required]
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("Province")]
        public string Province { get; set; }
        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Telephone")]
        public string Telephone { get; set; }
        public string Fax { get; set; }
        [Required]
        [DisplayName("Membership Type")]
        public string MembershipType { get; set; }
        public string MembershipNumber { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public BLSupport bLSupport { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.CompanyDetail
                    {
                        FirstName = this.FirstName,
                        Country = this.Country,
                        LastName = this.LastName,
                        CreatedBy = this.CreatedBy,
                        AddressLine1=this.AddressLine1,
                        AddressLine2=this.AddressLine2,
                        City=this.City,
                        CompanyEmail=this.CompanyEmail,
                        CompanyName=this.CompanyName,
                        Fax=this.Fax,
                        MembershipNumber=this.MembershipNumber,
                        MembershipType=this.MembershipType,
                        PostalCode=this.PostalCode,
                        Province=this.Province,
                        Telephone=this.Telephone,
                        UpdatedOn=DateTime.Now
                    };
                    DB.CompanyDetails.Add(client);
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
        public Tuple<bool, string> Update()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.CompanyDetail
                    {
                        FirstName = this.FirstName,
                        Country = this.Country,
                        LastName = this.LastName,
                        CreatedBy = this.CreatedBy,
                        AddressLine1 = this.AddressLine1,
                        AddressLine2 = this.AddressLine2,
                        City = this.City,
                        CompanyEmail = this.CompanyEmail,
                        CompanyName = this.CompanyName,
                        Fax = this.Fax,
                        MembershipNumber = this.MembershipNumber,
                        MembershipType = this.MembershipType,
                        PostalCode = this.PostalCode,
                        Province = this.Province,
                        Telephone = this.Telephone,
                        UpdatedOn = DateTime.Now,
                        ID=this.ID
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
        public BLCompanyDetail GetById(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CompanyDetails.Where(d => d.CreatedBy == id).Select(c => new BLCompanyDetail
                    {
                        FirstName = c.FirstName,
                        Country = c.Country,
                        LastName = c.LastName,
                        CreatedBy = c.CreatedBy,
                        AddressLine1 = c.AddressLine1,
                        AddressLine2 = c.AddressLine2,
                        City = c.City,
                        CompanyEmail = c.CompanyEmail,
                        CompanyName = c.CompanyName,
                        Fax = c.Fax,
                        MembershipNumber = c.MembershipNumber,
                        MembershipType = c.MembershipType,
                        PostalCode = c.PostalCode,
                        Province = c.Province,
                        Telephone = c.Telephone,
                        UpdatedOn = DateTime.Now,
                        ID = c.ID
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCompanyDetail();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
