using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Corporate
{
    public class BLCorporatePosition : IDisposable
    {
        public long ID { get; set; }
        public Nullable<long> CorporateJobDetailId { get; set; }
        public string Position { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public List<BLCorporatePosition> GetByCorporateDetailId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporatePositions.Where(d => d.CorporateJobDetailId == id).Select(p => new BLCorporatePosition
                    {
                        ID = p.ID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Position = p.Position,
                        CorporateJobDetailId = p.CorporateJobDetailId
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCorporatePosition>();
            }
        }

        public bool Delete(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporatePositions.Where(d => d.CorporateJobDetailId == id).ToList();
                    foreach (var item in client)
                    {
                        DB.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
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
