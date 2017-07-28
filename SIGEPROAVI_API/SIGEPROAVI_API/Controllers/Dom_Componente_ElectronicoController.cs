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
    public class Dom_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Dom_Componente_Electronico
        public IQueryable<Dom_Componente_Electronico> GetDom_Componente_Electronico()
        {
            return db.Dom_Componente_Electronico;
        }

        [HttpGet]
        [Route("api/Dom_Componente_Electronico/Galpon/{idGalpon}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Dom_Componente_Electronico_ConsultaDTO> BuscarComponenteElectronicoXGalpon(int idGalpon)
        {
            var consulta = from CE in db.Dom_Componente_Electronico.Where(CE => CE.IdGprGalpon == idGalpon)
                           from TCE in db.Dom_Tipo_Componente_Electronico.Where(TCE => TCE.IdDomTipoComponenteElectronico == CE.IdDomTipoComponenteElectronico)
                           from S in db.Gpr_Servicio.Where(S => S.IdGprServicio == CE.IdGprServicio)
                           from TS in db.Gpr_Tipo_Servicio.Where(TS => TS.IdGprTipoServicio == S.IdGprTipoServicio)
                           select new Dom_Componente_Electronico_ConsultaDTO
                           {
                               IdGprServicio = CE.IdGprServicio,
                               DescripcionServicio = S.Descripcion,
                               DescripcionTipoComponenteElectronico = TCE.Descripcion,
                               IdDomComponenteElectronico = CE.IdDomComponenteElectronico,
                               IdDomTipoComponenteElectronico = CE.IdDomTipoComponenteElectronico,
                               IdGprGalpon = CE.IdGprGalpon,
                               Topic = CE.Topic,
                               IdGprTipoServicio = S.IdGprTipoServicio,
                               DescripcionTipoServicio = TS.Descripcion,
                           };

            return consulta;
        }

        // GET: api/Dom_Componente_Electronico/5
        //[ResponseType(typeof(Dom_Componente_Electronico))]
        //public async Task<IHttpActionResult> GetDom_Componente_Electronico(int id)
        //{
        //    Dom_Componente_Electronico dom_Componente_Electronico = await db.Dom_Componente_Electronico.FindAsync(id);
        //    if (dom_Componente_Electronico == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(dom_Componente_Electronico);
        //}
        public IQueryable<Dom_Componente_Electronico_ConsultaDTO> GetDom_Componente_Electronico(int id)
        {
            var consulta = from CE in db.Dom_Componente_Electronico.Where(CE => CE.IdDomComponenteElectronico == id)
                           from TCE in db.Dom_Tipo_Componente_Electronico.Where(TCE => TCE.IdDomTipoComponenteElectronico == CE.IdDomTipoComponenteElectronico)
                           from S in db.Gpr_Servicio.Where(S => S.IdGprServicio == CE.IdGprServicio)
                           select new Dom_Componente_Electronico_ConsultaDTO
                           {
                               IdGprServicio = CE.IdGprServicio,
                               DescripcionServicio = S.Descripcion,
                               DescripcionTipoComponenteElectronico = TCE.Descripcion,
                               IdDomComponenteElectronico = CE.IdDomComponenteElectronico,
                               IdDomTipoComponenteElectronico = CE.IdDomTipoComponenteElectronico,
                               IdGprGalpon = CE.IdGprGalpon,
                               Topic = CE.Topic,
                           };

            return consulta;
        }

        // PUT: api/Dom_Componente_Electronico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDom_Componente_Electronico(int id, Dom_Componente_Electronico_ModificacionDTO dom_Componente_ElectronicoM)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dom_Componente_ElectronicoM.IdDomComponenteElectronico)
            {
                return BadRequest();
            }

            Dom_Componente_Electronico dom_Componente_Electronico = await db.Dom_Componente_Electronico.FindAsync(id);
            dom_Componente_Electronico.Estado = dom_Componente_ElectronicoM.Estado;
            dom_Componente_Electronico.FechaModificacion = DateTime.Now;
            dom_Componente_Electronico.IdDomTipoComponenteElectronico = dom_Componente_ElectronicoM.IdDomTipoComponenteElectronico;
            dom_Componente_Electronico.IdGprServicio = dom_Componente_ElectronicoM.IdGprServicio;
            dom_Componente_Electronico.Topic = dom_Componente_ElectronicoM.Topic;
            dom_Componente_Electronico.UsuarioModificador = dom_Componente_ElectronicoM.UsuarioModificador;

            db.Entry(dom_Componente_Electronico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Dom_Componente_ElectronicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(BuscarComponenteElectronicoXGalpon(dom_Componente_Electronico.IdGprGalpon));
        }

        // POST: api/Dom_Componente_Electronico
        //[ResponseType(typeof(Dom_Componente_Electronico))]
        //public async Task<IHttpActionResult> PostDom_Componente_Electronico(Dom_Componente_Electronico dom_Componente_Electronico)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Dom_Componente_Electronico.Add(dom_Componente_Electronico);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = dom_Componente_Electronico.IdDomComponenteElectronico }, dom_Componente_Electronico);
        //}
        public async Task<IHttpActionResult> PostDom_Componente_Electronico(Dom_Componente_Electronico_InsercionDTO dom_Componente_ElectronicoI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Dom_Componente_Electronico_InsercionDTO, Dom_Componente_Electronico>());

            Dom_Componente_Electronico dom_Componente_Electronico = Mapper.Map<Dom_Componente_Electronico>(dom_Componente_ElectronicoI);
            dom_Componente_Electronico.FechaCreacion = DateTime.Now;
            dom_Componente_Electronico.Estado = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dom_Componente_Electronico.Add(dom_Componente_Electronico);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = dom_Componente_Electronico.IdDomComponenteElectronico }, dom_Componente_Electronico);
            return Ok(GetDom_Componente_Electronico(dom_Componente_Electronico.IdDomComponenteElectronico));
        }

        // DELETE: api/Dom_Componente_Electronico/5
        [ResponseType(typeof(Dom_Componente_Electronico))]
        public async Task<IHttpActionResult> DeleteDom_Componente_Electronico(int id)
        {
            Dom_Componente_Electronico dom_Componente_Electronico = await db.Dom_Componente_Electronico.FindAsync(id);
            if (dom_Componente_Electronico == null)
            {
                return NotFound();
            }

            db.Dom_Componente_Electronico.Remove(dom_Componente_Electronico);
            await db.SaveChangesAsync();

            return Ok(dom_Componente_Electronico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Dom_Componente_ElectronicoExists(int id)
        {
            return db.Dom_Componente_Electronico.Count(e => e.IdDomComponenteElectronico == id) > 0;
        }
    }
}