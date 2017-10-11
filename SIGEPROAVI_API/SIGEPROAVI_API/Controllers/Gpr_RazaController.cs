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
    public class Gpr_RazaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Raza
        public IQueryable<Gpr_Raza> GetGpr_Raza()
        {
            return db.Gpr_Raza;
        }

        // GET: api/Gpr_Raza/5
        [ResponseType(typeof(Gpr_Raza))]
        public async Task<IHttpActionResult> GetGpr_Raza(int id)
        {
            Gpr_Raza gpr_Raza = await db.Gpr_Raza.FindAsync(id);
            if (gpr_Raza == null)
            {
                return NotFound();
            }

            return Ok(gpr_Raza);
        }

        // PUT: api/Gpr_Raza/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Raza(int id, Gpr_Raza gpr_Raza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Raza.IdGprRaza)
            {
                return BadRequest();
            }

            db.Entry(gpr_Raza).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_RazaExists(id))
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

        // POST: api/Gpr_Raza
        [ResponseType(typeof(Gpr_Raza))]
        public async Task<IHttpActionResult> PostGpr_Raza(Gpr_Raza gpr_Raza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Raza.Add(gpr_Raza);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Raza.IdGprRaza }, gpr_Raza);
        }

        // DELETE: api/Gpr_Raza/5
        [ResponseType(typeof(Gpr_Raza))]
        public async Task<IHttpActionResult> DeleteGpr_Raza(int id)
        {
            Gpr_Raza gpr_Raza = await db.Gpr_Raza.FindAsync(id);
            if (gpr_Raza == null)
            {
                return NotFound();
            }

            db.Gpr_Raza.Remove(gpr_Raza);
            await db.SaveChangesAsync();

            return Ok(gpr_Raza);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_RazaExists(int id)
        {
            return db.Gpr_Raza.Count(e => e.IdGprRaza == id) > 0;
        }
    }
}