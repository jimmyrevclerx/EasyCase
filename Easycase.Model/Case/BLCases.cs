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
    public class BLCases : IDisposable
    {
        public long CaseID { get; set; }
        public Nullable<long> ClientId { get; set; }
        [Required]
        [DisplayName("Given Name(s)")]
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public string CaseNumber { get; set; }
        public string Email { get; set; }
        [Required]
        [DisplayName("Case Type")]
        public Nullable<long> CaseTypeId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        [Required]
        [DisplayName("Family Name")]
        public string FamilyName { get; set; }

        public Tuple<bool, string> Save()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = new DataModel.Case
                    {
                        CaseTypeId=this.CaseTypeId,
                        ClientId=this.ClientId,
                        CreatedOn=DateTime.Now,
                        Deleted=false,
                        CaseNumber= this.CaseNumber,
                        CreatedBy=this.CreatedBy
                    };
                    DB.Cases.Add(_case);
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

        public Tuple<bool,string> Update()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.Cases.Where(d => d.ID == this.CaseID).FirstOrDefault();
                    if (_case != null)
                    {
                        _case.CaseTypeId = this.CaseTypeId;
                        _case.ClientId = this.ClientId;
                        _case.CreatedOn = DateTime.Now;
                        _case.CreatedBy = this.CreatedBy;
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }
                }
                return new Tuple<bool, string>(true, Messages.SUCCESS);
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public BLCases GetCaseByClientId(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Clients.Where(d => d.ID == id && d.Deleted==false).Select(c => new BLCases
                    {
                        DOB = c.DOB,
                        Email = c.Email,
                        FamilyName=c.LastName,
                        Name=c.FirstName,
                        ClientId=c.ID
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCases();
            }
        }

        public List<BLCases> GetCaseByCurrentLogin(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Cases.Where(d => d.CreatedBy == id && d.Deleted == false).Select(c => new BLCases
                    {
                        Name = c.CaseNumber+" ("+c.Client.FirstName+" "+c.Client.LastName+"-"+c.Client.Email+")",
                        CaseID=c.ID
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCases>();
            }
        }

        /// <summary>
        /// Get Case by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BLCases GetCaseById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Cases.Where(d => d.ID == id && d.Deleted == false).Select(c => new BLCases
                    {
                        DOB = c.Client.DOB,
                        Email = c.Client.Email,
                        FamilyName = c.Client.LastName,
                        Name = c.Client.FirstName,
                        ClientId = c.ClientId,
                        CaseID=c.ID,
                        CaseTypeId=c.CaseTypeId
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLCases();
            }
        }

        /// <summary>
        /// Delete case
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateDeleteStatus(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.Cases.Where(d => d.ID == id).FirstOrDefault();
                    _case.Deleted = true;
                    DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
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
        public bool UpdateCaseNumber(long id,string caseNumber)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.Cases.Where(d => d.ID == id).FirstOrDefault();
                    _case.CaseNumber = caseNumber;
                    DB.Entry(_case).State = System.Data.Entity.EntityState.Modified;
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

        public bool LinkCase(long caseid, string createdBy)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.LinkCases.Where(d => d.CaseId == caseid && d.UserId==createdBy).FirstOrDefault();
                    if (_case == null)
                    {
                        var _linkcase = new DataModel.LinkCas
                        {
                            CaseId=caseid,
                            UserId=createdBy        
                        };
                        DB.LinkCases.Add(_linkcase);
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

        public List<BLCases> GetLinkCaseByCurrentUser(string id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.LinkCases.Where(d => d.UserId == id).Select(c => new BLCases
                    {
                        CaseNumber=c.Case.CaseNumber,
                        FamilyName=c.Case.Client.LastName,
                        CaseID=c.ID,
                        Name=c.Case.Client.FirstName,
                        Email=c.Case.Client.Email,
                    }).ToList();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLCases>();
            }
        }
        public bool DeleteLinkCase(long caseid)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var _case = DB.LinkCases.Where(d => d.ID == caseid).FirstOrDefault();
                    if (_case != null)
                    {
                        DB.Entry(_case).State = System.Data.Entity.EntityState.Deleted;
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
