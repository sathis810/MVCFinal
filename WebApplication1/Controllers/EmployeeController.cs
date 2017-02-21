using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public string GetString()
        {
            return "Hello World is old now. It’s time for wassup bro ;)";
        }

        [NonAction]
        public string SimpleMethod()
        {
            return "Hi, I am not action method";
        }

        [HeaderFooterFilter]
        [Route("Employee/List")]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
           // employeeListViewModel.UserName = User.Identity.Name; //New Line
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            //employeeListViewModel.UserName = User.Identity.Name; //New Line
            return View("CreateEmployee", employeeListViewModel);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }       

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        //vm.FooterData = new FooterViewModel();
                        //vm.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
                        //vm.FooterData.Year = DateTime.Now.Year.ToString();
                        //vm.UserName = User.Identity.Name; //New Line
                        return View("CreateEmployee", vm); // Day 4 Change - Passing e here
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }

        //public ActionResult MyFooter()
        //{
        //    FooterViewModel FooterData = new FooterViewModel();
        //    FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
        //    FooterData.Year = DateTime.Now.Year.ToString();
        //    return View("Footer", FooterData);
        //}


        //public ActionResult SaveEmployee([ModelBinder(typeof(MyEmployeeModelBinder))]Employee e, string BtnSubmit)
        //{
        //    switch (BtnSubmit)
        //    {
        //        case "Save Employee":
        //            return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
        //        case "Cancel":
        //            return RedirectToAction("Index");
        //    }
        //    return new EmptyResult();
        //}
    }
    //public class MyEmployeeModelBinder : DefaultModelBinder
    //{
    //    protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
    //    {
    //        Employee e = new Employee();
    //        e.FirstName = controllerContext.RequestContext.HttpContext.Request.Form["FName"];
    //        e.LastName = controllerContext.RequestContext.HttpContext.Request.Form["LName"];
    //        e.Salary = int.Parse(controllerContext.RequestContext.HttpContext.Request.Form["Salary"]);
    //        return e;
    //    }
    //}
}