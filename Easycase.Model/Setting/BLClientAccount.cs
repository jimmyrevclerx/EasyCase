using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Setting
{
    public class BLClientAccount : IDisposable
    {
        public long ID { get; set; }
        public string BankName { get; set; }
        public string Currency { get; set; }
        public string CreatedBy { get; set; }

        public string[] BankNameArray { get; set; }
        public string[] CurrencyArray { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.ClientAccounts.Where(d => d.ID == this.ID).FirstOrDefault();
                    if (_case != null)
                    {
                        _case.BankName = this.BankName;
                        _case.CreatedBy = this.CreatedBy;
                        _case.Currency = this.Currency;
                        _case.CreatedOn = DateTime.Now;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        var _clientAccount = new DataModel.ClientAccount
                        {
                            BankName = this.BankName,
                            Currency = this.Currency,
                            CreatedOn = DateTime.Now,
                            CreatedBy = this.CreatedBy,
                        };
                        DB.ClientAccounts.Add(_clientAccount);
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

        public BLClientAccount GetByUserId(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.ClientAccounts.Where(d => d.CreatedBy == id).Select(c => new BLClientAccount
                    {
                        ID = c.ID,
                        BankName=c.BankName,
                        CreatedBy=c.CreatedBy,
                        CreatedOn=c.CreatedOn,
                        Currency=c.Currency
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLClientAccount();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
