using AutoMapper;
using SIGEPROAVI_API.DTO;
using SIGEPROAVI_API.Models;
using System;
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
    public class Gpr_ServicioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Servicio
        //public IQueryable<Gpr_Servicio> GetGpr_Servicio()
        //{
        //    return db.Gpr_Servicio;
        //}
        public IQueryable<Gpr_Servicio_ConsultaDTO> GetGpr_Servicio()
        {
            var consulta = from S in db.Gpr_Servicio
                           from TS in db.Gpr_Tipo_Servicio.Where(TS => TS.IdGprTipoServicio == S.IdGprTipoServicio)
                           from UM in db.Gpr_Unidad_Medida.Where(UM => UM.IdGprUnidadMedida == S.IdGprUnidadMedida).DefaultIfEmpty()
                           select new Gpr_Servicio_ConsultaDTO
                           {
                               Descripcion = S.Descripcion,
                               IdGprUnidadMedida = S.IdGprUnidadMedida,
                               DescripcionTipoServicio = TS.Descripcion,
                               DescripcionUnidadMedida = UM.Descripcion,
                               IdGprServicio = S.IdGprServicio,
                               IdGprTipoServicio = S.IdGprTipoServicio,
                           };

            return consulta;
        }

        // GET: api/Gpr_Servicio/5
        [ResponseType(typeof(Gpr_Servicio))]
        public async Task<IHttpActionResult> GetGpr_Servicio(int id)
        {
            Gpr_Servicio gpr_Servicio = await db.Gpr_Servicio.FindAsync(id);
            if (gpr_Servicio == null)
            {
                return NotFound();
            }

            return Ok(gpr_Servicio);
        }

        // PUT: api/Gpr_Servicio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Servicio(int id, Gpr_Servicio gpr_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Servicio.IdGprServicio)
            {
                return BadRequest();
            }

            db.Entry(gpr_Servicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_ServicioExists(id))
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

        // POST: api/Gpr_Servicio
        [ResponseType(typeof(Gpr_Servicio))]
        public async Task<IHttpActionResult> PostGpr_Servicio(Gpr_Servicio_InsercionDTO gpr_ServicioI)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Gpr_Servicio_InsercionDTO, Gpr_Servicio>());

            Gpr_Servicio gpr_Servicio = Mapper.Map<Gpr_Servicio>(gpr_ServicioI);
            gpr_Servicio.FechaCreacion = DateTime.Now;
            gpr_Servicio.Estado = true;
            gpr_Servicio.UsuarioCreador = gpr_ServicioI.UsuarioCreador;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Servicio.Add(gpr_Servicio);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = gpr_Servicio.IdGprServicio }, gpr_Servicio);
            return Ok(GetGpr_Servicio());
        }

        // DELETE: api/Gpr_Servicio/5
        [ResponseType(typeof(Gpr_Servicio))]
        public async Task<IHttpActionResult> DeleteGpr_Servicio(int id)
        {
            Gpr_Servicio gpr_Servicio = await db.Gpr_Servicio.FindAsync(id);
            if (gpr_Servicio == null)
            {
                return NotFound();
            }

            db.Gpr_Servicio.Remove(gpr_Servicio);
            await db.SaveChangesAsync();

            return Ok(gpr_Servicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_ServicioExists(int id)
        {
            return db.Gpr_Servicio.Count(e => e.IdGprServicio == id) > 0;
        }
    }
}