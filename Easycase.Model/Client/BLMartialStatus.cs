using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Client
{
    public class BLMartialStatus : IDisposable
    {
        public long ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Get all Purposes
        /// </summary>
        /// <returns></returns>
        public List<BLMartialStatus> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var martialStatus = DB.MartialStatus.Select(c => new BLMartialStatus
                    {
                        ID = c.ID,
                        Name = c.Name,
                    }).ToList();
                    return martialStatus;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLMartialStatus>();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
