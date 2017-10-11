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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGEPROAVI_API.Controllers
{
    public class Seg_UsuarioController : ApiController
    {
        private SIGEPROAVI_APIContext db = new SIGEPROAVI_APIContext();

        [HttpGet]
        [Route("api/Seg_Usuario")]
        public IQueryable<Seg_Usuario_ConsultaDTO> ListarUsuario()
        {
            var consulta = from U in db.Seg_Usuario
                           from TU in db.Seg_Tipo_Usuario.Where(TU => TU.IdSegTipoUsuario == U.IdSegTipoUsuario)
                           select new Seg_Usuario_ConsultaDTO
                           {
                               ApellidoMaterno = U.ApellidoMaterno,
                               IdSegTipoUsuario = U.IdSegTipoUsuario,
                               ApellidoPaterno = U.ApellidoPaterno,
                               Clave = U.Clave,
                               DescripcionTipoUsuario = TU.Descripcion,
                               IdSegUsuario = U.IdSegUsuario,
                               Nombres = U.Nombres,
                               Usuario = U.Usuario,
                               Estado = U.Estado,
                           };

            return consulta.OrderByDescending(U => U.Estado);
        }

        public IQueryable<Seg_Usuario_ConsultaDTO> BuscarUsuarioXID(int id)
        {
            var consulta = from U in db.Seg_Usuario.Where(U => U.IdSegUsuario == id)
                           from TU in db.Seg_Tipo_Usuario.Where(TU => TU.IdSegTipoUsuario == U.IdSegTipoUsuario)
                           select new Seg_Usuario_ConsultaDTO
                           {
                               ApellidoMaterno = U.ApellidoMaterno,
                               IdSegTipoUsuario = U.IdSegTipoUsuario,
                               ApellidoPaterno = U.ApellidoPaterno,
                               Clave = U.Clave,
                               DescripcionTipoUsuario = TU.Descripcion,
                               IdSegUsuario = U.IdSegUsuario,
                               Nombres = U.Nombres,
                               Usuario = U.Usuario,
                               Estado = U.Estado
                           };

            return consulta;
        }

        [HttpPut]
        [Route("api/Seg_Usuario/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ModificarUsuario(int id, Seg_Usuario_ModificacionDTO seg_UsuarioM)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seg_UsuarioM.IdSegUsuario)
            {
                return BadRequest();
            }

            Seg_Usuario seg_Usuario = await db.Seg_Usuario.FindAsync(id);
            //seg_Usuario.Estado = seg_UsuarioM.Estado;
            seg_Usuario.FechaModificacion = DateTime.Now;
            seg_Usuario.Nombres = seg_UsuarioM.Nombres;
            seg_Usuario.ApellidoMaterno = seg_UsuarioM.ApellidoMaterno;
            seg_Usuario.ApellidoPaterno = seg_UsuarioM.ApellidoPaterno;
            seg_Usuario.IdSegTipoUsuario = seg_UsuarioM.IdSegTipoUsuario;
            seg_Usuario.UsuarioModificador = seg_UsuarioM.UsuarioModificador;

            if (seg_Usuario.Clave != seg_UsuarioM.Clave)
            {
                seg_Usuario.Clave = MD5Hash(seg_UsuarioM.Clave);
            }

            db.Entry(seg_Usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarUsuario(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);

            //Seg_Usuario_ConsultaDTO resultado = GetSeg_Usuario();

            //return Ok(db.Seg_Usuario);
            return Ok(ListarUsuario());
        }

        [HttpPut]
        [Route("api/Seg_Usuario/Desactivar")]
        public async Task<IHttpActionResult> DesactivarUsuario(Seg_Usuario_ModificacionDTO seg_UsuarioM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Seg_Usuario seg_Usuario = await db.Seg_Usuario.FindAsync(seg_UsuarioM.IdSegUsuario);
            seg_Usuario.Estado = false;
            seg_Usuario.FechaModificacion = DateTime.Now;
            seg_Usuario.UsuarioModificador = seg_UsuarioM.UsuarioModificador;

            db.Entry(seg_Usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificarUsuario(seg_UsuarioM.IdSegUsuario))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ListarUsuario());
        }

        [HttpPost]
        [Route("api/Seg_Usuario")]
        [ResponseType(typeof(Seg_Usuario))]
        public async Task<IHttpActionResult> GuardarUsuario(Seg_Usuario_InsercionDTO seg_UsuarioI)
        {
            List<Seg_Usuario> usuarios = db.Seg_Usuario.Where(X => X.Estado == true).ToList();

            foreach (Seg_Usuario usuario in usuarios)
            {
                if (usuario.Usuario != seg_UsuarioI.Usuario)
                {
                    return Content(HttpStatusCode.BadRequest, "Ya existe un usuario activo con este nombre de usuario.");
                }
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Seg_Usuario_InsercionDTO, Seg_Usuario>());

            Seg_Usuario seg_Usuario = Mapper.Map<Seg_Usuario>(seg_UsuarioI);
            seg_Usuario.FechaCreacion = DateTime.Now;
            seg_Usuario.Clave = MD5Hash(seg_UsuarioI.Clave);
            seg_Usuario.Estado = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seg_Usuario.Add(seg_Usuario);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = seg_Usuario.IdSegUsuario }, GetSeg_Usuario(seg_Usuario.IdSegUsuario));
            return Ok(BuscarUsuarioXID(seg_Usuario.IdSegUsuario));
        }

        private bool VerificarUsuario(int id)
        {
            return db.Seg_Usuario.Count(e => e.IdSegUsuario == id) > 0;
        }

        [HttpPost]
        [Route("api/Seg_Usuario/Login")]
        [ResponseType(typeof(Seg_Usuario))]
        public async Task<IHttpActionResult> Login(Seg_Usuario seg_UsuarioL)
        {
            string clave = MD5Hash(seg_UsuarioL.Clave);

            Seg_Usuario seg_Usuario = await db.Seg_Usuario.Where(u => u.Usuario == seg_UsuarioL.Usuario && u.Clave == clave).FirstOrDefaultAsync();
            if (seg_Usuario == null)
            {
                return NotFound();
            }

            return Ok(seg_Usuario);
        }

        public static string MD5Hash(string text)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

                //get hash result after compute it
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }

                return strBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}