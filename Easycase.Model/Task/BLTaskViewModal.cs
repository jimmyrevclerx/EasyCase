using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Task
{
    public class BLTaskViewModal
    {
        public long ID { get; set; }
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

        public IList<BLAssigned> Assigned { get; set; }
    }
}
