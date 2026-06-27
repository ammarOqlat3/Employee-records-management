using EmployeeRecordsManagement.Models;
using EmployeeRecordsManagement.repositorys;
using EmployeeRecordsManagement.Models;
using EmployeeRecordsManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EmployeeRecordsManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int pageNumber)
        {
            var employees= _employeeRepository.GetAllAsync();
            ViewData["CurrentSort"] = sortOrder;
            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            if (!string.IsNullOrEmpty(searchString))
            {
                var normalizedSearch = searchString.ToLower();
                employees = employees.Where(e =>
                    e.FirstName.ToLower().Contains(normalizedSearch) ||
                    e.LastName.ToLower().Contains(normalizedSearch));
            }
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateOfBirthSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["IsActiveSortParam"] = sortOrder == "isactive_asc" ? "isactive_desc" : "isactive_asc";

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;

                case "date_asc":
                    employees = employees.OrderBy(s => s.DateOfBirth) ;
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "isactive_desc":
                    employees = employees.OrderByDescending(e => e.IsActive);
                    break;
                case "isactive_asc":
                    employees = employees.OrderBy(e => e.IsActive);
                    break;

                default:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
            }
            if(pageNumber<1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            return View(await PaginatedList<EmployeeViewModel>.CreateAsync(employees,pageNumber,pageSize));


        }
        public async Task<IActionResult> Add()
        {
           var departments=await _employeeRepository.GetAllDepartment();
            ViewBag.departments = new SelectList(departments, "DepartmentId", "Name");
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _employeeRepository.AddAsync(model);
            return RedirectToAction("Index","employee");
        }

        [HttpGet]
        public async Task<IActionResult>Edit(int id)
        {
            //fetch department
            var departments = await _employeeRepository.GetAllDepartment();
            ViewBag.departments = new SelectList(departments, "DepartmentId", "Name");


            //fetch employee details
            var employee=await _employeeRepository.GetByIdAsync(id);

            return View(employee);
        }
        [HttpPost]
        public async Task< IActionResult> Edit(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            await _employeeRepository.UpdateAsync(employee);
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            await _employeeRepository.DeleteAsync(id);

            return RedirectToAction("Index", "Employee");

        }


    }
}
