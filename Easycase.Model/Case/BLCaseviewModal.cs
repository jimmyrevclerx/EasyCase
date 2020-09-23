using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLCaseviewModal : IDisposable
    {
        public long CaseID { get; set; }
        public Nullable<long> ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CaseTypeName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string FamilyName { get; set; }

        public List<BLCaseviewModal> GetAllCases()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Cases.Where(d=> d.Deleted==false).Select(c => new BLCaseviewModal
                    {
                        DOB = c.Client.DOB,
                        Email = c.Client.Email,
                        FamilyName = c.Client.LastName,
                        Name = c.Client.FirstName,
                        ClientId = c.ID,
                        CaseTypeName=c.CaseType.Name,
                        CaseID=c.ID
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCaseviewModal>();
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
