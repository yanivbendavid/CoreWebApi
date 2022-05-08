using CoreWebApi.Data;
using CoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //[controller] is taking the name of contacts
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext _dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            contact.FullName = updateContactRequest.FullName;
            contact.Email = updateContactRequest.Email;
            contact.Phone = updateContactRequest.Phone;
            contact.Address = updateContactRequest.Address;

            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
