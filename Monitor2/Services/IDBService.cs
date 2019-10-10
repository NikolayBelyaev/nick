using Monitor2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.Services
{
    public interface IDBService
    {
        void SetData(ServiceModel model);
        ServiceModel[] GetServices();
        ServiceModel GetService(int id);
    }
}
