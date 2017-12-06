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

        [HttpGet]
        [Route("api/Gpr_Servicio")]
        public IQueryable<Gpr_Servicio_ConsultaDTO> ListarServicio()
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

        [HttpPut]
        [Route("api/Gpr_Servicio/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ModificarServicio(int id, Gpr_Servicio gpr_Servicio)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != gpr_Servicio.IdGprServicio)
            //{
            //    return BadRequest();
            //}
            Gpr_Servicio gpr_servicio = await db.Gpr_Servicio.FindAsync(id);
            gpr_Servicio.FechaModificacion = DateTime.Now;

            db.Entry(gpr_Servicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarServicio(id))
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

        [HttpPost]
        [Route("api/Gpr_Servicio")]
        [ResponseType(typeof(Gpr_Servicio))]
        public async Task<IHttpActionResult> GuardarServicio(Gpr_Servicio_InsercionDTO gpr_ServicioI)
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
            return Ok(ListarServicio());
        }

        private bool VerificarServicio(int id)
        {
            return db.Gpr_Servicio.Count(e => e.IdGprServicio == id) > 0;
        }
    }
}