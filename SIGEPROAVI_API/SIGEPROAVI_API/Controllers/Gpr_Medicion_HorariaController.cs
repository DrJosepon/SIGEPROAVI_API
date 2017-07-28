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
    public class Gpr_Medicion_HorariaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Medicion_Horaria
        public IQueryable<Gpr_Medicion_Horaria> GetGpr_Medicion_Horaria()
        {
            return db.Gpr_Medicion_Horaria;
        }

        [HttpGet]
        [Route("api/Gpr_Medicion_Horaria/Temporada/{idGalpon}/{fecMedicion}/{idServicio}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Medicion_Horaria_ConsultaDTO> BuscarMedicionHorariaXTemporada(int idGalpon, string fecMedicion, int idServicio)
        {
            DateTime fecha = Convert.ToDateTime(fecMedicion);

            var consulta = from MH in db.Gpr_Medicion_Horaria.Where(MH => MH.IdGprGalpon == idGalpon && MH.IdGprServicio == idServicio && DbFunctions.TruncateTime(MH.Fecha) == fecha.Date)
                           select new Gpr_Medicion_Horaria_ConsultaDTO
                           {
                               Fecha = MH.Fecha,
                               IdGprMedicionHoraria = MH.IdGprMedicionHoraria,
                               Medicion = MH.Medicion,
                               Hora = MH.Hora,
                           };

            return consulta;
        }

        // GET: api/Gpr_Medicion_Horaria/5
        [ResponseType(typeof(Gpr_Medicion_Horaria))]
        public async Task<IHttpActionResult> GetGpr_Medicion_Horaria(int id)
        {
            Gpr_Medicion_Horaria gpr_Medicion_Horaria = await db.Gpr_Medicion_Horaria.FindAsync(id);
            if (gpr_Medicion_Horaria == null)
            {
                return NotFound();
            }

            return Ok(gpr_Medicion_Horaria);
        }

        // PUT: api/Gpr_Medicion_Horaria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Medicion_Horaria(int id, Gpr_Medicion_Horaria gpr_Medicion_Horaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Medicion_Horaria.IdGprMedicionHoraria)
            {
                return BadRequest();
            }

            db.Entry(gpr_Medicion_Horaria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Medicion_HorariaExists(id))
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

        // POST: api/Gpr_Medicion_Horaria
        [ResponseType(typeof(Gpr_Medicion_Horaria))]
        public async Task<IHttpActionResult> PostGpr_Medicion_Horaria(Gpr_Medicion_Horaria_InsercionDTO gpr_Medicion_HorariaI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Medicion_Horaria_InsercionDTO, Gpr_Medicion_Horaria>());
            Gpr_Medicion_Horaria gpr_Medicion_Horaria = Mapper.Map<Gpr_Medicion_Horaria>(gpr_Medicion_HorariaI);
            gpr_Medicion_Horaria.Fecha = Convert.ToDateTime(gpr_Medicion_HorariaI.Fecha);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Medicion_Horaria.Add(gpr_Medicion_Horaria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Medicion_Horaria.IdGprMedicionHoraria }, gpr_Medicion_Horaria);
        }

        // DELETE: api/Gpr_Medicion_Horaria/5
        [ResponseType(typeof(Gpr_Medicion_Horaria))]
        public async Task<IHttpActionResult> DeleteGpr_Medicion_Horaria(int id)
        {
            Gpr_Medicion_Horaria gpr_Medicion_Horaria = await db.Gpr_Medicion_Horaria.FindAsync(id);
            if (gpr_Medicion_Horaria == null)
            {
                return NotFound();
            }

            db.Gpr_Medicion_Horaria.Remove(gpr_Medicion_Horaria);
            await db.SaveChangesAsync();

            return Ok(gpr_Medicion_Horaria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Medicion_HorariaExists(int id)
        {
            return db.Gpr_Medicion_Horaria.Count(e => e.IdGprMedicionHoraria == id) > 0;
        }
    }
}