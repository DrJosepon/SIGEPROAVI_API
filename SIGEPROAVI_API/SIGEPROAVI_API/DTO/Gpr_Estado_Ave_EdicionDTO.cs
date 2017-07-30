namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Estado_Ave_EdicionDTO
    {
        public int? IdGprEstadoAve { get; set; }

        public int? CantidadAves { get; set; }

        public string DescripcionEstadoAve { get; set; }

        public int? IdGprTipoEstadoAve { get; set; }

        public int IdGprTemporada { get; set; }

        public string UsuarioCreador { get; set; }
        public string UsuarioModificador { get; set; }
    }
}