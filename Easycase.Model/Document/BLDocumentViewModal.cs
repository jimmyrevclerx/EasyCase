using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Document
{
    public class BLDocumentViewModal
    {
        public long CaseID { get; set; }
        public string CaseNumber { get; set; }
        public List<BLDocument> bLDocuments { get; set; }
    }
}
