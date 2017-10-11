using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Gasto_DiarioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Gasto_Diario/Temporada/{idTemporada}")]
        public IQueryable<Gpr_Gasto_Diario_ConsultaDTO> BuscarGastoDiarioXTemporada(int idTemporada)
        {
            Gpr_Temporada temporada = db.Gpr_Temporada.Where(T => T.IdGprTemporada == idTemporada).FirstOrDefault();

            if (temporada.FechaFin == null)
            {
                temporada.FechaFin = DateTime.Now;
            }

            var consulta = from MD in db.Gpr_Medicion_Diaria.Where(MD => MD.IdGprGalpon == temporada.IdGprGalpon && (MD.Fecha >= temporada.FechaInicio && MD.Fecha <= temporada.FechaFin))
                           from GD in db.Gpr_Gasto_Diario.Where(GD => GD.IdGprMedicionDiaria == MD.IdGprMedicionDiaria)
                           select new Gpr_Gasto_Diario_ConsultaDTO
                           {
                               Fecha = MD.Fecha,
                               Gasto = GD.Gasto,
                               IdGprGastoDiario = GD.IdGprGastoDiario,
                               IdGprServicio = MD.IdGprServicio,
                           };

            return consulta;
        }

        [HttpPost]
        [Route("api/Gpr_Gasto_Diario")]
        [ResponseType(typeof(Gpr_Gasto_Diario))]
        public async Task<IHttpActionResult> GuardarGastoDiario(Gpr_Gasto_Diario gpr_Gasto_Diario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Gasto_Diario.Add(gpr_Gasto_Diario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Gasto_Diario.IdGprGastoDiario }, gpr_Gasto_Diario);
        }
    }
}