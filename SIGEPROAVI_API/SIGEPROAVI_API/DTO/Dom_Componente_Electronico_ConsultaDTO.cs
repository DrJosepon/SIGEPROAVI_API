namespace SIGEPROAVI_API.DTO
{
    public class Dom_Componente_Electronico_ConsultaDTO
    {
        public int IdDomComponenteElectronico { get; set; }
        public string Topic { get; set; }
        public int IdDomTipoComponenteElectronico { get; set; }
        public int IdGprGalpon { get; set; }
        public int IdGprServicio { get; set; }
        public int IdGprTipoServicio { get; set; }

        public string DescripcionTipoComponenteElectronico { get; set; }
        public string DescripcionServicio { get; set; }
        public string DescripcionTipoServicio { get; set; }

        public bool Estado { get; set; }
    }
}