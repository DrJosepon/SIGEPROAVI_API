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
    public class Gpr_Medicion_DiariaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Medicion_Diaria
        public IQueryable<Gpr_Medicion_Diaria> GetGpr_Medicion_Diaria()
        {
            return db.Gpr_Medicion_Diaria;
        }

        [HttpGet]
        [Route("api/Gpr_Medicion_Diaria/Temporada/{idGalpon}/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Medicion_Diaria_ConsultaDTO> BuscarMedicionDiariaXTemporada(int idGalpon, int idTemporada)
        {
            var consulta = from MD in db.Gpr_Medicion_Diaria.Where(MD => MD.IdGprGalpon == idGalpon && MD.Fecha == Convert.ToDateTime("01-07-2017"))
                           select new Gpr_Medicion_Diaria_ConsultaDTO
                           {
                               Fecha = MD.Fecha,
                               IdGprMedicionDiaria = MD.IdGprMedicionDiaria,
                               Medicion = MD.Medicion,
                               IdGprServicio = MD.IdGprServicio,
                           };

            return consulta;
        }

        [HttpGet]
        [Route("api/Gpr_Medicion_Diaria/Temporada/{idTemporada}")]
        //[ResponseType(typeof(Dom_Componente_ElectronicoConsultaDTO))]
        public IQueryable<Gpr_Medicion_Diaria_ConsultaDTO> BuscarMedicionDiariaXTemporada(int idTemporada)
        {
            Gpr_Temporada temporada = db.Gpr_Temporada.Where(T => T.IdGprTemporada == idTemporada).FirstOrDefault();

            if (temporada.FechaFin == null)
            {
                temporada.FechaFin = DateTime.Now;
            }

            var consulta = from MD in db.Gpr_Medicion_Diaria.Where(MD => MD.IdGprGalpon == temporada.IdGprGalpon && (MD.Fecha >= temporada.FechaInicio && MD.Fecha <= temporada.FechaFin))
                           select new Gpr_Medicion_Diaria_ConsultaDTO
                           {
                               Fecha = MD.Fecha,
                               IdGprMedicionDiaria = MD.IdGprMedicionDiaria,
                               Medicion = MD.Medicion,
                               IdGprServicio = MD.IdGprServicio,
                           };

            return consulta;
        }

        // GET: api/Gpr_Medicion_Diaria/5
        [ResponseType(typeof(Gpr_Medicion_Diaria))]
        public async Task<IHttpActionResult> GetGpr_Medicion_Diaria(int id)
        {
            Gpr_Medicion_Diaria gpr_Medicion_Diaria = await db.Gpr_Medicion_Diaria.FindAsync(id);
            if (gpr_Medicion_Diaria == null)
            {
                return NotFound();
            }

            return Ok(gpr_Medicion_Diaria);
        }

        // PUT: api/Gpr_Medicion_Diaria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Medicion_Diaria(int id, Gpr_Medicion_Diaria gpr_Medicion_Diaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Medicion_Diaria.IdGprMedicionDiaria)
            {
                return BadRequest();
            }

            db.Entry(gpr_Medicion_Diaria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Medicion_DiariaExists(id))
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

        // POST: api/Gpr_Medicion_Diaria
        [ResponseType(typeof(Gpr_Medicion_Diaria))]
        public async Task<IHttpActionResult> PostGpr_Medicion_Diaria(Gpr_Medicion_Diaria_InsercionDTO gpr_Medicion_DiariaI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Medicion_Diaria_InsercionDTO, Gpr_Medicion_Diaria>());
            Gpr_Medicion_Diaria gpr_Medicion_Diaria = Mapper.Map<Gpr_Medicion_Diaria>(gpr_Medicion_DiariaI);
            gpr_Medicion_Diaria.Fecha = Convert.ToDateTime(gpr_Medicion_DiariaI.Fecha);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Medicion_Diaria.Add(gpr_Medicion_Diaria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Medicion_Diaria.IdGprMedicionDiaria }, gpr_Medicion_Diaria);
        }

        // DELETE: api/Gpr_Medicion_Diaria/5
        [ResponseType(typeof(Gpr_Medicion_Diaria))]
        public async Task<IHttpActionResult> DeleteGpr_Medicion_Diaria(int id)
        {
            Gpr_Medicion_Diaria gpr_Medicion_Diaria = await db.Gpr_Medicion_Diaria.FindAsync(id);
            if (gpr_Medicion_Diaria == null)
            {
                return NotFound();
            }

            db.Gpr_Medicion_Diaria.Remove(gpr_Medicion_Diaria);
            await db.SaveChangesAsync();

            return Ok(gpr_Medicion_Diaria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Medicion_DiariaExists(int id)
        {
            return db.Gpr_Medicion_Diaria.Count(e => e.IdGprMedicionDiaria == id) > 0;
        }
    }
}