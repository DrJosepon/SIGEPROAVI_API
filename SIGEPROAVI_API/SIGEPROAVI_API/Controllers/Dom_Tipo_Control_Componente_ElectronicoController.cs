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
    public class Dom_Tipo_Control_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Dom_Tipo_Control_Componente_Electronico
        public IQueryable<Dom_Tipo_Control_Componente_Electronico> GetDom_Tipo_Control_Componente_Electronico()
        {
            return db.Dom_Tipo_Control_Componente_Electronico;
        }

        // GET: api/Dom_Tipo_Control_Componente_Electronico/5
        [ResponseType(typeof(Dom_Tipo_Control_Componente_Electronico))]
        public async Task<IHttpActionResult> GetDom_Tipo_Control_Componente_Electronico(int id)
        {
            Dom_Tipo_Control_Componente_Electronico dom_Tipo_Control_Componente_Electronico = await db.Dom_Tipo_Control_Componente_Electronico.FindAsync(id);
            if (dom_Tipo_Control_Componente_Electronico == null)
            {
                return NotFound();
            }

            return Ok(dom_Tipo_Control_Componente_Electronico);
        }

        // PUT: api/Dom_Tipo_Control_Componente_Electronico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDom_Tipo_Control_Componente_Electronico(int id, Dom_Tipo_Control_Componente_Electronico dom_Tipo_Control_Componente_Electronico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dom_Tipo_Control_Componente_Electronico.IdDomTipoControlComponenteElectronico)
            {
                return BadRequest();
            }

            db.Entry(dom_Tipo_Control_Componente_Electronico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Dom_Tipo_Control_Componente_ElectronicoExists(id))
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

        // POST: api/Dom_Tipo_Control_Componente_Electronico
        [ResponseType(typeof(Dom_Tipo_Control_Componente_Electronico))]
        public async Task<IHttpActionResult> PostDom_Tipo_Control_Componente_Electronico(Dom_Tipo_Control_Componente_Electronico dom_Tipo_Control_Componente_Electronico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dom_Tipo_Control_Componente_Electronico.Add(dom_Tipo_Control_Componente_Electronico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dom_Tipo_Control_Componente_Electronico.IdDomTipoControlComponenteElectronico }, dom_Tipo_Control_Componente_Electronico);
        }

        // DELETE: api/Dom_Tipo_Control_Componente_Electronico/5
        [ResponseType(typeof(Dom_Tipo_Control_Componente_Electronico))]
        public async Task<IHttpActionResult> DeleteDom_Tipo_Control_Componente_Electronico(int id)
        {
            Dom_Tipo_Control_Componente_Electronico dom_Tipo_Control_Componente_Electronico = await db.Dom_Tipo_Control_Componente_Electronico.FindAsync(id);
            if (dom_Tipo_Control_Componente_Electronico == null)
            {
                return NotFound();
            }

            db.Dom_Tipo_Control_Componente_Electronico.Remove(dom_Tipo_Control_Componente_Electronico);
            await db.SaveChangesAsync();

            return Ok(dom_Tipo_Control_Componente_Electronico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Dom_Tipo_Control_Componente_ElectronicoExists(int id)
        {
            return db.Dom_Tipo_Control_Componente_Electronico.Count(e => e.IdDomTipoControlComponenteElectronico == id) > 0;
        }
    }
}