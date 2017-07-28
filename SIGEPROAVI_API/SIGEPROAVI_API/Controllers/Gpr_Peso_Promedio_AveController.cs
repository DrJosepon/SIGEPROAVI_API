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
    public class Gpr_Peso_Promedio_AveController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Peso_Promedio_Ave
        public IQueryable<Gpr_Peso_Promedio_Ave> GetGpr_Peso_Promedio_Ave()
        {
            return db.Gpr_Peso_Promedio_Ave;
        }

        [HttpGet]
        [Route("api/Gpr_Peso_Promedio_Ave/Temporada/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Peso_Promedio_Ave> BuscarPesoPromedioXTemporada(int idTemporada)
        {
            return db.Gpr_Peso_Promedio_Ave.Where(X => X.IdGprTemporada == idTemporada);
        }

        // GET: api/Gpr_Peso_Promedio_Ave/5
        [ResponseType(typeof(Gpr_Peso_Promedio_Ave))]
        public async Task<IHttpActionResult> GetGpr_Peso_Promedio_Ave(int id)
        {
            Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave = await db.Gpr_Peso_Promedio_Ave.FindAsync(id);
            if (gpr_Peso_Promedio_Ave == null)
            {
                return NotFound();
            }

            return Ok(gpr_Peso_Promedio_Ave);
        }

        // PUT: api/Gpr_Peso_Promedio_Ave/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Peso_Promedio_Ave(int id, Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Peso_Promedio_Ave.IdGprPesoPromedioAve)
            {
                return BadRequest();
            }

            db.Entry(gpr_Peso_Promedio_Ave).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Peso_Promedio_AveExists(id))
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

        // POST: api/Gpr_Peso_Promedio_Ave
        [ResponseType(typeof(Gpr_Peso_Promedio_Ave))]
        //public async Task<IHttpActionResult> PostGpr_Peso_Promedio_Ave(Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Gpr_Peso_Promedio_Ave.Add(gpr_Peso_Promedio_Ave);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = gpr_Peso_Promedio_Ave.IdGprPesoPromedioAve }, gpr_Peso_Promedio_Ave);
        //}
        public async Task<IHttpActionResult> PostGpr_Peso_Promedio_Ave(Gpr_Peso_Promedio_Ave_InsercionDTO gpr_Peso_Promedio_AveI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Peso_Promedio_Ave_InsercionDTO, Gpr_Peso_Promedio_Ave>());

            Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave = Mapper.Map<Gpr_Peso_Promedio_Ave>(gpr_Peso_Promedio_AveI);
            gpr_Peso_Promedio_Ave.FechaCreacion = DateTime.Now;
            gpr_Peso_Promedio_Ave.IdGprTemporada = gpr_Peso_Promedio_AveI.IdGprTemporada;
            gpr_Peso_Promedio_Ave.Peso = gpr_Peso_Promedio_AveI.Peso;
            gpr_Peso_Promedio_Ave.Estado = true;
            gpr_Peso_Promedio_Ave.UsuarioCreador = gpr_Peso_Promedio_AveI.UsuarioCreador;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Gpr_Peso_Promedio_Ave> pesos = db.Gpr_Peso_Promedio_Ave.Where(X => X.IdGprTemporada == gpr_Peso_Promedio_AveI.IdGprTemporada).ToList();

            foreach (Gpr_Peso_Promedio_Ave peso in pesos)
            {
                if (peso.Fecha == gpr_Peso_Promedio_AveI.Fecha)
                {
                    return Content(HttpStatusCode.BadRequest, "No pueden existir dos registros en la misma fecha.");
                }
            }

            db.Gpr_Peso_Promedio_Ave.Add(gpr_Peso_Promedio_Ave);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Peso_Promedio_Ave.IdGprPesoPromedioAve }, gpr_Peso_Promedio_Ave);
        }

        // DELETE: api/Gpr_Peso_Promedio_Ave/5
        [ResponseType(typeof(Gpr_Peso_Promedio_Ave))]
        public async Task<IHttpActionResult> DeleteGpr_Peso_Promedio_Ave(int id)
        {
            Gpr_Peso_Promedio_Ave gpr_Peso_Promedio_Ave = await db.Gpr_Peso_Promedio_Ave.FindAsync(id);
            if (gpr_Peso_Promedio_Ave == null)
            {
                return NotFound();
            }

            db.Gpr_Peso_Promedio_Ave.Remove(gpr_Peso_Promedio_Ave);
            await db.SaveChangesAsync();

            return Ok(gpr_Peso_Promedio_Ave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Peso_Promedio_AveExists(int id)
        {
            return db.Gpr_Peso_Promedio_Ave.Count(e => e.IdGprPesoPromedioAve == id) > 0;
        }
    }
}