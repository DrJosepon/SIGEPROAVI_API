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
    public class Gpr_Tipo_ServicioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Tipo_Servicio")]
        public IQueryable<Gpr_Tipo_Servicio> ListarTipoServicio()
        {
            return db.Gpr_Tipo_Servicio;
        }
    }
}