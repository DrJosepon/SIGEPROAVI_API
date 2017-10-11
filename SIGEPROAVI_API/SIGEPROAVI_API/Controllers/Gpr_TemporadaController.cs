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
    public class Gpr_TemporadaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Gpr_Temporada/Galpon/{idGalpon}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Temporada> BuscarTemporadaXGalpon(int idGalpon)
        {
            return db.Gpr_Temporada.Where(X => X.IdGprGalpon == idGalpon);
        }

        [HttpPut]
        [Route("api/Gpr_Temporada/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ModificarTemporada(int id, Gpr_Temporada_ModificacionDTO gpr_TemporadaM)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_TemporadaM.IdGprTemporada)
            {
                return BadRequest();
            }

            Gpr_Temporada gpr_Temporada = await db.Gpr_Temporada.FindAsync(id);
            gpr_Temporada.Estado = gpr_TemporadaM.Estado;
            gpr_Temporada.FechaModificacion = DateTime.Now;
            gpr_Temporada.Descripcion = gpr_TemporadaM.Descripcion;
            gpr_Temporada.CantidadAves = gpr_TemporadaM.CantidadAves;
            gpr_Temporada.FechaInicio = gpr_TemporadaM.FechaInicio;
            gpr_Temporada.CostoInicial = gpr_TemporadaM.CostoInicial;
            gpr_Temporada.FechaFin = gpr_TemporadaM.FechaFin;
            gpr_Temporada.TotalVenta = gpr_TemporadaM.TotalVenta;
            gpr_Temporada.UsuarioModificador = gpr_TemporadaM.UsuarioModificador;

            if (gpr_Temporada.FechaFin < gpr_Temporada.FechaCreacion)
            {
                return Content(HttpStatusCode.BadRequest, "La fecha de fin no debe ser inferior a la fecha de creación.");
            }

            db.Entry(gpr_Temporada).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarTemporada(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(BuscarTemporadaXGalpon(gpr_Temporada.IdGprGalpon));
        }

        [HttpPost]
        [Route("api/Gpr_Temporada")]
        public async Task<IHttpActionResult> GuardarTemporada(Gpr_Temporada_InsercionDTO gpr_TemporadaI)
        {
            List<Gpr_Temporada> temporadasActivas = db.Gpr_Temporada.Where(X => X.IdGprGalpon == gpr_TemporadaI.IdGprGalpon && X.FechaFin == null).ToList();
            Gpr_Galpon galpon = db.Gpr_Galpon.Where(X => X.IdGprGalpon == gpr_TemporadaI.IdGprGalpon).FirstOrDefault();

            if (temporadasActivas.Count > 0)
            {
                return Content(HttpStatusCode.BadRequest, "Sólo puede haber una temporada activa por galpón, ingrese una fecha de fin para la temporada actual.");
            }

            if (galpon.CantidadAves < gpr_TemporadaI.CantidadAves)
            {
                return Content(HttpStatusCode.BadRequest, "La cantidad de aves durante la temporada, no debe superar la capacidad del galpón.");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Temporada_InsercionDTO, Gpr_Temporada>());

            Gpr_Temporada gpr_Temporada = Mapper.Map<Gpr_Temporada>(gpr_TemporadaI);
            gpr_Temporada.FechaCreacion = DateTime.Now;
            gpr_Temporada.Estado = true;
            gpr_Temporada.UsuarioCreador = gpr_TemporadaI.UsuarioCreador;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Temporada.Add(gpr_Temporada);
            await db.SaveChangesAsync();

            Gpr_Estado_Ave estadoAve = new Gpr_Estado_Ave();
            estadoAve.CantidadAves = gpr_TemporadaI.CantidadAves;
            estadoAve.Estado = true;
            estadoAve.FechaCreacion = DateTime.Now;
            estadoAve.IdGprTemporada = gpr_Temporada.IdGprTemporada;
            estadoAve.IdGprTipoEstadoAve = 1;

            db.Gpr_Estado_Ave.Add(estadoAve);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Temporada.IdGprTemporada }, gpr_Temporada);
            //return Ok(GetGpr_Temporada(gpr_Temporada.IdGprTemporada));
        }

        private bool VerificarTemporada(int id)
        {
            return db.Gpr_Temporada.Count(e => e.IdGprTemporada == id) > 0;
        }
    }
}