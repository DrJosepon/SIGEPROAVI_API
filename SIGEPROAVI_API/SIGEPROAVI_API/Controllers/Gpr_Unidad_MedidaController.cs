using SIGEPROAVI_API.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Gpr_Unidad_MedidaController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        // GET: api/Gpr_Unidad_Medida
        public IQueryable<Gpr_Unidad_Medida> GetGpr_Unidad_Medida()
        {
            return db.Gpr_Unidad_Medida;
        }

        // GET: api/Gpr_Unidad_Medida/5
        [ResponseType(typeof(Gpr_Unidad_Medida))]
        public async Task<IHttpActionResult> GetGpr_Unidad_Medida(int id)
        {
            Gpr_Unidad_Medida gpr_Unidad_Medida = await db.Gpr_Unidad_Medida.FindAsync(id);
            if (gpr_Unidad_Medida == null)
            {
                return NotFound();
            }

            return Ok(gpr_Unidad_Medida);
        }

        // PUT: api/Gpr_Unidad_Medida/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGpr_Unidad_Medida(int id, Gpr_Unidad_Medida gpr_Unidad_Medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gpr_Unidad_Medida.IdGprUnidadMedida)
            {
                return BadRequest();
            }

            db.Entry(gpr_Unidad_Medida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Gpr_Unidad_MedidaExists(id))
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

        // POST: api/Gpr_Unidad_Medida
        [ResponseType(typeof(Gpr_Unidad_Medida))]
        public async Task<IHttpActionResult> PostGpr_Unidad_Medida(Gpr_Unidad_Medida gpr_Unidad_Medida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gpr_Unidad_Medida.Add(gpr_Unidad_Medida);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gpr_Unidad_Medida.IdGprUnidadMedida }, gpr_Unidad_Medida);
        }

        // DELETE: api/Gpr_Unidad_Medida/5
        [ResponseType(typeof(Gpr_Unidad_Medida))]
        public async Task<IHttpActionResult> DeleteGpr_Unidad_Medida(int id)
        {
            Gpr_Unidad_Medida gpr_Unidad_Medida = await db.Gpr_Unidad_Medida.FindAsync(id);
            if (gpr_Unidad_Medida == null)
            {
                return NotFound();
            }

            db.Gpr_Unidad_Medida.Remove(gpr_Unidad_Medida);
            await db.SaveChangesAsync();

            return Ok(gpr_Unidad_Medida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Gpr_Unidad_MedidaExists(int id)
        {
            return db.Gpr_Unidad_Medida.Count(e => e.IdGprUnidadMedida == id) > 0;
        }
    }
}