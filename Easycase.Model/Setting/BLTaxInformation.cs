using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Setting
{
    public class BLTaxInformation : IDisposable
    {
        public long ID { get; set; }
        [Required]
        public Nullable<bool> IsTaxable { get; set; }
        [Required]
        public string TaxType { get; set; }
        [Required]
        public double TaxPercent { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.TaxInformations.Where(d => d.ID == this.ID).FirstOrDefault();
                    if (_case != null)
                    {
                        _case.IsTaxable = this.IsTaxable;
                        _case.UserId = this.UserId;
                        _case.TaxPercent = this.TaxPercent;
                        _case.TaxType = this.TaxType;
                        _case.UpdatedOn = DateTime.Now;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        var _clientAccount = new DataModel.TaxInformation
                        {
                            IsTaxable=this.IsTaxable,
                            UpdatedOn=DateTime.Now,
                            TaxType=this.TaxType,
                            TaxPercent=this.TaxPercent,
                            UserId=this.UserId
                        };
                        DB.TaxInformations.Add(_clientAccount);
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
        public BLTaxInformation GetByUserId(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.TaxInformations.Where(d => d.UserId == id).Select(c => new BLTaxInformation
                    {
                        ID = c.ID,
                        UserId=c.UserId,
                        TaxPercent=c.TaxPercent.Value,
                        TaxType=c.TaxType,
                        IsTaxable=c.IsTaxable,
                        UpdatedOn=c.UpdatedOn
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLTaxInformation();
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
