using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Client
{
    public class BLClientViewModal
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int? Prefix { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string MartialStatus { get; set; }
        public string Country { get; set; }
        public long? Purpose { get; set; }
        public string Citizenship { get; set; }
        public Nullable<long> ImageId { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
    }
}
