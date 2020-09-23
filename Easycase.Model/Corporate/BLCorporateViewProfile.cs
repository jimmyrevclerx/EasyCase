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
    public class BLCorporateViewProfile : IDisposable
    {
        public long ID { get; set; }
        public string BussinessName { get; set; }
        public string Email { get; set; }
        public string CRA { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string KeyContactPerson { get; set; }
        public string KeyContactPosition { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// get all corporate profile
        /// </summary>
        /// <returns></returns>
        public List<BLCorporateViewProfile> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporateProfileInformations.Where(s => s.Deleted == false).Select(c => new BLCorporateViewProfile
                    {
                        ID = c.ID,
                        Address1=c.Address1,
                        Address2=c.Address2,
                        Address3=c.Address3,
                        BussinessName=c.BussinessName,
                        CRA=c.CRA,
                        Email=c.Email,
                        Fax=c.Fax,
                        KeyContactPerson=c.KeyContactPerson,
                        KeyContactPosition=c.KeyContactPosition,
                        Notes=c.Notes,
                        Phone=c.Phone,
                        Website=c.Website
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCorporateViewProfile>();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
