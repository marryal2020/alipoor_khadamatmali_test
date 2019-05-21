using System.Collections.Generic;
using alipoor_test.Models;


namespace alipoor_test.ViewModels
{
    public class PersonVM
    {

        public long NationalID { get; set; }


        public string FirstMidName { get; set; }


        public string LastName { get; set; }
        public string BirthDate { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public string PersonPicture { get; set; }
    }

}