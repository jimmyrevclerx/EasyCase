using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Setting
{
    public class BLAddServiceFee
    {
        public long[] ID { get; set; }
        public string[] ServiceName { get; set; }
        public string[] IsExpense { get; set; }
        public string[] Description { get; set; }
        public string[] AdditionalInfo { get; set; }
        public Nullable<double>[] Fee { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    for (int i = 0; i < this.ServiceName.Count(); i++)
                    {
                        var servicefee = new DataModel.ServiceFee
                        {
                            CreatedOn = DateTime.Now,
                            CreatedBy = this.CreatedBy,
                            ServiceName = this.ServiceName[i],
                            IsExpense = this.IsExpense[i],
                            Fee = this.Fee[i],
                            Description = this.Description[i],
                            Deleted = false,
                            AdditionalInfo = this.AdditionalInfo[i],
                            ID = this.ID[i],
                        };
                        if (servicefee.ID == 0)
                        {
                            if (!string.IsNullOrEmpty(servicefee.ServiceName) && servicefee.Fee > 0)
                                DB.ServiceFees.Add(servicefee);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(servicefee.ServiceName) && servicefee.Fee > 0)
                                DB.Entry(servicefee).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
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
    }
}
