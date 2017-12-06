using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Dom_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Dom_Componente_Electronico")]
        public IQueryable<Dom_Componente_Electronico> ListarComponenteElectronico()
        {
            return db.Dom_Componente_Electronico;
        }

        [HttpGet]
        [Route("api/Dom_Componente_Electronico/Galpon/{idGalpon}")]
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
                               Estado = CE.Estado,
                           };

            return consulta.OrderByDescending(X => X.Estado);
        }

        [HttpGet]
        [Route("api/Dom_Componente_Electronico/{id}")]
        public IQueryable<Dom_Componente_Electronico_ConsultaDTO> BuscarComponenteElectronicoXID(int id)
        {
            var consulta = from CE in db.Dom_Componente_Electronico.Where(CE => CE.IdDomComponenteElectronico == id)
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
                               Estado = CE.Estado,
                           };

            return consulta;
        }

        [HttpPut]
        [Route("api/Dom_Componente_Electronico/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ModificarComponenteElectronico(int id, Dom_Componente_Electronico_ModificacionDTO dom_Componente_ElectronicoM)
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
                if (!VerificarComponenteElectronico(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(BuscarComponenteElectronicoXGalpon(dom_Componente_Electronico.IdGprGalpon));
        }

        [HttpPut]
        [Route("api/Dom_Componente_Electronico/Desactivar")]
        public async Task<IHttpActionResult> DesactivarComponenteElectronico(Dom_Componente_Electronico_ModificacionDTO dom_Componente_ElectronicoM)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dom_Componente_Electronico dom_Componente_Electronico = await db.Dom_Componente_Electronico.FindAsync(dom_Componente_ElectronicoM.IdDomComponenteElectronico);
            dom_Componente_Electronico.Estado = false;
            dom_Componente_Electronico.FechaModificacion = DateTime.Now;
            dom_Componente_Electronico.UsuarioModificador = dom_Componente_ElectronicoM.UsuarioModificador;

            db.Entry(dom_Componente_Electronico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarComponenteElectronico(dom_Componente_ElectronicoM.IdDomComponenteElectronico))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(BuscarComponenteElectronicoXGalpon(dom_Componente_Electronico.IdGprGalpon));
        }

        [HttpPost]
        [Route("api/Dom_Componente_Electronico")]
        public async Task<IHttpActionResult> GuardarComponenteElectronico(Dom_Componente_Electronico_InsercionDTO dom_Componente_ElectronicoI)
        {
            List<Dom_Componente_Electronico> componentes = db.Dom_Componente_Electronico.Where(X => X.Estado == true).ToList();

            foreach (Dom_Componente_Electronico componente in componentes)
            {
                if (componente.Topic == dom_Componente_ElectronicoI.Topic)
                {
                    return Content(HttpStatusCode.BadRequest, "Ya existe un componente activo con este nombre de componente.");
                }
            }

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

            return Ok(BuscarComponenteElectronicoXID(dom_Componente_Electronico.IdDomComponenteElectronico));
        }

        private bool VerificarComponenteElectronico(int id)
        {
            return db.Dom_Componente_Electronico.Count(e => e.IdDomComponenteElectronico == id) > 0;
        }
    }
}