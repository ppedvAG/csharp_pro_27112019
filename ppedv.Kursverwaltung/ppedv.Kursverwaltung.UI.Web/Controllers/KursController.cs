using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.Kursverwaltung.Logic;
using ppedv.Kursverwaltung.Model;
using ppedv.Kursverwaltung.Model.Contracts;

namespace ppedv.Kursverwaltung.UI.Web.Controllers
{
    public class KursController : Controller
    {
        public KursController()
        {
#if DEBUG
            var path = @"C:\Users\ar2\source\repos\ppedvAG\csharp_pro_27112019\ppedv.Kursverwaltung\ppedv.Kursverwaltung.Data.EF\bin\Debug\ppedv.Kursverwaltung.Data.EF.dll";
#else
            var path = @"C:\Users\ar2\source\repos\ppedvAG\csharp_pro_27112019\ppedv.Kursverwaltung\ppedv.Kursverwaltung.Data.EF\bin\Release\ppedv.Kursverwaltung.Data.EF.dll";
#endif

            var ass = Assembly.LoadFile(path);
            var derTypMitDemInterface = ass.GetTypes()
                                  .FirstOrDefault(x => x.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IRepository)));

            var repo = Activator.CreateInstance(derTypMitDemInterface) as IRepository;


            core = new Core(repo);
        }

        readonly Core core = null;

        // GET: Kurs
        public ActionResult Index()
        {
            var kurse = core.Repository.GetAll<Kurs>();
            return View(kurse);
        }

        // GET: Kurs/Details/5
        public ActionResult Details(int id)
        {
            return View(core.Repository.GetById<Kurs>(id));
        }

        // GET: Kurs/Create
        public ActionResult Create()
        {
            return View(new Kurs() { Ort = "Düsseldorf" });
        }

        // POST: Kurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kurs kurs)
        {
            try
            {
                core.Repository.Add(kurs);
                core.Repository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Kurs/Edit/5
        public ActionResult Edit(int id)
        {
            return View(core.Repository.GetById<Kurs>(id));
        }

        // POST: Kurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kurs kurs)
        {
            try
            {
                core.Repository.Update(kurs);
                core.Repository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Kurs/Delete/5
        public ActionResult Delete(int id)
        {
            return View(core.Repository.GetById<Kurs>(id));
        }

        // POST: Kurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kurs kurs)
        {
            try
            {
                var loaded = core.Repository.GetById<Kurs>(id);
                if(loaded!=null)
                {
                    core.Repository.Delete(loaded);
                    core.Repository.Save();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}