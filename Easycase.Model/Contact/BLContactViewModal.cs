using Easycase.Extension;
using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Contact
{
    public class BLContactViewModal : IDisposable
    {
        public long ID { get; set; }
        public string MobileNo { get; set; }
        public string WorkNo { get; set; }
        public string Email { get; set; }
        public string Prefix { get; set; }
        public string OtherNo { get; set; }
        public string Name { get; set; }
        public string FaxNo { get; set; }
        public string MailingAddress { get; set; }
        public string Notes { get; set; }
        public string ContactTypeName { get; set;}
        public string Status { get; set; }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
