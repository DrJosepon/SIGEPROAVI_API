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
    public class Gpr_Estado_AveController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Estado_Ave/Temporada/{idTemporada}")]
        public IQueryable<Gpr_Estado_Ave_ConsultaDTO> BuscarEstadoAveXTemporada(int idTemporada)
        {
            var consulta = from TEA in db.Gpr_Tipo_Estado_Ave
                           from EA in db.Gpr_Estado_Ave.Where(EA => EA.IdGprTemporada == idTemporada && EA.IdGprTipoEstadoAve == TEA.IdGprTipoEstadoAve).DefaultIfEmpty()
                           select new Gpr_Estado_Ave_ConsultaDTO
                           {
                               CantidadAves = EA.CantidadAves,
                               DescripcionEstadoAve = TEA.Descripcion,
                               IdGprEstadoAve = EA.IdGprEstadoAve,
                               IdGprTemporada = idTemporada,
                               IdGprTipoEstadoAve = TEA.IdGprTipoEstadoAve,
                               Fecha = EA.Fecha,
                           };

            return consulta;
        }

        [HttpGet]
        [Route("api/Gpr_Estado_Ave/Activo/Temporada/{idTemporada}")]
        public IQueryable<Gpr_Estado_Ave_ConsultaDTO> BuscarEstadoAveXTemporadaActivo(int idTemporada)
        {
            var consulta = from TEA in db.Gpr_Tipo_Estado_Ave
                           from EA in db.Gpr_Estado_Ave.Where(EA => EA.IdGprTemporada == idTemporada && EA.IdGprTipoEstadoAve == TEA.IdGprTipoEstadoAve && EA.Estado == true).DefaultIfEmpty()
                           select new Gpr_Estado_Ave_ConsultaDTO
                           {
                               CantidadAves = EA.CantidadAves,
                               DescripcionEstadoAve = TEA.Descripcion,
                               IdGprEstadoAve = EA.IdGprEstadoAve,
                               IdGprTemporada = idTemporada,
                               IdGprTipoEstadoAve = TEA.IdGprTipoEstadoAve,
                               Fecha = EA.Fecha,
                           };

            return consulta;
        }

        [HttpPost]
        [Route("api/Gpr_Estado_Ave/Procesar")]
        public async Task<IHttpActionResult> ProcesarEstadoAve(List<Gpr_Estado_Ave_EdicionDTO> gpr_Estado_Aves)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            int CantidadAves = 0;

            foreach (Gpr_Estado_Ave_EdicionDTO estado in gpr_Estado_Aves)
            {
                CantidadAves = CantidadAves + (estado.CantidadAves ?? 0);
            }

            Gpr_Temporada temporada = await db.Gpr_Temporada.FindAsync(gpr_Estado_Aves[0].IdGprTemporada);//db.Gpr_Temporada.Where(X => X.IdGprTemporada == gpr_Estado_Aves[0].IdGprTemporada).FirstOrDefault();

            if (temporada.CantidadAves != CantidadAves)
            {
                return Content(HttpStatusCode.BadRequest, "El total de aves debe ser igual a la cantidad de aves de la temporada.");
            }

            List<Gpr_Estado_Ave> estadoAves = db.Gpr_Estado_Ave.Where(X => X.IdGprTemporada == temporada.IdGprTemporada && X.Estado == true).ToList();

            foreach (Gpr_Estado_Ave estado in estadoAves)
            {
                Gpr_Estado_Ave gpr_Estado_Ave = db.Gpr_Estado_Ave.Find(estado.IdGprEstadoAve);
                gpr_Estado_Ave.FechaModificacion = DateTime.Now;
                gpr_Estado_Ave.UsuarioModificador = gpr_Estado_Aves[0].UsuarioCreador;
                gpr_Estado_Ave.Estado = false;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            foreach (Gpr_Estado_Ave_EdicionDTO estado in gpr_Estado_Aves)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Estado_Ave_EdicionDTO, Gpr_Estado_Ave>());

                Gpr_Estado_Ave gpr_Estado_Ave = Mapper.Map<Gpr_Estado_Ave>(estado);
                gpr_Estado_Ave.FechaCreacion = DateTime.Now;
                gpr_Estado_Ave.Estado = true;

                db.Gpr_Estado_Ave.Add(gpr_Estado_Ave);
                await db.SaveChangesAsync();
            }

            return Content(HttpStatusCode.Accepted, "Completado.");
        }

        private bool VerificarEstadoAve(int id)
        {
            return db.Gpr_Estado_Ave.Count(e => e.IdGprEstadoAve == id) > 0;
        }
    }
}