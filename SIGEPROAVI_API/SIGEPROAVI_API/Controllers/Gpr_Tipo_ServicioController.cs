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
    public class Gpr_Tipo_ServicioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Tipo_Servicio
        public IQueryable<Gpr_Tipo_Servicio> GetGpr_Tipo_Servicio()
        {
            return db.Gpr_Tipo_Servicio;
        }

        // GET: api/Gpr_Tipo_Servicio/5
        [ResponseType(typeof(Gpr_Tipo_Servicio))]
        public async Task<IHttpActionResult> GetGpr_Tipo_Servicio(int id)
        {
            Gpr_Tipo_Servicio gpr_Tipo_Servicio = await db.Gpr_Tipo_Servicio.FindAsync(id);
            if (gpr_Tipo_Servicio == null)
            {
                return NotFound();
            }

            return Ok(gpr_Tipo_Servicio);
        }

        // PUT: api/Gpr_Tipo_Servicio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Tipo_Servicio(int id, Gpr_Tipo_Servicio gpr_Tipo_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Tipo_Servicio.IdGprTipoServicio)
            {
                return BadRequest();
            }

            db.Entry(gpr_Tipo_Servicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Tipo_ServicioExists(id))
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

        // POST: api/Gpr_Tipo_Servicio
        [ResponseType(typeof(Gpr_Tipo_Servicio))]
        public async Task<IHttpActionResult> PostGpr_Tipo_Servicio(Gpr_Tipo_Servicio gpr_Tipo_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Tipo_Servicio.Add(gpr_Tipo_Servicio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Tipo_Servicio.IdGprTipoServicio }, gpr_Tipo_Servicio);
        }

        // DELETE: api/Gpr_Tipo_Servicio/5
        [ResponseType(typeof(Gpr_Tipo_Servicio))]
        public async Task<IHttpActionResult> DeleteGpr_Tipo_Servicio(int id)
        {
            Gpr_Tipo_Servicio gpr_Tipo_Servicio = await db.Gpr_Tipo_Servicio.FindAsync(id);
            if (gpr_Tipo_Servicio == null)
            {
                return NotFound();
            }

            db.Gpr_Tipo_Servicio.Remove(gpr_Tipo_Servicio);
            await db.SaveChangesAsync();

            return Ok(gpr_Tipo_Servicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Tipo_ServicioExists(int id)
        {
            return db.Gpr_Tipo_Servicio.Count(e => e.IdGprTipoServicio == id) > 0;
        }
    }
}