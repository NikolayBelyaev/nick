using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitor2.DAL;
using Monitor2.DAL.Entities;
using Monitor2.Models;
using Monitor2.Services.DataMappers;

namespace Monitor2.Services.Implementation
{
    public class DBService : IDBService
    {
        MonitorDBContext _context;

        public DBService(MonitorDBContext db)
        {
            _context = db;
        }

        public ServiceModel GetService(int id)
        {
            var res = _context.Services.Where(s => s.Id == id).FirstOrDefault();

            if (res == null)
                throw new Exception($"No services with id {id}");

            return res.ToModel();
        }

        public ServiceModel[] GetServices()
        {
            var res = _context.Services.Select(x => x.ToModel()).ToArray();

            if (res == null)
                throw new Exception("There are no services in database");
            
            return res;
        }

        public void SetData(ServiceModel model)
        {
            if (model == null)
                throw new Exception("Model is null");

            _context.Add(model.ToEntity());
            _context.SaveChanges();
        }
    }
}
