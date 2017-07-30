namespace SIGEPROAVI_API.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SIGEPROAVI_API.Models.SIGEPROAVI_APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SIGEPROAVI_API.Models.SIGEPROAVI_APIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Seg_Tipo_Usuario.AddOrUpdate(x => x.IdSegTipoUsuario,
                new Seg_Tipo_Usuario() { IdSegTipoUsuario = 1, Descripcion = "Administrador" },
                new Seg_Tipo_Usuario() { IdSegTipoUsuario = 2, Descripcion = "Control" },
                new Seg_Tipo_Usuario() { IdSegTipoUsuario = 3, Descripcion = "Gestion" }
                );

            context.Seg_Usuario.AddOrUpdate(x => x.IdSegUsuario,
                new Seg_Usuario() { IdSegUsuario = 1, Nombres = "Administrador", ApellidoPaterno = "Administrador", ApellidoMaterno = "Administrador", Usuario = "admin", Clave = "e10adc3949ba59abbe56e057f20f883e", Estado = true, IdSegTipoUsuario = 1, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
                );

            context.Dom_Tipo_Control_Componente_Electronico.AddOrUpdate(x => x.IdDomTipoControlComponenteElectronico,
              new Dom_Tipo_Control_Componente_Electronico() { IdDomTipoControlComponenteElectronico = 1, Descripcion = "Humedad", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
              new Dom_Tipo_Control_Componente_Electronico() { IdDomTipoControlComponenteElectronico = 2, Descripcion = "Temperatura", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
              new Dom_Tipo_Control_Componente_Electronico() { IdDomTipoControlComponenteElectronico = 3, Descripcion = "Hora", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
            );

            context.Dom_Tipo_Componente_Electronico.AddOrUpdate(x => x.IdDomTipoComponenteElectronico,
                new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 1, Descripcion = "Sensor de Temperatura", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 2, Descripcion = "Sensor de Humedad", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 3, Descripcion = "Distribuidor de Alimento", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 4, Descripcion = "Distribuidor de Bebida", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 5, Descripcion = "Deshumidificador", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 6, Descripcion = "Controlador de Luz", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 7, Descripcion = "Ventilador", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 8, Descripcion = "Medidor de Corriente", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 9, Descripcion = "Medidor de Alimento", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
            new Dom_Tipo_Componente_Electronico() { IdDomTipoComponenteElectronico = 10, Descripcion = "Medidor de Bebida", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
           );

            context.Gpr_Unidad_Medida.AddOrUpdate(x => x.IdGprUnidadMedida,
                new Gpr_Unidad_Medida() { IdGprUnidadMedida = 1, Descripcion = "Kilowatt/Hora", Simbolo = "KwH", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Unidad_Medida() { IdGprUnidadMedida = 2, Descripcion = "Kilogramo", Simbolo = "Kg", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Unidad_Medida() { IdGprUnidadMedida = 3, Descripcion = "Litro", Simbolo = "L", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Unidad_Medida() { IdGprUnidadMedida = 4, Descripcion = "Grado Centígrado", Simbolo = "ºC", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Unidad_Medida() { IdGprUnidadMedida = 5, Descripcion = "Porcentaje", Simbolo = "%", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
                );

            context.Gpr_Tipo_Servicio.AddOrUpdate(x => x.IdGprTipoServicio,
                new Gpr_Tipo_Servicio() { IdGprTipoServicio = 1, Descripcion = "Medicion", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Tipo_Servicio() { IdGprTipoServicio = 2, Descripcion = "Consumo", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Tipo_Servicio() { IdGprTipoServicio = 3, Descripcion = "Control", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
                );

            context.Gpr_Servicio.AddOrUpdate(x => x.IdGprServicio,
                new Gpr_Servicio() { IdGprServicio = 1, IdGprTipoServicio = 1, IdGprUnidadMedida = 4, Descripcion = "Medición de Temperatura", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 2, IdGprTipoServicio = 1, IdGprUnidadMedida = 5, Descripcion = "Medición de Humedad", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 3, IdGprTipoServicio = 2, IdGprUnidadMedida = 1, Descripcion = "Consumo de Corriente", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 4, IdGprTipoServicio = 2, IdGprUnidadMedida = 2, Descripcion = "Consumo de Alimento", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 5, IdGprTipoServicio = 2, IdGprUnidadMedida = 3, Descripcion = "Consumo de Bebida", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 6, IdGprTipoServicio = 3, IdGprUnidadMedida = null, Descripcion = "Control de Luces", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 7, IdGprTipoServicio = 3, IdGprUnidadMedida = null, Descripcion = "Control de Ventiladores", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 8, IdGprTipoServicio = 3, IdGprUnidadMedida = null, Descripcion = "Control de Deshumidificador", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 9, IdGprTipoServicio = 3, IdGprUnidadMedida = null, Descripcion = "Control de Distribuidor de Alimento", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Servicio() { IdGprServicio = 10, IdGprTipoServicio = 3, IdGprUnidadMedida = null, Descripcion = "Control de Distribuidor de Bebida", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
                );

            context.Gpr_Tipo_Estado_Ave.AddOrUpdate(x => x.IdGprTipoEstadoAve,
                new Gpr_Tipo_Estado_Ave() { IdGprTipoEstadoAve = 1, Descripcion = "Normal", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Tipo_Estado_Ave() { IdGprTipoEstadoAve = 2, Descripcion = "Enferma", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now },
                new Gpr_Tipo_Estado_Ave() { IdGprTipoEstadoAve = 3, Descripcion = "Muerta", Estado = true, UsuarioCreador = "admin", FechaCreacion = DateTime.Now }
                );
        }
    }
}