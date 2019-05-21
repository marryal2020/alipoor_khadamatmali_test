using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alipoor_test.Models;
using alipoor_test.Data;
using System.Globalization;
using alipoor_test.ViewModels;

namespace alipoor_test.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DBContext _context;

        public PeopleController(DBContext context)
        {
            _context = context;

        }


        // GET: api/People
        [HttpGet]
        public ActionResult<IEnumerable<PersonVM>> GetPersons()
        {
            var list = new List<PersonVM>();
            var persons = _context.Persons.Include(b => b.Addresses).ToList();
            foreach (Person p in persons)
            {

                var result = GetPersianDate(p.BirthDate);

                list.Add(new PersonVM
                {
                    NationalID = p.NationalID,
                    FirstMidName = p.FirstMidName,
                    BirthDate = result,
                    LastName = p.LastName,
                    Addresses = p.Addresses,
                    PersonPicture = p.PersonPicture
                });

            }
            return list;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonVM>> GetPerson(int id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _context.Persons
       .Include(s => s.Addresses)
       .AsNoTracking()
       .FirstOrDefaultAsync(m => m.NationalID == id);

            if (person == null)
            {
                return NotFound();
            }



            var pv = new PersonVM
            {
                NationalID = person.NationalID,
                FirstMidName = person.FirstMidName,
                BirthDate = GetPersianDate(person.BirthDate),
                LastName = person.LastName,
                Addresses = person.Addresses,
                PersonPicture = person.PersonPicture
            };
            return pv;
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {


            foreach (var a in person.Addresses) { _context.Addresses.Add(a); }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetPerson", new { id = person.NationalID }, person);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> PutPerson(int id, Person person)
        {
            if (id != person.NationalID)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;
            foreach (var a in person.Addresses)
            {
                _context.Entry(a).State = EntityState.Modified;

            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return person;
        }


        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            //var person = await _context.Persons.FindAsync(id);

            var person = await _context.Persons
       .Include(s => s.Addresses)
       .AsNoTracking()
       .FirstOrDefaultAsync(m => m.NationalID == id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            foreach (var a in person.Addresses) { _context.Remove(a); }

            await _context.SaveChangesAsync();

            return person;
        }


        public string GetPersianDate(DateTime gdate)
        {


            DateTime date = new DateTime(gdate.Year, gdate.Month, gdate.Day);
            var pc = new PersianCalendar();
            var persianDate = new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));
            var result = persianDate.ToString("yyyy MMM ddd", CultureInfo.GetCultureInfo("fa-Ir"));
            //var result = string.Format("{0}/{1}/{2}", pc.GetYear(persianDate), pc.GetMonth(persianDate), pc.GetDayOfMonth(persianDate));
            return result;
        }
        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.NationalID == id);
        }
    }
}
