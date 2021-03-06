﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.SPA;
using OldViewModel = WebApplication1.ViewModels;

namespace WebApplication1.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.Username = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", v);
        }
    }
}