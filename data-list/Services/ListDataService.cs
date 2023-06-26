using System;
using System.Web;

namespace data_list.Services
{
    public class ListDataService : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //services.AddScoped<IDataRepository, DataRepository>();

    }
}
