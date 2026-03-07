using EmployeeRecordsManagement;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecordsManagement.ViewModels
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        public string Name { get; set; }
    }
}