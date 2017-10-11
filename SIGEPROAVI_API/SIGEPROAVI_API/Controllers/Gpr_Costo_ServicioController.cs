using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Costo_ServicioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Costo_Servicio/Activo")]
        public IQueryable<Gpr_Costo_Servicio> ListarCostoServicioActivo()
        {
            return db.Gpr_Costo_Servicio.Where(S => S.Estado == true);
        }

        [HttpGet]
        [Route("api/Gpr_Costo_Servicio/Servicio/{idServicio}")]
        public IQueryable<Gpr_Costo_Servicio_ConsultaDTO> BuscarCostoServicioXServicio(int idServicio)
        {
            var consulta = from CS in db.Gpr_Costo_Servicio.Where(CS => CS.IdGprServicio == idServicio)
                           select new Gpr_Costo_Servicio_ConsultaDTO
                           {
                               Costo = CS.Costo,
                               Estado = CS.Estado,
                               Fecha = CS.Fecha,
                               IdGprCostoServicio = CS.IdGprCostoServicio,
                               IdGprServicio = CS.IdGprServicio
                           };

            return consulta.OrderByDescending(C => C.Fecha).ThenBy(C => C.Estado);
        }

        [HttpPost]
        [Route("api/Gpr_Costo_Servicio/Procesar")]
        public async Task<IHttpActionResult> ProcesarCostoServicio(Gpr_Costo_Servicio_EdicionDTO gpr_Costo_ServicioE)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            List<Gpr_Costo_Servicio> costos = db.Gpr_Costo_Servicio.Where(S => S.IdGprServicio == gpr_Costo_ServicioE.IdGprServicio).ToList();

            foreach (Gpr_Costo_Servicio costo in costos)
            {
                costo.Estado = false;
                costo.FechaModificacion = DateTime.Now;
                costo.UsuarioModificador = gpr_Costo_ServicioE.UsuarioCreador;

                db.Entry(costo).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerificarCostoServicio(costo.IdGprCostoServicio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Costo_Servicio_EdicionDTO, Gpr_Costo_Servicio>());

            Gpr_Costo_Servicio gpr_Costo_Servicio = Mapper.Map<Gpr_Costo_Servicio>(gpr_Costo_ServicioE);
            gpr_Costo_Servicio.FechaCreacion = DateTime.Now;
            gpr_Costo_Servicio.Fecha = DateTime.Now;
            gpr_Costo_Servicio.Estado = true;

            db.Gpr_Costo_Servicio.Add(gpr_Costo_Servicio);
            await db.SaveChangesAsync();

            //return Content(HttpStatusCode.Accepted, "Completado.");
            return Ok(BuscarCostoServicioXServicio(gpr_Costo_ServicioE.IdGprServicio));
            //return CreatedAtRoute("DefaultApi", new { id = gpr_Costo_Servicio.IdGprCostoServicio }, gpr_Costo_Servicio);
        }

        private bool VerificarCostoServicio(int id)
        {
            return db.Gpr_Costo_Servicio.Count(e => e.IdGprCostoServicio == id) > 0;
        }
    }
}