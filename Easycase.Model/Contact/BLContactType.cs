using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Contact
{
    public class BLContactType:IDisposable
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Deleted { get; set; }

        public List<BLContactType> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ContactTypes.Select(c => new BLContactType
                    {
                        ID = c.ID,
                        Name = c.Name,
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLContactType>();
            }
        }

        

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
