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
    public class Gpr_Medicion_HorariaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Medicion_Horaria/Temporada/{idGalpon}/{fecMedicion}/{idServicio}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Medicion_Horaria_ConsultaDTO> BuscarMedicionHorariaXTemporada(int idGalpon, string fecMedicion, int idServicio)
        {
            DateTime fecha = Convert.ToDateTime(fecMedicion);

            var consulta = from MH in db.Gpr_Medicion_Horaria.Where(MH => MH.IdGprGalpon == idGalpon && MH.IdGprServicio == idServicio && DbFunctions.TruncateTime(MH.Fecha) == fecha.Date)
                           select new Gpr_Medicion_Horaria_ConsultaDTO
                           {
                               Fecha = MH.Fecha,
                               IdGprMedicionHoraria = MH.IdGprMedicionHoraria,
                               Medicion = MH.Medicion,
                               Hora = MH.Hora,
                           };

            return consulta;
        }

        [HttpPost]
        [Route("api/Gpr_Medicion_Horaria")]
        [ResponseType(typeof(Gpr_Medicion_Horaria))]
        public async Task<IHttpActionResult> GuardarMedicionHoraria(Gpr_Medicion_Horaria_InsercionDTO gpr_Medicion_HorariaI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Medicion_Horaria_InsercionDTO, Gpr_Medicion_Horaria>());
            Gpr_Medicion_Horaria gpr_Medicion_Horaria = Mapper.Map<Gpr_Medicion_Horaria>(gpr_Medicion_HorariaI);
            gpr_Medicion_Horaria.Fecha = Convert.ToDateTime(gpr_Medicion_HorariaI.Fecha);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Medicion_Horaria.Add(gpr_Medicion_Horaria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Medicion_Horaria.IdGprMedicionHoraria }, gpr_Medicion_Horaria);
        }
    }
}