using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Corporate
{
    public class BLCorporateJobDetail : IDisposable
    {
        public long ID { get; set; }
        public Nullable<long> ProfileId { get; set; }
        public string LMIANo { get; set; }
        public string PositionDescription { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> PositionAvailable { get; set; }
        public Nullable<System.DateTime> Expiry { get; set; }
        public long? Status { get; set; }
        public string StatusName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Position { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string[] PositionArray { get; set; }
        public string[] LastNameArray { get; set; }
        public string[] FirstNameArray { get; set; }
        public virtual IList<BLCorporatePosition> CorporatePositions { get; set; }
        /// <summary>
        /// Save Corporate Job detail
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    // Savecorporate detail
                    var client = new DataModel.CorporateJobDetail
                    {
                        CreatedOn = DateTime.Now,
                        ApprovedDate=this.ApprovedDate,
                        Expiry=this.Expiry,
                        LMIANo=this.LMIANo,
                        PositionAvailable=this.PositionAvailable,
                        PositionDescription=this.PositionDescription,
                        ProfileId=this.ProfileId,
                        Status=this.Status
                    };
                    DB.CorporateJobDetails.Add(client);
                    id = client.ID;
                    //

                    // save position
                    for (int i = 0; i < this.PositionArray.Length; i++)
                    {
                        var corporateposition = new DataModel.CorporatePosition
                        {
                            CreatedOn = DateTime.Now,
                            CorporateJobDetailId = id,
                            FirstName = this.FirstNameArray[i],
                            LastName = this.LastNameArray[i],
                            Position = this.PositionArray[i]
                        };
                        DB.CorporatePositions.Add(corporateposition);
                    }
                    //
                    DB.SaveChanges();
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public BLCorporateJobDetail GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.CorporateJobDetails.Where(d => d.ProfileId == id).Select(s => new BLCorporateJobDetail
                    {
                        ID = s.ID,
                        ApprovedDate = s.ApprovedDate,
                        Expiry = s.Expiry,
                        LMIANo = s.LMIANo,
                        PositionAvailable = s.PositionAvailable,
                        PositionDescription = s.PositionDescription,
                        Status = s.Status,
                        ProfileId = s.ProfileId,
                        CorporatePositions = s.CorporatePositions.Select(p => new BLCorporatePosition
                        {
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Position = p.Position,
                            ID = p.ID,
                            CorporateJobDetailId = p.CorporateJobDetailId
                        }).ToList()
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCorporateJobDetail();
            }
        }
        public Tuple<bool, string, long> Update()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    // Savecorporate detail
                    var client = new DataModel.CorporateJobDetail
                    {
                        CreatedOn = DateTime.Now,
                        ApprovedDate = this.ApprovedDate,
                        Expiry = this.Expiry,
                        LMIANo = this.LMIANo,
                        PositionAvailable = this.PositionAvailable,
                        PositionDescription = this.PositionDescription,
                        ProfileId = this.ProfileId,
                        Status = this.Status,
                        ID=this.ID
                    };
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
                    id = client.ID;
                    //

                    // save position
                    for (int i = 0; i < this.PositionArray.Length; i++)
                    {
                        var corporateposition = new DataModel.CorporatePosition
                        {
                            CreatedOn = DateTime.Now,
                            CorporateJobDetailId = id,
                            FirstName = this.FirstNameArray[i],
                            LastName = this.LastNameArray[i],
                            Position = this.PositionArray[i]
                        };
                        DB.CorporatePositions.Add(corporateposition);
                    }
                    //
                    DB.SaveChanges();
                }
                return new Tuple<bool, string, long>(true, Messages.SUCCESS, id);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string, long>(false, ex.Message, 0);
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
