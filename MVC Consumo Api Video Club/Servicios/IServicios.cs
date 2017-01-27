using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Consumo_Api_Video_Club.Servicios
{
    public interface IServicios<TModel>
    {
        Task Add(TModel modelo);
        Task Update(TModel modelo);
        Task Delete(int id);
        List<TModel> Get();
        TModel Get(int id);
        List<TModel> Get(Dictionary<String, String> args);
    }
}
