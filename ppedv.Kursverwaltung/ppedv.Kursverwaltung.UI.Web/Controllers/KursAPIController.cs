using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.Kursverwaltung.Logic;
using ppedv.Kursverwaltung.Model;
using ppedv.Kursverwaltung.Model.Contracts;
using ppedv.Kursverwaltung.UI.Web.Models;

namespace ppedv.Kursverwaltung.UI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KursAPIController : ControllerBase
    {
        public KursAPIController()
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

            //var map = new Mapper().Map<Kurs,KursViewModel>(x=>x.)

        }

        readonly Core core = null;

        // GET: api/KursAPI
        [HttpGet]
        public IEnumerable<KursViewModel> Get()
        {
            foreach (var k in core.Repository.GetAll<Kurs>())
            {
                yield return new KursViewModel()
                {
                    Id = k.Id,
                    Ort = k.Ort,
                    Thema = k.Thema?.Beschreibung,
                    Trainer = k.Trainer?.Name,
                    Start = k.Start
                };
            }
        }

        // GET: api/KursAPI/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/KursAPI
        [HttpPost]
        public void Post([FromBody] KursViewModel value)
        {
            var k = new Kurs()
            {
                Start = value.Start,
                Ort = value.Ort
            };
            core.Repository.Add(k);
            core.Repository.Save();
        }

        // PUT: api/KursAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
