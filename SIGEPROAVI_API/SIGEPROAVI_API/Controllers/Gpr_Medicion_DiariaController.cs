using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Medicion_DiariaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Medicion_Diaria/Temporada/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Medicion_Diaria_ConsultaDTO> BuscarMedicionDiariaXTemporada(int idTemporada)
        {
            Gpr_Temporada temporada = db.Gpr_Temporada.Where(T => T.IdGprTemporada == idTemporada).FirstOrDefault();

            if (temporada.FechaFin == null)
            {
                temporada.FechaFin = DateTime.Now;
            }

            var consulta = from MD in db.Gpr_Medicion_Diaria.Where(MD => MD.IdGprGalpon == temporada.IdGprGalpon && (MD.Fecha >= temporada.FechaInicio && MD.Fecha <= temporada.FechaFin))
                           select new Gpr_Medicion_Diaria_ConsultaDTO
                           {
                               Fecha = MD.Fecha,
                               IdGprMedicionDiaria = MD.IdGprMedicionDiaria,
                               Medicion = MD.Medicion,
                               IdGprServicio = MD.IdGprServicio,
                           };

            return consulta;
        }

        [HttpPost]
        [Route("api/Gpr_Medicion_Diaria")]
        [ResponseType(typeof(Gpr_Medicion_Diaria))]
        public async Task<IHttpActionResult> GuardarMedicionDiaria(Gpr_Medicion_Diaria_InsercionDTO gpr_Medicion_DiariaI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Medicion_Diaria_InsercionDTO, Gpr_Medicion_Diaria>());
            Gpr_Medicion_Diaria gpr_Medicion_Diaria = Mapper.Map<Gpr_Medicion_Diaria>(gpr_Medicion_DiariaI);
            gpr_Medicion_Diaria.Fecha = Convert.ToDateTime(gpr_Medicion_DiariaI.Fecha);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Medicion_Diaria.Add(gpr_Medicion_Diaria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Medicion_Diaria.IdGprMedicionDiaria }, gpr_Medicion_Diaria);
        }
    }
}