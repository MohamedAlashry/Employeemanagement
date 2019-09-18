using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    //allpwanonymous is more powerful than authorize
    //allowanonymouse overrides authorize but not vice versa
    public class HomeController : Controller
    {
        private IGenericUnitOfWork _uow;
        private IHostingEnvironment _hostEnvironment;

        public HomeController(IGenericUnitOfWork uow, IHostingEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<EmployeeViewModel> emps = _uow.EmpRepo.GetAll().Select(e => new EmployeeViewModel()
            {
                DeptId = e.Department,
                Email = e.Email,
                Id = e.Id,
                Name = e.Name,
                Photos= _uow.EmpImagesRepo.GetAll().Where(i => i.EmpId == e.Id)?.Select(i => i.PhotoPath).ToList(),
        });
            return View(emps);
        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            EmployeeViewModel homeDetailsViewModel = new EmployeeViewModel();
            Models.Employee Employee = _uow.EmpRepo.GetById(id);
            if (Employee!=null)
            {
                homeDetailsViewModel.Id = Employee.Id;
                     homeDetailsViewModel.DeptId = Employee.Department;
                homeDetailsViewModel.Name = Employee.Name;
                homeDetailsViewModel.Email = Employee.Email;
                homeDetailsViewModel.PageTitle = "Employee Details";

                homeDetailsViewModel.Photos = _uow.EmpImagesRepo.GetAll().Where(i => i.EmpId == Employee.Id)?.Select(i=>i.PhotoPath).ToList();
            }
            else
            {
                HttpContext.Response.StatusCode = 404;
                return View("404NotFound", id);
            }
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            var id = 0;
            if (ModelState.IsValid)
            {
                List<string> images = await SaveImages(employee);
                Models.Employee emp = new Models.Employee()
                {
                    Email = employee.Email,
                    Name = employee.Name,
                    Department = employee.DeptId,

                };
                Models.Employee NewEmp = _uow.EmpRepo.Add(emp);
                bool saved = await _uow.SaveChangesAsync() > 0;
                if (saved && images.Count > 0)
                {
                  
                    await SaveEmployeeImages(images, NewEmp);
                }
                return RedirectToAction("details", new { Id = NewEmp.Id });

            }

            return View();          
           

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Models.Employee employee = _uow.EmpRepo.GetById(id);
            EmployeeViewModel employeeEditViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                DeptId = employee.Department,
                Photos = _uow.EmpImagesRepo.GetAll().Where(i => i.EmpId == employee.Id)?.Select(i => i.PhotoPath).ToList(),
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _uow.EmpRepo.GetById(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.DeptId;
                Employee UpdatedEmp = _uow.EmpRepo.Update(employee);
                bool saved = await _uow.SaveChangesAsync() > 0;              
                if (model.Files != null&&saved)
                {
                    List<string> images = await SaveImages(model);
                    await SaveEmployeeImages(images, UpdatedEmp);
                }             
                return RedirectToAction("details",new {id=UpdatedEmp.Id});
            }

            return View(model);
        }
        [NonAction]
        private async Task SaveEmployeeImages(List<string> images, Models.Employee NewEmp)
        {
            List<Models.EmployeeImages> empImages = _uow.EmpImagesRepo.GetAll().Where(i => i.EmpId == NewEmp.Id).ToList();
            bool deleted = true;
            if (empImages != null && empImages.Any())
            {
                 deleted = await DeleteDBEmpImage(empImages);
            }
            if (deleted)
            {  
                foreach (string item in images)
                {
                    _uow.EmpImagesRepo.Add(new Models.EmployeeImages
                    {
                        EmpId = NewEmp.Id,
                        PhotoPath = item
                    });
                }
                await _uow.SaveChangesAsync();
            }


        }
        [NonAction]
        public async Task<bool> DeleteDBEmpImage(List<Models.EmployeeImages> images)
        {
            foreach (Models.EmployeeImages image in images)
            {
                _uow.EmpImagesRepo.Delete(image.ImageId);
            }
            bool deleted = await _uow.SaveChangesAsync() > 0;
            if (deleted)
            {
                foreach (var image   in images)
                {
                    if (System.IO.File.Exists(Path.Combine(_hostEnvironment.WebRootPath, "images", image.PhotoPath)))
                    {
                        System.IO.File.Delete(Path.Combine(_hostEnvironment.WebRootPath, "images", image.PhotoPath));
                    }  
                }
            }
            return deleted;
        }

        [NonAction]
        private async Task<List<string>> SaveImages(EmployeeViewModel employee)
        {
            List<string> images = new List<string>();
            if (employee.Files != null && employee.Files.Any())
            {
                foreach (Microsoft.AspNetCore.Http.IFormFile file in employee.Files)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    string filename = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string imagePath = Path.Combine(uploadFolder, filename);
                    using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    images.Add(filename);
                }
            }

            return images;
        }


       
    }

    
}
