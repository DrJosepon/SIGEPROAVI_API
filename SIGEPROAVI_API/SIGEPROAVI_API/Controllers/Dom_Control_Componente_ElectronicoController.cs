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
    public class Dom_Control_Componente_ElectronicoController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Dom_Control_Componente_Electronico")]
        public IQueryable<Dom_Control_Componente_Electronico> ListarControlComponenteElectronico()
        {
            return db.Dom_Control_Componente_Electronico;
        }

        [HttpGet]
        [Route("api/Dom_Control_Componente_Electronico/ComponenteElectronico/{idComponenteElectronico}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Dom_Control_Componente_Electronico_ConsultaDTO> BuscarControlComponenteElectronicoXComponenteElectronico(int idComponenteElectronico)
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

        [HttpPost]
        [Route("api/Dom_Control_Componente_Electronico")]
        [ResponseType(typeof(Dom_Control_Componente_Electronico))]
        public async Task<IHttpActionResult> GuardarControlComponenteElectronico(Dom_Control_Componente_Electronico_InsercionDTO dom_Control_Componente_ElectronicoI)
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

                //HORA
                if (control.IdDomTipoControlComponenteElectronico == 3)
                {
                    if ((Convert.ToDateTime(control.Inicio) <= Convert.ToDateTime(dom_Control_Componente_ElectronicoI.Inicio) && Convert.ToDateTime(control.Fin) > Convert.ToDateTime(dom_Control_Componente_ElectronicoI.Inicio)) || (Convert.ToDateTime(control.Inicio) < Convert.ToDateTime(dom_Control_Componente_ElectronicoI.Fin) && Convert.ToDateTime(control.Fin) >= Convert.ToDateTime(dom_Control_Componente_ElectronicoI.Fin)))
                    {
                        return Content(HttpStatusCode.BadRequest, "No pueden haber cruces de horario.");
                    }
                }
                //RESTO
                else if (control.IdDomTipoControlComponenteElectronico == 2 || control.IdDomTipoControlComponenteElectronico == 1)
                {
                    if ((Double.Parse(control.Inicio) >= Double.Parse(dom_Control_Componente_ElectronicoI.Inicio) && Double.Parse(control.Fin) < Double.Parse(dom_Control_Componente_ElectronicoI.Inicio)) || (Double.Parse(control.Inicio) > Double.Parse(dom_Control_Componente_ElectronicoI.Fin) && Double.Parse(control.Fin) <= Double.Parse(dom_Control_Componente_ElectronicoI.Fin)))
                    {
                        return Content(HttpStatusCode.BadRequest, "No pueden haber cruces de límites.");
                    }
                }
            }

            db.Dom_Control_Componente_Electronico.Add(dom_Control_Componente_Electronico);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = dom_Componente_Electronico.IdDomComponenteElectronico }, dom_Componente_Electronico);
            return Ok(BuscarControlComponenteElectronicoXComponenteElectronico(dom_Control_Componente_Electronico.IdDomControlComponenteElectronico));
        }

        private bool VerificarControlComponenteElectronico(int id)
        {
            return db.Dom_Control_Componente_Electronico.Count(e => e.IdDomControlComponenteElectronico == id) > 0;
        }
    }
}