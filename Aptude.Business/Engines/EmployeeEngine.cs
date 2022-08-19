using Aptude.Business.Contracts;
using Aptude.Core.Contracts;
using Aptude.Entities.DTOs;
using Aptude.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aptude.Business.Engines
{
    public class EmployeeEngine : IEmployeeEngine
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public EmployeeEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto model)
        {
            var repo = _dataRepositoryFactory.GetDataRepository<Employee>();

            var employee = new Employee
            {
                Name = model.Name,
                Salary = model.Salary,
                Designation = model.Designation,
                EndDate = model.EndDate,
                StartDate = model.StartDate
            };

            employee.Validates();

            employee = await repo.AddAsync(employee);

            model.Id = employee.Id;

            return model;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var repo = _dataRepositoryFactory.GetDataRepository<Employee>();

            var employee = await repo.GetAsync(x => x, x => x.Id == id);

            if (employee is null)
                throw new Exception("Employee not found!");

            await repo.RemoveAsync(employee);

            return true;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var repo = _dataRepositoryFactory.GetDataRepository<Employee>();

            var employeeDto = await repo.GetAsync(x => x.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Designation = e.Designation,
                Salary = e.Salary,
                StartDate = e.StartDate,
                EndDate = e.EndDate                
            }), x => x.Id == id);

            if (employeeDto is null)
                throw new Exception("Employee not found!");

            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto model)
        {
            var repo = _dataRepositoryFactory.GetDataRepository<Employee>();

            var employee = await repo.GetAsync(x => x, x => x.Id == model.Id);

            if (employee is null)
                throw new Exception("Employee not found!");

            employee.Name = model.Name;
            employee.Salary = model.Salary;
            employee.Designation = model.Designation;
            employee.StartDate = model.StartDate;
            employee.EndDate = model.EndDate;

            employee.Validates();

            await repo.UpdateAsync(employee);

            return model;
        }
    }
}
