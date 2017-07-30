using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_GalponController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Galpon
        public IQueryable<Gpr_Galpon> GetGpr_Galpon()
        {
            return db.Gpr_Galpon;
        }

        // GET: api/Gpr_Galpon/5
        [ResponseType(typeof(Gpr_Galpon))]
        public async Task<IHttpActionResult> GetGpr_Galpon(int id)
        {
            Gpr_Galpon gpr_Galpon = await db.Gpr_Galpon.FindAsync(id);
            if (gpr_Galpon == null)
            {
                return NotFound();
            }

            return Ok(gpr_Galpon);
        }

        // PUT: api/Gpr_Galpon/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Galpon(int id, Gpr_Galpon_ModificacionDTO gpr_GalponM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_GalponM.IdGprGalpon)
            {
                return BadRequest();
            }

            Gpr_Galpon gpr_Galpon = await db.Gpr_Galpon.FindAsync(id);
            gpr_Galpon.Estado = gpr_GalponM.Estado;
            gpr_Galpon.FechaModificacion = DateTime.Now;
            gpr_Galpon.Descripcion = gpr_GalponM.Descripcion;
            gpr_Galpon.CantidadAves = gpr_GalponM.CantidadAves;
            gpr_Galpon.UsuarioModificador = gpr_GalponM.UsuarioModificador;

            db.Entry(gpr_Galpon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_GalponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(db.Gpr_Galpon);
        }

        // POST: api/Gpr_Galpon
        [ResponseType(typeof(Gpr_Galpon))]
        //public async Task<IHttpActionResult> PostGpr_Galpon(Gpr_Galpon gpr_Galpon)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Gpr_Galpon.Add(gpr_Galpon);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = gpr_Galpon.IdGprGalpon }, gpr_Galpon);
        //}
        public async Task<IHttpActionResult> PostGpr_Galpon(Gpr_Galpon_InsercionDTO gpr_GalponI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Galpon_InsercionDTO, Gpr_Galpon>());

            Gpr_Galpon gpr_Galpon = Mapper.Map<Gpr_Galpon>(gpr_GalponI);
            gpr_Galpon.FechaCreacion = DateTime.Now;
            gpr_Galpon.Estado = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Galpon.Add(gpr_Galpon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Galpon.IdGprGalpon }, gpr_Galpon);
        }

        // DELETE: api/Gpr_Galpon/5
        [ResponseType(typeof(Gpr_Galpon))]
        public async Task<IHttpActionResult> DeleteGpr_Galpon(int id)
        {
            Gpr_Galpon gpr_Galpon = await db.Gpr_Galpon.FindAsync(id);
            if (gpr_Galpon == null)
            {
                return NotFound();
            }

            db.Gpr_Galpon.Remove(gpr_Galpon);
            await db.SaveChangesAsync();

            return Ok(gpr_Galpon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_GalponExists(int id)
        {
            return db.Gpr_Galpon.Count(e => e.IdGprGalpon == id) > 0;
        }
    }
}