using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Collections.Generic;
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
    public class Gpr_Peso_Promedio_AveController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Peso_Promedio_Ave/Temporada/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Peso_Promedio_Ave> BuscarPesoPromedioXTemporada(int idTemporada)
        {
            return db.Gpr_Peso_Promedio_Ave.Where(X => X.IdGprTemporada == idTemporada);
        }

        [HttpPost]
        [Route("api/Gpr_Peso_Promedio_Ave")]
        [ResponseType(typeof(Gpr_Peso_Promedio_Ave))]
        public async Task<IHttpActionResult> GuardarPesoPromedioAve(Gpr_Peso_Promedio_Ave_InsercionDTO gpr_Peso_Promedio_AveI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Peso_Promedio_Ave_InsercionDTO, Gpr_Peso_Promedio_Ave>());

            Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave = Mapper.Map<Gpr_Peso_Promedio_Ave>(gpr_Peso_Promedio_AveI);
            gpr_Peso_Promedio_Ave.FechaCreacion = DateTime.Now;
            gpr_Peso_Promedio_Ave.IdGprTemporada = gpr_Peso_Promedio_AveI.IdGprTemporada;
            gpr_Peso_Promedio_Ave.Peso = gpr_Peso_Promedio_AveI.Peso;
            gpr_Peso_Promedio_Ave.Estado = true;
            gpr_Peso_Promedio_Ave.UsuarioCreador = gpr_Peso_Promedio_AveI.UsuarioCreador;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Gpr_Peso_Promedio_Ave> pesos = db.Gpr_Peso_Promedio_Ave.Where(X => X.IdGprTemporada == gpr_Peso_Promedio_AveI.IdGprTemporada).ToList();

            foreach (Gpr_Peso_Promedio_Ave peso in pesos)
            {
                if (peso.Fecha == gpr_Peso_Promedio_AveI.Fecha)
                {
                    return Content(HttpStatusCode.BadRequest, "No pueden existir dos registros en la misma fecha.");
                }
            }

            db.Gpr_Peso_Promedio_Ave.Add(gpr_Peso_Promedio_Ave);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Peso_Promedio_Ave.IdGprPesoPromedioAve }, gpr_Peso_Promedio_Ave);
        }
    }
}