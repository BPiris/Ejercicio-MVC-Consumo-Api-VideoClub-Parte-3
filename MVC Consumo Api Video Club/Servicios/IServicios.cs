using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Consumo_Api_Video_Club.Servicios
{
    public interface IServicios<TModel>
    {
        Task Add(TModel modelo, String user, String pass);
        Task Update(TModel modelo, String user, String pass);
        Task Delete(int id, String user, String pass);
        List<TModel> Get(String user, String pass);
        TModel Get(int id, String user, String pass);
        List<TModel> Get(Dictionary<String, String> args, String user, String pass);
    }
}
