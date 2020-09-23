using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Client
{
    public class BLPurpose : IDisposable
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        /// <summary>
        /// Get all Purposes
        /// </summary>
        /// <returns></returns>
        public List<BLPurpose> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var purpose = DB.Purposes.Select(c => new BLPurpose
                    {
                        ID = c.ID,
                        Name = c.Name,
                    }).ToList();
                    return purpose;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLPurpose>();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
