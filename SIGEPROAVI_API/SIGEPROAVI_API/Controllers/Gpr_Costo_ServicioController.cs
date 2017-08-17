using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Costo_ServicioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Costo_Servicio
        public IQueryable<Gpr_Costo_Servicio> GetGpr_Costo_Servicio()
        {
            return db.Gpr_Costo_Servicio;
        }

        [HttpGet]
        [Route("api/Gpr_Costo_Servicio/Activo")]
        public IQueryable<Gpr_Costo_Servicio> GetGpr_Costo_ServicioActivo()
        {
            return db.Gpr_Costo_Servicio.Where(S => S.Estado == true);
        }

        [HttpGet]
        [Route("api/Gpr_Costo_Servicio/Servicio/{idServicio}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Costo_Servicio_ConsultaDTO> BuscarCostoXServicio(int idServicio)
        {
            var consulta = from CS in db.Gpr_Costo_Servicio.Where(CS => CS.IdGprServicio == idServicio)
                           select new Gpr_Costo_Servicio_ConsultaDTO
                           {
                               Costo = CS.Costo,
                               Estado = CS.Estado,
                               Fecha = CS.Fecha,
                               IdGprCostoServicio = CS.IdGprCostoServicio,
                               IdGprServicio = CS.IdGprServicio
                           };

            return consulta.OrderByDescending(C => C.Fecha).ThenBy(C => C.Estado);
        }

        // GET: api/Gpr_Costo_Servicio/5
        [ResponseType(typeof(Gpr_Costo_Servicio))]
        public async Task<IHttpActionResult> GetGpr_Costo_Servicio(int id)
        {
            Gpr_Costo_Servicio gpr_Costo_Servicio = await db.Gpr_Costo_Servicio.FindAsync(id);
            if (gpr_Costo_Servicio == null)
            {
                return NotFound();
            }

            return Ok(gpr_Costo_Servicio);
        }

        // PUT: api/Gpr_Costo_Servicio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Costo_Servicio(int id, Gpr_Costo_Servicio gpr_Costo_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Costo_Servicio.IdGprCostoServicio)
            {
                return BadRequest();
            }

            db.Entry(gpr_Costo_Servicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Costo_ServicioExists(id))
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

        // POST: api/Gpr_Costo_Servicio
        [ResponseType(typeof(Gpr_Costo_Servicio))]
        public async Task<IHttpActionResult> PostGpr_Costo_Servicio(Gpr_Costo_Servicio gpr_Costo_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Costo_Servicio.Add(gpr_Costo_Servicio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Costo_Servicio.IdGprCostoServicio }, gpr_Costo_Servicio);
        }

        [HttpPost]
        [Route("api/Gpr_Costo_Servicio/Procesar")]
        public async Task<IHttpActionResult> ProcesarGpr_Costo_Servicio(Gpr_Costo_Servicio_EdicionDTO gpr_Costo_ServicioE)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            List<Gpr_Costo_Servicio> costos = db.Gpr_Costo_Servicio.Where(S => S.IdGprServicio == gpr_Costo_ServicioE.IdGprServicio).ToList();

            foreach (Gpr_Costo_Servicio costo in costos)
            {
                costo.Estado = false;
                costo.FechaModificacion = DateTime.Now;
                costo.UsuarioModificador = gpr_Costo_ServicioE.UsuarioCreador;

                db.Entry(costo).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Gpr_Costo_ServicioExists(costo.IdGprCostoServicio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Costo_Servicio_EdicionDTO, Gpr_Costo_Servicio>());

            Gpr_Costo_Servicio gpr_Costo_Servicio = Mapper.Map<Gpr_Costo_Servicio>(gpr_Costo_ServicioE);
            gpr_Costo_Servicio.FechaCreacion = DateTime.Now;
            gpr_Costo_Servicio.Fecha = DateTime.Now;
            gpr_Costo_Servicio.Estado = true;

            db.Gpr_Costo_Servicio.Add(gpr_Costo_Servicio);
            await db.SaveChangesAsync();

            //return Content(HttpStatusCode.Accepted, "Completado.");
            return Ok(BuscarCostoXServicio(gpr_Costo_ServicioE.IdGprServicio));
            //return CreatedAtRoute("DefaultApi", new { id = gpr_Costo_Servicio.IdGprCostoServicio }, gpr_Costo_Servicio);
        }

        // DELETE: api/Gpr_Costo_Servicio/5
        [ResponseType(typeof(Gpr_Costo_Servicio))]
        public async Task<IHttpActionResult> DeleteGpr_Costo_Servicio(int id)
        {
            Gpr_Costo_Servicio gpr_Costo_Servicio = await db.Gpr_Costo_Servicio.FindAsync(id);
            if (gpr_Costo_Servicio == null)
            {
                return NotFound();
            }

            db.Gpr_Costo_Servicio.Remove(gpr_Costo_Servicio);
            await db.SaveChangesAsync();

            return Ok(gpr_Costo_Servicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Costo_ServicioExists(int id)
        {
            return db.Gpr_Costo_Servicio.Count(e => e.IdGprCostoServicio == id) > 0;
        }
    }
}