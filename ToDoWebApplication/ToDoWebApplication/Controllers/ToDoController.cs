using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using System.Diagnostics;
using System.Text.Json;
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
        //Main page
        public ActionResult Index()
        {
            UpdateFilter();
            SaveFilter();
            return Index(ViewData["Filter"] as ToDoModelFilter ?? new ToDoModelFilter());
        }
        //Main page with filter
        [HttpPost]
        public ActionResult Index(ToDoModelFilter filter)
        {
            if (filter == null)
            {
                var listToDo = session.Query<ToDoModel>().Select(i => i).ToList();
                ViewData["Filter"] = new ToDoModelFilter();
                SaveFilter();
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
                    ToDoModel? itemId = session.Get<ToDoModel>(id.Value);
                    if (itemId != null)
                    {
                        ((List<ToDoModel>)listToDo).Add(itemId);
                    }
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
                if (filter.ExpireDateType == ExpireDateType.Today)
                {
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date == DateTime.Today);
                }
                else if (filter.ExpireDateType == ExpireDateType.Nextday)
                {
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date == DateTime.Today.AddDays(1));
                }
                else if (filter.ExpireDateType == ExpireDateType.CurrentWeek)
                {
                    Tuple<DateTime,DateTime> currenWeek = Helper.Helper.CurrentWeek;
                    listToDo = listToDo.Where(i => i.ExpiryDateTime.Date >= currenWeek.Item1 &&  i.ExpiryDateTime.Date <= currenWeek.Item2);

                }
                ViewData["Filter"] = filter;
                SaveFilter();
                listToDo = listToDo.ToList();
                return View(listToDo);
            }
        }

        //Mark as done todo's using in main page
        public ActionResult MarkAsDone(int modelId)
        {
            UpdateFilter();
            var updateModel = session.Get<ToDoModel>(modelId);
            if (updateModel == null)
            {
                return RedirectToAction(nameof(Error));
            }
            else
            {
                updateModel.Done = 1;
                updateModel.Complete = 100;
                session.SaveOrUpdate(updateModel);
                session.Flush();
                return RedirectToAction(nameof(Index));
            }
        }
        //Unmark as done todo's using in main page
        public ActionResult UnmarkAsDone(int modelId)
        {
            UpdateFilter();
            var updateModel = session.Get<ToDoModel>(modelId);
            if (updateModel == null)
            {
                return RedirectToAction(nameof(Error));
            }
            else
            {
                updateModel.Done = 0;
                if(updateModel.Complete == 100)
                {
                    updateModel.Complete = 90;
                }
                session.SaveOrUpdate(updateModel);
                session.Flush();
                return RedirectToAction(nameof(Index));
            }
        }
        //minus complate todo's using in main page
        public ActionResult MinusComplete(int modelId)
        {
            UpdateFilter();
            var updateModel = session.Get<ToDoModel>(modelId);
            if (updateModel == null)
            {
                return RedirectToAction(nameof(Error));
            }
            else
            {
                int newComplate = (int)updateModel.Complete - Settings.Setting.DefualtDeltaComplete;
                if (newComplate < 0)
                {
                    newComplate = 0;
                }
                updateModel.Complete = (byte)newComplate;
                session.SaveOrUpdate(updateModel);
                session.Flush();
                return RedirectToAction(nameof(Index));
            }
        }
        //add complate todo's using in main page
        public ActionResult AddComplete(int modelId)
        {
            UpdateFilter();
            var updateModel = session.Get<ToDoModel>(modelId);
            if (updateModel == null)
            {
                return RedirectToAction(nameof(Error));
            }
            else
            {
                updateModel.Complete += Settings.Setting.DefualtDeltaComplete;
                if(updateModel.Complete > 100)
                {
                    updateModel.Complete = 100;
                }
                session.SaveOrUpdate(updateModel);
                session.Flush();
                return RedirectToAction(nameof(Index));
            }
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
            ToDoModel toDo = new ToDoModel();
            
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
        /// <summary>
        /// Save filter (search criteria) in database
        /// </summary>
        private void SaveFilter()
        {
            try
            {
                if (ViewData.ContainsKey("Filter"))
                {
                    ToDoModelFilter filter = ViewData["Filter"] as ToDoModelFilter ?? new ToDoModelFilter();
                    string jsonStringFilter = JsonSerializer.Serialize(filter);
                    UserTempData data = session.Query<UserTempData>().Select(i => i)
                                .Where(i => i.Name == "Filter").FirstOrDefault()
                                ?? new UserTempData() { Name = "Filter" };
                    data.Value = jsonStringFilter;
                    session.SaveOrUpdate(data);
                    session.Flush();
                }
            }
            catch(Exception)
            {
                
            }
        }
        /// <summary>
        /// Load filter (search criteria) from database
        /// </summary>
        private void UpdateFilter()
        {
            try
            {
                UserTempData data = session.Query<UserTempData>().Select(i => i)
                                .Where(i => i.Name == "Filter").FirstOrDefault()
                                ?? new UserTempData() { Name = "Filter" };
                if (String.IsNullOrWhiteSpace(data.Value))
                {
                    ViewData["Filter"] = new ToDoModelFilter();
                }
                else
                {
                    ToDoModelFilter filter = JsonSerializer.Deserialize<ToDoModelFilter>(data.Value) ?? new ToDoModelFilter();
                    ViewData["Filter"] = filter;
                }
            }
            catch(Exception)
            {

            }
        }
    }
}
