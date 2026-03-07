using EmployeeRecordsManagement.ViewModels;
using EmployeeRecordsManagement.Models;
using EmployeeRecordsManagement.ViewModels;


namespace EmployeeRecordsManagement.repositorys
{
    public interface IDepartmentRepository
    {
        Task<DepartmentViewModel> GetByIdAsync(int id);
        Task<List<DepartmentViewModel>> GetAllAsync();
        Task AddAsync(DepartmentViewModel department);
        Task UpdateAsync(DepartmentViewModel department);
        Task DeleteAsync(int Id);
    }
}
