using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Company
{
    public class BLSupport
    {
        public long ID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<long> ImageId { get; set; }
        public string Email { get; set; }
        public string Greeting { get; set; }
        public string PhoneNo { get; set; }
        public string FacebookUrl { get; set; }
        public string Linkdin { get; set; }
        public string WebsiteUrl { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string ImagePath { get; set; }

        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = new DataModel.Support
                    {
                        UpdatedOn = DateTime.Now,
                        CreatedBy=this.CreatedBy,
                        Email=this.Email,
                        Greeting=this.Greeting,
                        FacebookUrl=this.FacebookUrl,
                        ImageId=this.ImageId,
                        Linkdin=this.Linkdin,
                        PhoneNo=this.PhoneNo,
                        WebsiteUrl=this.WebsiteUrl
                    };
                    DB.Supports.Add(client);
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
                    var client = new DataModel.Support
                    {
                        UpdatedOn = DateTime.Now,
                        CreatedBy = this.CreatedBy,
                        Email = this.Email,
                        Greeting = this.Greeting,
                        FacebookUrl = this.FacebookUrl,
                        ImageId = this.ImageId,
                        Linkdin = this.Linkdin,
                        PhoneNo = this.PhoneNo,
                        WebsiteUrl = this.WebsiteUrl,
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
        public BLSupport GetById(string id)
        {
            try
            {
                string defaultprofilepath = ConfigurationManager.AppSettings["DefaultProfile"];
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Supports.Where(d => d.CreatedBy == id).Select(c => new BLSupport
                    {
                        UpdatedOn = DateTime.Now,
                        CreatedBy = c.CreatedBy,
                        Email = c.Email,
                        Greeting = c.Greeting,
                        FacebookUrl = c.FacebookUrl,
                        ImageId = c.ImageId,
                        Linkdin = c.Linkdin,
                        PhoneNo = c.PhoneNo,
                        WebsiteUrl = c.WebsiteUrl,
                        ID = c.ID,
                        ImagePath= c.Image != null ? c.Image.ImagePath : defaultprofilepath,
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLSupport();
            }
        }
    }
}
