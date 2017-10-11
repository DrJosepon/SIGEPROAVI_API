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
    public class Seg_Tipo_UsuarioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Seg_Tipo_Usuario")]
        public IQueryable<Seg_Tipo_Usuario> ListarTipoUsuario()
        {
            return db.Seg_Tipo_Usuario;
        }
    }
}