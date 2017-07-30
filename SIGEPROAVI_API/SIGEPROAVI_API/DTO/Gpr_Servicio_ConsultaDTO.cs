namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Servicio_ConsultaDTO
    {
        public int IdGprServicio { get; set; }
        public string Descripcion { get; set; }
        public int? IdGprUnidadMedida { get; set; }
        public int IdGprTipoServicio { get; set; }

        public string DescripcionUnidadMedida { get; set; }
        public string DescripcionTipoServicio { get; set; }
    }
}