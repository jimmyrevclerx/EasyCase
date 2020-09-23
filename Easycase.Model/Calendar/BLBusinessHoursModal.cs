using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Calendar
{
    public class BLBusinessHoursModal
    {
        public long BusinessHourId { get; set; }
        public int? ConsultingSession { get; set; }
        public int? WaitTime { get; set; }
        public int? LeadDays { get; set; }
        public IList<BLManageHours> bLManageHours { get; set; }
        public BLHolidays bLHolidays { get; set; }
    }
}
