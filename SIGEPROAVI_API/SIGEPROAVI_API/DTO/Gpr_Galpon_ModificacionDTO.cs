namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Galpon_ModificacionDTO
    {
        public int IdGprGalpon { get; set; }
        public int CantidadAves { get; set; }
        public string Descripcion { get; set; }

        public int IdSegTipoUsuario { get; set; }
        public string UsuarioModificador { get; set; }
        public bool Estado { get; set; }
    }
}