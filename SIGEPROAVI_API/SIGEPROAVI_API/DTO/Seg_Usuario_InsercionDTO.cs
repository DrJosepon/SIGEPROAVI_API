namespace SIGEPROAVI_API.DTO
{
    public class Seg_Usuario_InsercionDTO
    {
        public string Nombres { get; set; }

        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int IdSegTipoUsuario { get; set; }

        public string UsuarioCreador { get; set; }
    }
}