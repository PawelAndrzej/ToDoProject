using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoWebApplication.Models;



namespace ToDoWebApplication.Controllers
{
    public class ToDoController : Controller
    {
        private NHibernate.ISession session;
        public ToDoController(NHibernate.ISession session)
        {
            this.session = session;
        }
        // GET: ToDoController
        /*
        public ActionResult Index()
        {
            var listToDo = session.Query<ToDoModel>().Select(i => i).ToList();
            ViewData["Filter"] = new ToDoModelFilter()
            {
                All = true
            };
            return View(listToDo);
        }*/
        public ActionResult Index(ToDoModelFilter filter)
        {
            if (filter == null)
            {
                var listToDo = session.Query<ToDoModel>().Select(i => i).ToList();
                ViewData["Filter"] = new ToDoModelFilter()
                {
                    All = true
                };
                return View(listToDo);
            }
            else
            {
                int? id = null;
                try
                {
                    id = int.Parse(filter.SearchText);
                }
                catch (Exception)
                {

                }

                IEnumerable<ToDoModel> listToDo = new List<ToDoModel>();
                if (id != null)
                {
                    ((List<ToDoModel>)listToDo).Add(session.Get<ToDoModel>(id.Value));
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(filter.SearchText))
                    {
                        listToDo = session.Query<ToDoModel>().Select(i => i);

                    }
                    else
                    {
                        listToDo = session.Query<ToDoModel>().Select(i => i)
                            .Where(i => i.Title.ToLower().Contains(filter.SearchText.ToLower())
                            || i.Description.ToLower().Contains(filter.SearchText.ToLower())
                            );
                    }
                }
                if (filter.Today)
                {
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date == DateTime.Today);
                }
                if (filter.NextDay)
                {
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date == DateTime.Today.AddDays(1));
                }
                if (filter.IncommingWeek)
                {
                    DateTime startNextWeek = DateTime.Today;
                    for (int i = 1; i <= 7; i++)
                    {
                        startNextWeek = startNextWeek.AddDays(1);
                        if (startNextWeek.DayOfWeek == DayOfWeek.Monday)
                        {
                            break;
                        }
                    }
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date >= startNextWeek && i.ExpiryDateTime <= startNextWeek.AddDays(6));

                }
                ViewData["Filter"] = filter;
                listToDo = listToDo.ToList();
                return View(listToDo);
            }
        }

        public ActionResult IncomingToDo()
        {
            var listToDo = session.Query<ToDoModel>().Select(i => i).ToList();
            return View(listToDo);
        }

        // GET: ToDoController/Details/5
        public ActionResult Details(int id)
        {
            var toDo = session.Get<ToDoModel>(id);
            return View(toDo);
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            ToDoModel toDo = new ToDoModel()
            {
                Title = Settings.Setting.ToDoDefaultTitle,
                Complete = Settings.Setting.ToDoDefaultComplete,
                Description = Settings.Setting.ToDoDefaultDescription,
                ExpiryDateTime = Settings.Setting.ToDoDefaultExpiryDateTime,
                Done = 0
            };
            return View(toDo);
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            ToDoModel toDo = new ToDoModel();
            try
            {
                toDo.Complete = byte.Parse(collection["Complete"].FirstOrDefault() ?? "0");
                toDo.Title = collection["Title"].FirstOrDefault() ?? String.Empty;
                toDo.Done = 0;
                toDo.Description = collection["Description"].FirstOrDefault() ?? String.Empty;
                toDo.ExpiryDateTime = DateTime.Parse(collection["ExpiryDateTime"].FirstOrDefault() ?? DateTime.Today.ToString());
                session.SaveOrUpdate(toDo);
                session.Flush();
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
            var toDo = session.Get<ToDoModel>(id);
            return View(toDo);
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var toDo = session.Get<ToDoModel>(id);
            try
            {
                toDo.Complete = byte.Parse(collection["Complete"].FirstOrDefault() ?? "0");
                toDo.Title = collection["Title"].FirstOrDefault() ?? String.Empty;
                toDo.Description = collection["Description"].FirstOrDefault() ?? String.Empty;
                toDo.ExpiryDateTime = DateTime.Parse(collection["ExpiryDateTime"].FirstOrDefault() ?? DateTime.Today.ToString());
                session.SaveOrUpdate(toDo);
                session.Flush();
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
            var toDo = session.Get<ToDoModel>(id);
            return View(toDo);
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var toDo = session.Get<ToDoModel>(id);
                session.Delete(toDo);
                session.Flush();
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
