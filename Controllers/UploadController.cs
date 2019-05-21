using System;
using System.IO;
using System.Threading.Tasks;
using alipoor_test.Models;
using alipoor_test.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alipoor_test.Controllers
{
    [Route("/api/Upload")]
    public class filesController : Controller
    {

        private readonly IHostingEnvironment host;
        private readonly DBContext _context;
        public filesController(IHostingEnvironment host, DBContext context)
        {
            this._context = context;
            this.host = host;
        }



        [HttpPost]

        public async Task<ActionResult<Person>> Post(int nationalID, string firstMidName, string lastName, DateTime birthDate, IFormFile file, Address[] addresses)
        {



            string tofilebase64 = null;
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    tofilebase64 = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }


            foreach (var a in addresses) { _context.Addresses.Add(a); }
            var person = new Person
            {
                NationalID = nationalID,
                FirstMidName = firstMidName,
                LastName = lastName,
                BirthDate = birthDate,
                PersonPicture = tofilebase64
            };
            _context.Persons.Add(person);

            await _context.SaveChangesAsync();
            return person;

        }



    }
}