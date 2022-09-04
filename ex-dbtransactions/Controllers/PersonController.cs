using ex_dbtransactions.Database;
using ex_dbtransactions.Entities;
using ex_dbtransactions.UoW;
using Microsoft.AspNetCore.Mvc;

namespace ex_dbtransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IUnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Person, Guid> repository;

        public PersonController(IRepository<Person, Guid> repository, IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await repository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            unitOfWork.CreateTransaction();

            for (int i = 0; i < 10; i++)
            {
                var person = new Person
                {
                    Name = $"Pessoa {i} [{DateTime.Now}]",
                    Address = new Entities.ValueObjects.PersonAddress
                    {
                        District = $"District {i}",
                        Number = $"{i}",
                        Street = $"Rua {i}"
                    }
                };

                repository.Add(person);
                unitOfWork.Save();
            }

            unitOfWork.Commit();

            return Ok();
        }
    }
}
