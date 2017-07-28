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
    public class Seg_Tipo_UsuarioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Seg_Tipo_Usuario
        public IQueryable<Seg_Tipo_Usuario> GetSeg_Tipo_Usuario()
        {
            return db.Seg_Tipo_Usuario;
        }

        // GET: api/Seg_Tipo_Usuario/5
        [ResponseType(typeof(Seg_Tipo_Usuario))]
        public async Task<IHttpActionResult> GetSeg_Tipo_Usuario(int id)
        {
            Seg_Tipo_Usuario seg_Tipo_Usuario = await db.Seg_Tipo_Usuario.FindAsync(id);
            if (seg_Tipo_Usuario == null)
            {
                return NotFound();
            }

            return Ok(seg_Tipo_Usuario);
        }

        // PUT: api/Seg_Tipo_Usuario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSeg_Tipo_Usuario(int id, Seg_Tipo_Usuario seg_Tipo_Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seg_Tipo_Usuario.IdSegTipoUsuario)
            {
                return BadRequest();
            }

            db.Entry(seg_Tipo_Usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Seg_Tipo_UsuarioExists(id))
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

        // POST: api/Seg_Tipo_Usuario
        [ResponseType(typeof(Seg_Tipo_Usuario))]
        public async Task<IHttpActionResult> PostSeg_Tipo_Usuario(Seg_Tipo_Usuario seg_Tipo_Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seg_Tipo_Usuario.Add(seg_Tipo_Usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = seg_Tipo_Usuario.IdSegTipoUsuario }, seg_Tipo_Usuario);
        }

        // DELETE: api/Seg_Tipo_Usuario/5
        [ResponseType(typeof(Seg_Tipo_Usuario))]
        public async Task<IHttpActionResult> DeleteSeg_Tipo_Usuario(int id)
        {
            Seg_Tipo_Usuario seg_Tipo_Usuario = await db.Seg_Tipo_Usuario.FindAsync(id);
            if (seg_Tipo_Usuario == null)
            {
                return NotFound();
            }

            db.Seg_Tipo_Usuario.Remove(seg_Tipo_Usuario);
            await db.SaveChangesAsync();

            return Ok(seg_Tipo_Usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Seg_Tipo_UsuarioExists(int id)
        {
            return db.Seg_Tipo_Usuario.Count(e => e.IdSegTipoUsuario == id) > 0;
        }
    }
}