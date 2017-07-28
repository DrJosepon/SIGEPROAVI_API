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