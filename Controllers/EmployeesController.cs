using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDecimalSkipToken.Models;

namespace TestDecimalSkipToken.Controllers
{
    [ODataRoutePrefix("Employees")]
    public class EmployeesController : ODataController
    {
        [HttpGet]
        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        public IActionResult Get(ODataQueryOptions<Employee> options)
        {
            var employees = GetEmployees();
            var settings = new ODataQuerySettings { PageSize = 1 };
            var filtered = options.ApplyTo(employees.AsQueryable(), settings);

            return base.Ok(filtered);
        }

        private static Employee[] GetEmployees()
        {
            return new Employee[]
            {
                new Employee { Id = 11 },
                new Employee { Id = 71 },
                new Employee { Id = 123 },
                new Employee { Id = 1 },
                new Employee { Id = 4232 },
            };
        }
    }
}