using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Task
{
    public class BLMentions
    {
        public long TaskId { get; set; }
        public long ClientId { get; set; }

        public string ClientName { get; set; }
        public string TaskName { get; set; }
    }
}
