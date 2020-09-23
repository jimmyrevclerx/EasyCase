using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Task
{
    public class BLTask : IDisposable
    {
        public long ID { get; set; }
        [Required]
        [DisplayName("Task")]
        public string Name { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string TimeDue { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public Nullable<long> RelatedCase { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public long[] Mentions { get; set; }
        public long[] Assigned { get; set; }

        public List<BLTaskViewModal> GetAll()
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var task = DB.Tasks.Where(s => s.Deleted == false&&s.Archived==false).Select(c => new BLTaskViewModal
                    {
                        Notes = c.Notes,
                        Name = c.Name,
                        ID=c.ID,
                        CreatedOn=c.CreatedOn,
                        Deleted=c.Deleted,
                        DueDate=c.DueDate,
                        Priority=c.Priority,
                        RelatedCase=c.RelatedCase,
                        Status=c.Status,
                        TimeDue=c.TimeDue,
                        Assigned = DB.Assigneds.Where(t => t.TaskId == c.ID).Select(s => new BLAssigned
                        {
                            TaskId = s.TaskId,
                            ClientId = s.ClientId,
                            ClientName = s.Client.FirstName + " " + s.Client.LastName,
                            TaskName = s.Task.Name
                        }).ToList()
                    }).ToList();
                    return task;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new List<BLTaskViewModal>();
            }
        }
        public Tuple<bool, string, long> Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var task = new DataModel.Task
                    {
                        Notes = this.Notes,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Deleted=false,
                        DueDate=this.DueDate,
                        Name=this.Name,
                        Priority=this.Priority,
                        RelatedCase=this.RelatedCase,
                        Status=this.Status,
                        TimeDue=this.TimeDue,
                        Archived=false
                    };
                    DB.Tasks.Add(task);
                    DB.SaveChanges();
                    id = task.ID;
                    //Save Mentions and Assigned
                    if (this.Mentions.Length > 0)
                    {
                        foreach (var item in this.Mentions)
                        {
                            var mention = new DataModel.Mention
                            {
                                TaskId = id,
                                ClientId = item
                            };
                            DB.Mentions.Add(mention);
                        }
                    }
                    if (this.Assigned.Length > 0)
                    {
                        foreach (var item in this.Assigned)
                        {
                            var assign = new DataModel.Assigned
                            {
                                TaskId = id,
                                ClientId = item
                            };
                            DB.Assigneds.Add(assign);
                        }
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
        public bool UpdateAssignee(long[] assignedIds,long taskId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var assignees = DB.Assigneds.Where(t => t.TaskId == taskId).ToList();
                    // Remove assignee
                    foreach (var item in assignees)
                    {
                        DB.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                    DB.SaveChanges();
                    //Save Assigned
                    foreach (var item in assignedIds)
                    {
                        var assign = new DataModel.Assigned
                        {
                            TaskId = taskId,
                            ClientId = item
                        };
                        DB.Assigneds.Add(assign);
                    }
                    //
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public bool UpdatePriority(string priority, long taskId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    //Update Priority
                    var task = DB.Tasks.Where(t => t.ID == taskId).FirstOrDefault();
                    task.Priority = priority;
                    DB.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    //
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public bool UpdateStatus(string status, long taskId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    //Update Priority
                    var task = DB.Tasks.Where(t => t.ID == taskId).FirstOrDefault();
                    task.Status = status;
                    DB.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    //
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public bool UpdateTaskName(string name, long taskId)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    //Update Priority
                    var task = DB.Tasks.Where(t => t.ID == taskId).FirstOrDefault();
                    task.Name = name;
                    DB.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    //
                    DB.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return false;
            }
        }
        public bool UpdateDeleteStatus(long id,string action)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Tasks.Where(d => d.ID == id).FirstOrDefault();
                    if(action=="D")
                        client.Deleted = true;
                    else
                        client.Archived = true;
                    DB.Entry(client).State = System.Data.Entity.EntityState.Modified;
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
