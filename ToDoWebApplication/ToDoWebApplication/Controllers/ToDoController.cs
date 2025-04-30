using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoWebApplication.Models;



namespace ToDoWebApplication.Controllers
{
    public class ToDoController : Controller
    {
        // GET: ToDoController
        public ActionResult Index()
        {
            List<ToDoModel> tempList = new List<ToDoModel>()
            {
                new ToDoModel()
                {
                    Complete = 10,
                    Description = "Test description",
                    Done = 0,
                    ExpiryDateTime = DateTime.Now,
                    Title = "Test title"
                },
                new ToDoModel()
                {
                    Complete = 20,
                    Description = "Test description2",
                    Done = 0,
                    ExpiryDateTime = DateTime.Now.AddDays(7),
                    Title = "Test title2"

                }
            };
            return View(tempList);
        }

        public ActionResult IncomingToDo()
        {
            List<ToDoModel> tempList = new List<ToDoModel>()
            {
                new ToDoModel()
                {
                    Complete = 10,
                    Description = "Test description",
                    Done = 0,
                    ExpiryDateTime = DateTime.Today.AddDays(1).AddMinutes(-1),
                    Title = "Test title"
                },
                new ToDoModel()
                {
                    Complete = 20,
                    Description = "Test description2",
                    Done = 0,
                    ExpiryDateTime = DateTime.Today.AddDays(7),
                    Title = "Test title2"

                }
            };
            return View(tempList);
        }

        // GET: ToDoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
