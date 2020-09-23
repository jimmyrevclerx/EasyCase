using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLLinkDetail
    {
        public List<BLCases> bLCases { get; set; }
        public string CaseNumber { get; set; }
        public long CaseId { get; set; }
    }
}
