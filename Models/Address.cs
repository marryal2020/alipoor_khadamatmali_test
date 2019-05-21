using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alipoor_test.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public long NationalID { get; set; }
        public string AddressLine { get; set; }
    }
}
