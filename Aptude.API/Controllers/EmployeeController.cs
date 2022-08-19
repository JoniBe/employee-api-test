using Aptude.Business.Contracts;
using Aptude.Core.Contracts;
using Aptude.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aptude.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IBusinessEngineFactory _businessEngineFactory;

        public EmployeeController(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        [HttpPost]
        [Route("create")]
        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            var engine = _businessEngineFactory.GetBusinessEngine<IEmployeeEngine>();

            var result = await engine.CreateEmployeeAsync(employeeDto);

            return result;
        }

        [HttpPut]
        [Route("update")]
        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            var engine = _businessEngineFactory.GetBusinessEngine<IEmployeeEngine>();

            var result = await engine.UpdateEmployeeAsync(employeeDto);

            return result;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<bool> DeleteEmployee(int id)
        {
            var engine = _businessEngineFactory.GetBusinessEngine<IEmployeeEngine>();

            var result = await engine.DeleteEmployeeAsync(id);

            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var engine = _businessEngineFactory.GetBusinessEngine<IEmployeeEngine>();

            var result = await engine.GetEmployeeByIdAsync(id);

            return result;
        }
    }
}
