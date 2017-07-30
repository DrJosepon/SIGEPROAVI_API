namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Servicio_InsercionDTO
    {
        public string Descripcion { get; set; }
        public int? IdGprUnidadMedida { get; set; }
        public int IdGprTipoServicio { get; set; }

        public string UsuarioCreador { get; set; }
    }
}