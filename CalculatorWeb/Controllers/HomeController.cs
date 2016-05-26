using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using samw.Calculator.Model;
using System.Text;

namespace CalculatorWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            Exam exam = new Exam();
            exam.Add(new Exercise("Add I", Expression.InitAdd, 10, 10, 10));
            StringBuilder sb = new StringBuilder();
            foreach (IEvaluable eva in exam.Generate(false))
            {
                sb.AppendFormat("{0},", eva);
            }

            ViewData["Message"] = sb.ToString();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
