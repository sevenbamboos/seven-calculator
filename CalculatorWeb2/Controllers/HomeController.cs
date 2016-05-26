using samw.Calculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CalculatorWeb2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            Exam exam = new Exam();
            exam.Add(new Exercise("Add I", Expression.InitAdd, 10, 10, 10));
            StringBuilder sb = new StringBuilder();
            foreach (IEvaluable eva in exam.Generate(false))
            {
                sb.AppendFormat("{0},", eva);
            }
            ViewBag.Message = sb.ToString();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}