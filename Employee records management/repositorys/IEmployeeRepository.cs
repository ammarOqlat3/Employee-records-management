using EmployeeRecordsManagement.Models;
using EmployeeRecordsManagement.ViewModels;

namespace EmployeeRecordsManagement.repositorys
{
    public interface IEmployeeRepository
    {
        Task <EmployeeViewModel> GetByIdAsync(int id);
        IQueryable<EmployeeViewModel> GetAllAsync();
        Task AddAsync(EmployeeViewModel employee);
        Task UpdateAsync(EmployeeViewModel employee);
        Task DeleteAsync(int id);
        Task<List<Department>> GetAllDepartment();


    }
}
