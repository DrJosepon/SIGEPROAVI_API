namespace SIGEPROAVI_API.DTO
{
    public class Dom_Componente_Electronico_ModificacionDTO
    {
        public int IdDomComponenteElectronico { get; set; }

        public string Topic { get; set; }

        public int IdDomTipoComponenteElectronico { get; set; }
        public int IdGprGalpon { get; set; }
        public int IdGprServicio { get; set; }

        public string UsuarioModificador { get; set; }
        public bool Estado { get; set; }
    }
}