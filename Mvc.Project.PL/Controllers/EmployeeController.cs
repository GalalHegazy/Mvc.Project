using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.BLL.Repositories.Specifications;
using Mvc.Project.DAL.Models;
using Mvc.Project.PL.Helpers;
using Mvc.Project.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository; //(Go To UnitOfWork)
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IUintOfWork _uintOfWork;

        //private readonly IDepartmentRepository _departmentRepository;// because Create Obj form this Does't need in for All Actions // Go to Action who Need this obj And Inject there, //(Go To UnitOfWork)

        public EmployeeController(/*IEmployeeRepository employeeRepository*/ //(Go To UnitOfWork)
                                 IWebHostEnvironment env
                                /*, IDepartmentRepository departmentRepository*/ // because Create Obj form this Does't need in for All Actions // Go to Action who Need this obj And Inject there, //(Go To UnitOfWork)
                                , IMapper mapper,
                                  IUintOfWork uintOfWork)
        {
            //_employeeRepository = employeeRepository; //(Go To UnitOfWork)
            _env = env;
            _mapper = mapper;
            _uintOfWork = uintOfWork;
            //_departmentRepository = departmentRepository; // because Create Obj form this Does't need in for All Actions // Go to Action who Need this obj And Inject there, //(Go To UnitOfWork)
        }


        public async Task<IActionResult> Index()
        {
            var spec = new EmployeesWithDepartmentsSpec();
            var Employees = await _uintOfWork.Repository<Employee>().GetAllWithSpecAsync(spec);
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            ViewBag.TotalEmp = MappedEmployees.Count();
            ViewBag.TotalEmpMale = MappedEmployees.Where(x=>x.Gander is Gander.Male ).Count();
            ViewBag.TotalEmpFemale = MappedEmployees.Where(x => x.Gander is Gander.Female).Count();
            ViewBag.TotalEmpFull = MappedEmployees.Where(x => x.EmployeeType is EmpType.FullTime).Count();
            ViewBag.TotalEmpPart = MappedEmployees.Where(x => x.EmployeeType is EmpType.PartTime).Count();
            return View(MappedEmployees);
        }
        public async Task<IActionResult> Search(string SearchInput)
        {
            var Employees = Enumerable.Empty<Employee>();
            var spec = new EmployeesWithDepartmentsSpec();
           
            if (string.IsNullOrEmpty(SearchInput))
                Employees = await _uintOfWork.Repository<Employee>().GetAllWithSpecAsync(spec);
            else
            {
                spec = new EmployeesWithDepartmentsSpec(SearchInput.ToLower());
                Employees = _uintOfWork.Repository<Employee>().SearchByName(spec);
            }

            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);

            return PartialView("PartialViews/EmployeeTablePartial", MappedEmployees);
        }
        public IActionResult Create()
        {
            //ViewBag.Departments= _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {

            employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.FormFile, "Images");

            if (!ModelState.IsValid)
                return BadRequest();

            var MappedEmployeeVM = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            _uintOfWork.Repository<Employee>().Add(MappedEmployeeVM);
            var Count = await _uintOfWork.CompleteAsync();
            if (Count > 0)
                return RedirectToAction(nameof(Index));
            else
                 DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");

            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var spec = new EmployeesWithDepartmentsSpec(id);

            var Employee = await _uintOfWork.Repository<Employee>().GetByIdWithSpecAsync(spec);


            if (Employee is null)
                return BadRequest();

            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(Employee);

            if (viewName.Equals("Delete", StringComparison.OrdinalIgnoreCase)|| viewName.Equals("Update", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ImageName"] = Employee.ImageName;
                ViewData["Image"] = Employee.ImageName;
            }

            return View(viewName, MappedEmployee);
        }
        public async Task<IActionResult> Update(int? id)
        {
            return await Details(id, nameof(Update));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeViewModel employeeVM)
        {
            var oldImg = TempData["ImageName"] as string;

            if (!ModelState.IsValid)
                return View(employeeVM);

            if (employeeVM.FormFile is not null)
                employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.FormFile, "Images");

            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            
            try
            {

                _uintOfWork.Repository<Employee>().Update(MappedEmployee);
                var count= await _uintOfWork.CompleteAsync();
                if (count > 0) 
                {
                    DocumentSettings.DeleteFile(oldImg, "Images");
                    return RedirectToAction(nameof(Index));
                }

                return View(employeeVM);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, " An Error During Your Update");
            }

            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, nameof(Delete));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            employeeVM.ImageName = TempData["ImageName"] as string;
            
            var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            try
            {
                _uintOfWork.Repository<Employee>().Delete(MappedEmployee);
                var Count= await _uintOfWork.CompleteAsync(); 

                if(Count > 0)
                {

                  DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                  return RedirectToAction(nameof(Index));

                }

                return View(employeeVM);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error During Your Delete");
            }

            return View(employeeVM);
        }
    }
}
