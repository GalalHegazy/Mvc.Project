using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.BLL.Repositories.Specifications;
using Mvc.Project.DAL.Models;
using Mvc.Project.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepo; (Go To UnitOfWork)
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IUintOfWork _uintOfWork;

        public DepartmentController(/*IDepartmentRepository departmentRepo,*/ //(Go To UnitOfWork)
                                    IWebHostEnvironment env
                                   ,IMapper mapper
                                   ,IUintOfWork uintOfWork)
        {
            //_departmentRepo = departmentRepo;
            _env = env;
            _mapper = mapper;
            _uintOfWork = uintOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var Departments = await _uintOfWork.Repository<Department>().GetAllAsync();
            var MappedDepartments = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Departments);
            return View(MappedDepartments);
        }

        public async Task<IActionResult> Search(string SearchInputDep)
        {
            var Departments = Enumerable.Empty<Department>();

            if (string.IsNullOrEmpty(SearchInputDep))
                Departments = await _uintOfWork.Repository<Department>().GetAllAsync();
            else
            {
                var spec = new DepartmentsSpec(SearchInputDep.ToLower());
                Departments = _uintOfWork.Repository<Department>().SearchByName(spec);
            }

            var MappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Departments);

            return PartialView("PartialViews/DepartmentTablePartial", MappedDepartment);
        }

        //[HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {

            if(ModelState.IsValid)
            {
               var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _uintOfWork.Repository<Department>().Add(MappedDepartment);
                var count = await _uintOfWork.CompleteAsync();
                if (count > 0) 
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            
            var department = await _uintOfWork.Repository<Department>().GetByIdAsync(id.Value);

            if(department is null)
                return NotFound();

            var MappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, MappedDepartment);
        }

        public async Task<IActionResult> Update(int? id)
        {
            return await Details(id,nameof(Update)) ;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DepartmentViewModel departmentVM)
        {
            if(!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _uintOfWork.Repository<Department>().Update(MappedDepartment);
                await  _uintOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                   ModelState.AddModelError(string.Empty,ex.Message);
                else
                    ModelState.AddModelError(string.Empty," An Error During Your Update");
            }

            return View(departmentVM);
        }
        public async Task<IActionResult> Delete(int? id) 
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
               var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _uintOfWork.Repository<Department>().Delete(MappedDepartment);
                int Flag = await _uintOfWork.CompleteAsync();
                if (Flag > 0 )
                    TempData["msg"] = "Department is Deleted";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, " An Error During Your Delete");
            }

            return View(departmentVM);
        }


    }
}
