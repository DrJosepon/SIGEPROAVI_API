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
    public class Dom_Control_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Dom_Control_Componente_Electronico
        public IQueryable<Dom_Control_Componente_Electronico> GetDom_Control_Componente_Electronico()
        {
            return db.Dom_Control_Componente_Electronico;
        }

        [HttpGet]
        [Route("api/Dom_Control_Componente_Electronico_ConsultaDTO/ComponenteElectronico/{idComponenteElectronico}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Dom_Control_Componente_Electronico_ConsultaDTO> BuscarControlXComponente(int idComponenteElectronico)
        {
            var consulta = from CCE in db.Dom_Control_Componente_Electronico.Where(CCE => CCE.IdDomComponenteElectronico == idComponenteElectronico)
                           from TCCE in db.Dom_Tipo_Control_Componente_Electronico.Where(TCCE => TCCE.IdDomTipoControlComponenteElectronico == CCE.IdDomTipoControlComponenteElectronico)
                           select new Dom_Control_Componente_Electronico_ConsultaDTO
                           {
                               DescripcionTipoControlComponenteElectronico = TCCE.Descripcion,
                               IdDomTipoControlComponenteElectronico = CCE.IdDomTipoControlComponenteElectronico,
                               Fin = CCE.Fin,
                               IdDomComponenteElectronico = CCE.IdDomComponenteElectronico,
                               IdDomControlComponenteElectronico = CCE.IdDomControlComponenteElectronico,
                               Inicio = CCE.Inicio,
                           };

            return consulta;
        }

        // GET: api/Dom_Control_Componente_Electronico/5
        [ResponseType(typeof(Dom_Control_Componente_Electronico))]
        //public async Task<IHttpActionResult> GetDom_Control_Componente_Electronico(int id)
        //{
        //    Dom_Control_Componente_Electronico dom_Control_Componente_Electronico = await db.Dom_Control_Componente_Electronico.FindAsync(id);
        //    if (dom_Control_Componente_Electronico == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(dom_Control_Componente_Electronico);
        //}
        public IQueryable<Dom_Control_Componente_Electronico_ConsultaDTO> GetDom_Control_Componente_Electronico(int id)
        {
            var consulta = from CCE in db.Dom_Control_Componente_Electronico.Where(CCE => CCE.IdDomControlComponenteElectronico == id)
                           from TCCE in db.Dom_Tipo_Control_Componente_Electronico.Where(TCCE => TCCE.IdDomTipoControlComponenteElectronico == CCE.IdDomTipoControlComponenteElectronico)
                           select new Dom_Control_Componente_Electronico_ConsultaDTO
                           {
                               DescripcionTipoControlComponenteElectronico = TCCE.Descripcion,
                               IdDomTipoControlComponenteElectronico = CCE.IdDomTipoControlComponenteElectronico,
                               Fin = CCE.Fin,
                               IdDomComponenteElectronico = CCE.IdDomComponenteElectronico,
                               IdDomControlComponenteElectronico = CCE.IdDomControlComponenteElectronico,
                               Inicio = CCE.Inicio,
                           };

            return consulta;
        }

        // PUT: api/Dom_Control_Componente_Electronico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDom_Control_Componente_Electronico(int id, Dom_Control_Componente_Electronico dom_Control_Componente_Electronico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dom_Control_Componente_Electronico.IdDomControlComponenteElectronico)
            {
                return BadRequest();
            }

            db.Entry(dom_Control_Componente_Electronico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Dom_Control_Componente_ElectronicoExists(id))
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

        // POST: api/Dom_Control_Componente_Electronico
        [ResponseType(typeof(Dom_Control_Componente_Electronico))]
        //public async Task<IHttpActionResult> PostDom_Control_Componente_Electronico(Dom_Control_Componente_Electronico dom_Control_Componente_Electronico)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Dom_Control_Componente_Electronico.Add(dom_Control_Componente_Electronico);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = dom_Control_Componente_Electronico.IdDomControlComponenteElectronico }, dom_Control_Componente_Electronico);
        //}
        public async Task<IHttpActionResult> PostDom_Control_Componente_Electronico(Dom_Control_Componente_Electronico_InsercionDTO dom_Control_Componente_ElectronicoI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Dom_Control_Componente_Electronico_InsercionDTO, Dom_Control_Componente_Electronico>());

            Dom_Control_Componente_Electronico dom_Control_Componente_Electronico = Mapper.Map<Dom_Control_Componente_Electronico>(dom_Control_Componente_ElectronicoI);
            dom_Control_Componente_Electronico.FechaCreacion = DateTime.Now;
            dom_Control_Componente_Electronico.Estado = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Dom_Control_Componente_Electronico> controles = db.Dom_Control_Componente_Electronico.Where(X => X.IdDomComponenteElectronico == dom_Control_Componente_Electronico.IdDomComponenteElectronico).ToList();

            foreach (Dom_Control_Componente_Electronico control in controles)
            {
                if (control.IdDomTipoControlComponenteElectronico != dom_Control_Componente_ElectronicoI.IdDomTipoControlComponenteElectronico)
                {
                    return Content(HttpStatusCode.BadRequest, "Todos los controles deben ser del mismo tipo.");
                }
            }

            db.Dom_Control_Componente_Electronico.Add(dom_Control_Componente_Electronico);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = dom_Componente_Electronico.IdDomComponenteElectronico }, dom_Componente_Electronico);
            return Ok(GetDom_Control_Componente_Electronico(dom_Control_Componente_Electronico.IdDomControlComponenteElectronico));
        }

        // DELETE: api/Dom_Control_Componente_Electronico/5
        [ResponseType(typeof(Dom_Control_Componente_Electronico))]
        public async Task<IHttpActionResult> DeleteDom_Control_Componente_Electronico(int id)
        {
            Dom_Control_Componente_Electronico dom_Control_Componente_Electronico = await db.Dom_Control_Componente_Electronico.FindAsync(id);
            if (dom_Control_Componente_Electronico == null)
            {
                return NotFound();
            }

            db.Dom_Control_Componente_Electronico.Remove(dom_Control_Componente_Electronico);
            await db.SaveChangesAsync();

            return Ok(dom_Control_Componente_Electronico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Dom_Control_Componente_ElectronicoExists(int id)
        {
            return db.Dom_Control_Componente_Electronico.Count(e => e.IdDomControlComponenteElectronico == id) > 0;
        }
    }
}