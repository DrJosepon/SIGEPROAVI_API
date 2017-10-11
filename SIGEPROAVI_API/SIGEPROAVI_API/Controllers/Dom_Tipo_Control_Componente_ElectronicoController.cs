using SIGEPROAVI_API.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Dom_Tipo_Control_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Dom_Tipo_Control_Componente_Electronico")]
        public IQueryable<Dom_Tipo_Control_Componente_Electronico> ListarTipoControlComponenteElectronico()
        {
            return db.Dom_Tipo_Control_Componente_Electronico;
        }
    }
}