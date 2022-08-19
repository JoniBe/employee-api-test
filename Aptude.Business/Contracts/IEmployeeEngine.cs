using Aptude.Core.Contracts;
using Aptude.Entities.DTOs;
using System.Threading.Tasks;

namespace Aptude.Business.Contracts
{
    public interface IEmployeeEngine : IBusinessEngine
    {
        public Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto model);
        public Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        public Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto model);
        public Task<bool> DeleteEmployeeAsync(int id);
    }
}
