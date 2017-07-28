using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SIGEPROAVI_API.Models;
using SIGEPROAVI_API.DTO;
using AutoMapper;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Estado_AveController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Estado_Ave
        public IQueryable<Gpr_Estado_Ave> GetGpr_Estado_Ave()
        {
            return db.Gpr_Estado_Ave;
        }

        [HttpGet]
        [Route("api/Gpr_Estado_Ave/Temporada/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Estado_Ave_ConsultaDTO> BuscarEstadoAveXTemporada(int idTemporada)
        {
            //var consulta = from EA in db.Gpr_Estado_Ave.Where(EA => EA.IdGprTemporada == idTemporada)
            //               from TEA in db.Gpr_Tipo_Estado_Ave.Where(TEA => TEA.IdGprTipoEstadoAve == EA.IdGprTipoEstadoAve)
            //               select new Gpr_Estado_Ave_ConsultaDTO
            //               {
            //                   CantidadAves = EA.CantidadAves,
            //                   DescripcionEstadoAve = TEA.Descripcion,
            //                   IdGprEstadoAve = EA.IdGprEstadoAve,
            //                   IdGprTemporada = EA.IdGprTemporada,
            //                   IdGprTipoEstadoAve = EA.IdGprTipoEstadoAve,
            //               };

            var consulta = from TEA in db.Gpr_Tipo_Estado_Ave
                           from EA in db.Gpr_Estado_Ave.Where(EA => EA.IdGprTemporada == idTemporada && EA.IdGprTipoEstadoAve == TEA.IdGprTipoEstadoAve).DefaultIfEmpty()
                           select new Gpr_Estado_Ave_ConsultaDTO
                           {
                               CantidadAves = EA.CantidadAves,
                               DescripcionEstadoAve = TEA.Descripcion,
                               IdGprEstadoAve = EA.IdGprEstadoAve,
                               IdGprTemporada = idTemporada,
                               IdGprTipoEstadoAve = TEA.IdGprTipoEstadoAve,
                           };

            return consulta;
        }

        // GET: api/Gpr_Estado_Ave/5
        [ResponseType(typeof(Gpr_Estado_Ave))]
        public async Task<IHttpActionResult> GetGpr_Estado_Ave(int id)
        {
            Gpr_Estado_Ave gpr_Estado_Ave = await db.Gpr_Estado_Ave.FindAsync(id);
            if (gpr_Estado_Ave == null)
            {
                return NotFound();
            }

            return Ok(gpr_Estado_Ave);
        }

        // PUT: api/Gpr_Estado_Ave/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Estado_Ave(int id, Gpr_Estado_Ave gpr_Estado_Ave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Estado_Ave.IdGprEstadoAve)
            {
                return BadRequest();
            }

            db.Entry(gpr_Estado_Ave).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Estado_AveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Gpr_Estado_Ave
        [ResponseType(typeof(Gpr_Estado_Ave))]
        public async Task<IHttpActionResult> PostGpr_Estado_Ave(Gpr_Estado_Ave gpr_Estado_Ave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Estado_Ave.Add(gpr_Estado_Ave);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Estado_Ave.IdGprEstadoAve }, gpr_Estado_Ave);
        }

        [HttpPost]
        [Route("api/Gpr_Estado_Ave/Procesar")]
        public async Task<IHttpActionResult> ProcesarGpr_Estado_Ave(List<Gpr_Estado_Ave_EdicionDTO> gpr_Estado_Aves)
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

            foreach (Gpr_Estado_Ave_EdicionDTO estado in gpr_Estado_Aves)
            {
                if (estado.IdGprEstadoAve != null)
                {
                    Gpr_Estado_Ave gpr_Estado_Ave = db.Gpr_Estado_Ave.Find(estado.IdGprEstadoAve);
                    //gpr_Estado_Ave.Estado = estado.Estado;
                    gpr_Estado_Ave.FechaModificacion = DateTime.Now;
                    gpr_Estado_Ave.UsuarioModificador = estado.UsuarioModificador;
                    //gpr_Estado_Ave.Usuario = estado.Usuario;
                    gpr_Estado_Ave.CantidadAves = estado.CantidadAves ?? 0;

                    db.Entry(gpr_Estado_Ave).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                else
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Estado_Ave_EdicionDTO, Gpr_Estado_Ave>());

                    Gpr_Estado_Ave gpr_Estado_Ave = Mapper.Map<Gpr_Estado_Ave>(estado);
                    gpr_Estado_Ave.FechaCreacion = DateTime.Now;
                    gpr_Estado_Ave.Estado = true;

                    db.Gpr_Estado_Ave.Add(gpr_Estado_Ave);
                    await db.SaveChangesAsync();
                }
            }

            return Content(HttpStatusCode.Accepted, "Completado.");
        }

        // DELETE: api/Gpr_Estado_Ave/5
        [ResponseType(typeof(Gpr_Estado_Ave))]
        public async Task<IHttpActionResult> DeleteGpr_Estado_Ave(int id)
        {
            Gpr_Estado_Ave gpr_Estado_Ave = await db.Gpr_Estado_Ave.FindAsync(id);
            if (gpr_Estado_Ave == null)
            {
                return NotFound();
            }

            db.Gpr_Estado_Ave.Remove(gpr_Estado_Ave);
            await db.SaveChangesAsync();

            return Ok(gpr_Estado_Ave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Estado_AveExists(int id)
        {
            return db.Gpr_Estado_Ave.Count(e => e.IdGprEstadoAve == id) > 0;
        }
    }
}