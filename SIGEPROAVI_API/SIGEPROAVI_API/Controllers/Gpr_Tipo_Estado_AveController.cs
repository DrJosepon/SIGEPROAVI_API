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
    public class Gpr_Tipo_Estado_AveController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Tipo_Estado_Ave
        public IQueryable<Gpr_Tipo_Estado_Ave> GetGpr_Tipo_Estado_Ave()
        {
            return db.Gpr_Tipo_Estado_Ave;
        }

        // GET: api/Gpr_Tipo_Estado_Ave/5
        [ResponseType(typeof(Gpr_Tipo_Estado_Ave))]
        public async Task<IHttpActionResult> GetGpr_Tipo_Estado_Ave(int id)
        {
            Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_Ave = await db.Gpr_Tipo_Estado_Ave.FindAsync(id);
            if (gpr_Tipo_Estado_Ave == null)
            {
                return NotFound();
            }

            return Ok(gpr_Tipo_Estado_Ave);
        }

        // PUT: api/Gpr_Tipo_Estado_Ave/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Tipo_Estado_Ave(int id, Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_AveM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Tipo_Estado_AveM.IdGprTipoEstadoAve)
            {
                return BadRequest();
            }

            Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_Ave = await db.Gpr_Tipo_Estado_Ave.FindAsync(id);
            gpr_Tipo_Estado_Ave.Estado = gpr_Tipo_Estado_AveM.Estado;
            gpr_Tipo_Estado_Ave.Descripcion = gpr_Tipo_Estado_AveM.Descripcion;

            db.Entry(gpr_Tipo_Estado_Ave).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Tipo_Estado_AveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok(db.Gpr_Tipo_Estado_Ave);
        }

        // POST: api/Gpr_Tipo_Estado_Ave
        //[ResponseType(typeof(Gpr_Tipo_Estado_Ave))]
        //public async Task<IHttpActionResult> PostGpr_Tipo_Estado_Ave(Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_Ave)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Gpr_Tipo_Estado_Ave.Add(gpr_Tipo_Estado_Ave);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = gpr_Tipo_Estado_Ave.IdGprTipoEstadoAve }, gpr_Tipo_Estado_Ave);
        //}
        [ResponseType(typeof(Gpr_Tipo_Estado_Ave))]
        public async Task<IHttpActionResult> PostGpr_Tipo_Estado_Ave(Gpr_Tipo_Estado_Ave_InsercionDTO gpr_Tipo_Estado_AveI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Tipo_Estado_Ave_InsercionDTO, Gpr_Tipo_Estado_Ave>());

            Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_Ave = Mapper.Map<Gpr_Tipo_Estado_Ave>(gpr_Tipo_Estado_AveI);
            gpr_Tipo_Estado_Ave.FechaCreacion = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Tipo_Estado_Ave.Add(gpr_Tipo_Estado_Ave);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Tipo_Estado_Ave.IdGprTipoEstadoAve }, gpr_Tipo_Estado_Ave);
        }

        // DELETE: api/Gpr_Tipo_Estado_Ave/5
        [ResponseType(typeof(Gpr_Tipo_Estado_Ave))]
        public async Task<IHttpActionResult> DeleteGpr_Tipo_Estado_Ave(int id)
        {
            Gpr_Tipo_Estado_Ave gpr_Tipo_Estado_Ave = await db.Gpr_Tipo_Estado_Ave.FindAsync(id);
            if (gpr_Tipo_Estado_Ave == null)
            {
                return NotFound();
            }

            db.Gpr_Tipo_Estado_Ave.Remove(gpr_Tipo_Estado_Ave);
            await db.SaveChangesAsync();

            return Ok(gpr_Tipo_Estado_Ave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Tipo_Estado_AveExists(int id)
        {
            return db.Gpr_Tipo_Estado_Ave.Count(e => e.IdGprTipoEstadoAve == id) > 0;
        }
    }
}