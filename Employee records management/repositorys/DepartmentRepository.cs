using EmployeeRecordsManagement.Data;
using EmployeeRecordsManagement.Models;
using EmployeeRecordsManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using EmployeeRecordsManagement.ViewModels;

namespace EmployeeRecordsManagement.repositorys
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<DepartmentViewModel> GetByIdAsync(int id)
        {
            var department = await _dbContext.Department.FindAsync(id);
            var departmentViewModel = new DepartmentViewModel
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name
            };
            return departmentViewModel;
        }

        public async Task<List<DepartmentViewModel>> GetAllAsync()
        {
            var departments = await _dbContext.Department.ToListAsync();
            List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
            foreach (var department in departments)
            {
                var departmentViewModel = new DepartmentViewModel
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name
                };

                departmentViewModels.Add(departmentViewModel);
            }

            return departmentViewModels;

        }

        public async Task AddAsync(DepartmentViewModel department)
        {
            var newDepartment = new Department()
            {
                Name = department.Name
            };
            await _dbContext.Department.AddAsync(newDepartment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DepartmentViewModel departmentUpdated)
        {
            var department = await _dbContext.Department.FindAsync(departmentUpdated.DepartmentId);
            department.Name = departmentUpdated.Name;

            _dbContext.Department.Update(department);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Department department = await _dbContext.Department.FindAsync(Id);
            _dbContext.Department.Remove(department);
            await _dbContext.SaveChangesAsync();
        }
      
       
    }
}
