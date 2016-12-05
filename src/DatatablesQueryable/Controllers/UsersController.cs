using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatatablesQueryable.DataTables;

namespace DatatablesQueryable.Controllers
{
    public class UsersController : Controller
    {
        public readonly IDataTablesQueryable<User, UserDTO> _usersDataTable;

        public UsersController(IDataTablesQueryable<User, UserDTO> usersDataTable)
        {
            _usersDataTable = usersDataTable;
        }

        [HttpGet]
        [Route("/api/users")]
        public IActionResult GetUsers([ModelBinder(BinderType = typeof(DataTablesModelBinder))] DTParameterModel model)
        {
            var users = new List<User>
            {
                new User() { Id = 1, Name = "Emma" },
                new User() { Id = 2, Name = "Noah" },
                new User() { Id = 3, Name = "Olivia" },
                new User() { Id = 4, Name = "Liam" },
                new User() { Id = 5, Name = "Sophia" },
                new User() { Id = 6, Name = "Mason" },
                new User() { Id = 7, Name = "Ava" },
                new User() { Id = 8, Name = "Jacob" },
                new User() { Id = 9, Name = "Isabella" },
                new User() { Id = 10, Name = "William" }
            };

            var queryable = users.AsQueryable();

            var dataTablesModel = _usersDataTable.Create(model, queryable, user => new UserDTO { id = user.Id, name = user.Name });

            return Ok(dataTablesModel);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserDTO
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
