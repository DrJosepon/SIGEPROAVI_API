namespace SIGEPROAVI_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dom_Componente_Electronico",
                c => new
                {
                    IdDomComponenteElectronico = c.Int(nullable: false, identity: true),
                    Topic = c.String(nullable: false, maxLength: 250),
                    IdDomTipoComponenteElectronico = c.Int(nullable: false),
                    IdGprGalpon = c.Int(nullable: false),
                    IdGprServicio = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdDomComponenteElectronico)
                .ForeignKey("dbo.Dom_Tipo_Componente_Electronico", t => t.IdDomTipoComponenteElectronico, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Galpon", t => t.IdGprGalpon, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Servicio", t => t.IdGprServicio, cascadeDelete: false)
                .Index(t => t.IdDomTipoComponenteElectronico)
                .Index(t => t.IdGprGalpon)
                .Index(t => t.IdGprServicio);

            CreateTable(
                "dbo.Dom_Tipo_Componente_Electronico",
                c => new
                {
                    IdDomTipoComponenteElectronico = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdDomTipoComponenteElectronico);

            CreateTable(
                "dbo.Gpr_Galpon",
                c => new
                {
                    IdGprGalpon = c.Int(nullable: false, identity: true),
                    CantidadAves = c.Int(nullable: false),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprGalpon);

            CreateTable(
                "dbo.Gpr_Servicio",
                c => new
                {
                    IdGprServicio = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    IdGprUnidadMedida = c.Int(),
                    IdGprTipoServicio = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprServicio)
                .ForeignKey("dbo.Gpr_Tipo_Servicio", t => t.IdGprTipoServicio, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Unidad_Medida", t => t.IdGprUnidadMedida)
                .Index(t => t.IdGprUnidadMedida)
                .Index(t => t.IdGprTipoServicio);

            CreateTable(
                "dbo.Gpr_Tipo_Servicio",
                c => new
                {
                    IdGprTipoServicio = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprTipoServicio);

            CreateTable(
                "dbo.Gpr_Unidad_Medida",
                c => new
                {
                    IdGprUnidadMedida = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Simbolo = c.String(maxLength: 10),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprUnidadMedida);

            CreateTable(
                "dbo.Dom_Control_Componente_Electronico",
                c => new
                {
                    IdDomControlComponenteElectronico = c.Int(nullable: false, identity: true),
                    Inicio = c.String(nullable: false, maxLength: 250),
                    Fin = c.String(maxLength: 250),
                    IdDomTipoControlComponenteElectronico = c.Int(nullable: false),
                    IdDomComponenteElectronico = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                    Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico = c.Int(),
                })
                .PrimaryKey(t => t.IdDomControlComponenteElectronico)
                .ForeignKey("dbo.Dom_Componente_Electronico", t => t.IdDomComponenteElectronico, cascadeDelete: false)
                .ForeignKey("dbo.Dom_Tipo_Componente_Electronico", t => t.Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico)
                .Index(t => t.IdDomComponenteElectronico)
                .Index(t => t.Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico);

            CreateTable(
                "dbo.Dom_Tipo_Control_Componente_Electronico",
                c => new
                {
                    IdDomTipoControlComponenteElectronico = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdDomTipoControlComponenteElectronico);

            CreateTable(
                "dbo.Gpr_Costo_Servicio",
                c => new
                {
                    IdGprCostoServicio = c.Int(nullable: false, identity: true),
                    Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Fecha = c.DateTime(nullable: false),
                    IdGprServicio = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprCostoServicio)
                .ForeignKey("dbo.Gpr_Servicio", t => t.IdGprServicio, cascadeDelete: false)
                .Index(t => t.IdGprServicio);

            CreateTable(
                "dbo.Gpr_Estado_Ave",
                c => new
                {
                    IdGprEstadoAve = c.Int(nullable: false, identity: true),
                    CantidadAves = c.Int(nullable: false),
                    IdGprTipoEstadoAve = c.Int(nullable: false),
                    IdGprTemporada = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprEstadoAve)
                .ForeignKey("dbo.Gpr_Temporada", t => t.IdGprTemporada, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Tipo_Estado_Ave", t => t.IdGprTipoEstadoAve, cascadeDelete: false)
                .Index(t => t.IdGprTipoEstadoAve)
                .Index(t => t.IdGprTemporada);

            CreateTable(
                "dbo.Gpr_Temporada",
                c => new
                {
                    IdGprTemporada = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    CantidadAves = c.Int(nullable: false),
                    FechaInicio = c.DateTime(nullable: false),
                    CostoInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                    FechaFin = c.DateTime(),
                    TotalVenta = c.Decimal(precision: 18, scale: 2),
                    IdGprGalpon = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprTemporada)
                .ForeignKey("dbo.Gpr_Galpon", t => t.IdGprGalpon, cascadeDelete: false)
                .Index(t => t.IdGprGalpon);

            CreateTable(
                "dbo.Gpr_Tipo_Estado_Ave",
                c => new
                {
                    IdGprTipoEstadoAve = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 250),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprTipoEstadoAve);

            CreateTable(
                "dbo.Gpr_Gasto_Diario",
                c => new
                {
                    IdGprGastoDiario = c.Int(nullable: false, identity: true),
                    Gasto = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IdGprMedicionDiaria = c.Int(nullable: false),
                    IdGprCostoServicio = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.IdGprGastoDiario)
                .ForeignKey("dbo.Gpr_Costo_Servicio", t => t.IdGprCostoServicio, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Medicion_Diaria", t => t.IdGprMedicionDiaria, cascadeDelete: false)
                .Index(t => t.IdGprMedicionDiaria)
                .Index(t => t.IdGprCostoServicio);

            CreateTable(
                "dbo.Gpr_Medicion_Diaria",
                c => new
                {
                    IdGprMedicionDiaria = c.Int(nullable: false, identity: true),
                    Medicion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Fecha = c.DateTime(nullable: false),
                    IdGprServicio = c.Int(nullable: false),
                    IdGprGalpon = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.IdGprMedicionDiaria)
                .ForeignKey("dbo.Gpr_Galpon", t => t.IdGprGalpon, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Servicio", t => t.IdGprServicio, cascadeDelete: false)
                .Index(t => t.IdGprServicio)
                .Index(t => t.IdGprGalpon);

            CreateTable(
                "dbo.Gpr_Medicion_Horaria",
                c => new
                {
                    IdGprMedicionHoraria = c.Int(nullable: false, identity: true),
                    Medicion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Hora = c.Int(nullable: false),
                    Fecha = c.DateTime(nullable: false),
                    IdGprServicio = c.Int(nullable: false),
                    IdGprGalpon = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.IdGprMedicionHoraria)
                .ForeignKey("dbo.Gpr_Galpon", t => t.IdGprGalpon, cascadeDelete: false)
                .ForeignKey("dbo.Gpr_Servicio", t => t.IdGprServicio, cascadeDelete: false)
                .Index(t => t.IdGprServicio)
                .Index(t => t.IdGprGalpon);

            CreateTable(
                "dbo.Gpr_Peso_Promedio_Ave",
                c => new
                {
                    IdGprPesoPromedioAve = c.Int(nullable: false, identity: true),
                    Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Fecha = c.DateTime(nullable: false),
                    IdGprTemporada = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprPesoPromedioAve)
                .ForeignKey("dbo.Gpr_Temporada", t => t.IdGprTemporada, cascadeDelete: false)
                .Index(t => t.IdGprTemporada);

            CreateTable(
                "dbo.Gpr_Raza",
                c => new
                {
                    IdGprRaza = c.Int(nullable: false, identity: true),
                    Nombre = c.String(),
                    PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdGprRaza);

            CreateTable(
                "dbo.Seg_Tipo_Usuario",
                c => new
                {
                    IdSegTipoUsuario = c.Int(nullable: false, identity: true),
                    Descripcion = c.String(nullable: false, maxLength: 150),
                })
                .PrimaryKey(t => t.IdSegTipoUsuario);

            CreateTable(
                "dbo.Seg_Usuario",
                c => new
                {
                    IdSegUsuario = c.Int(nullable: false, identity: true),
                    Nombres = c.String(nullable: false, maxLength: 250),
                    ApellidoMaterno = c.String(nullable: false, maxLength: 150),
                    ApellidoPaterno = c.String(nullable: false, maxLength: 150),
                    Usuario = c.String(nullable: false, maxLength: 15),
                    Clave = c.String(nullable: false, maxLength: 250),
                    IdSegTipoUsuario = c.Int(nullable: false),
                    Estado = c.Boolean(nullable: false),
                    UsuarioCreador = c.String(maxLength: 15),
                    FechaCreacion = c.DateTime(nullable: false),
                    UsuarioModificador = c.String(maxLength: 15),
                    FechaModificacion = c.DateTime(),
                })
                .PrimaryKey(t => t.IdSegUsuario)
                .ForeignKey("dbo.Seg_Tipo_Usuario", t => t.IdSegTipoUsuario, cascadeDelete: false)
                .Index(t => t.IdSegTipoUsuario);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Seg_Usuario", "IdSegTipoUsuario", "dbo.Seg_Tipo_Usuario");
            DropForeignKey("dbo.Gpr_Peso_Promedio_Ave", "IdGprTemporada", "dbo.Gpr_Temporada");
            DropForeignKey("dbo.Gpr_Medicion_Horaria", "IdGprServicio", "dbo.Gpr_Servicio");
            DropForeignKey("dbo.Gpr_Medicion_Horaria", "IdGprGalpon", "dbo.Gpr_Galpon");
            DropForeignKey("dbo.Gpr_Gasto_Diario", "IdGprMedicionDiaria", "dbo.Gpr_Medicion_Diaria");
            DropForeignKey("dbo.Gpr_Medicion_Diaria", "IdGprServicio", "dbo.Gpr_Servicio");
            DropForeignKey("dbo.Gpr_Medicion_Diaria", "IdGprGalpon", "dbo.Gpr_Galpon");
            DropForeignKey("dbo.Gpr_Gasto_Diario", "IdGprCostoServicio", "dbo.Gpr_Costo_Servicio");
            DropForeignKey("dbo.Gpr_Estado_Ave", "IdGprTipoEstadoAve", "dbo.Gpr_Tipo_Estado_Ave");
            DropForeignKey("dbo.Gpr_Estado_Ave", "IdGprTemporada", "dbo.Gpr_Temporada");
            DropForeignKey("dbo.Gpr_Temporada", "IdGprGalpon", "dbo.Gpr_Galpon");
            DropForeignKey("dbo.Gpr_Costo_Servicio", "IdGprServicio", "dbo.Gpr_Servicio");
            DropForeignKey("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico", "dbo.Dom_Tipo_Componente_Electronico");
            DropForeignKey("dbo.Dom_Control_Componente_Electronico", "IdDomComponenteElectronico", "dbo.Dom_Componente_Electronico");
            DropForeignKey("dbo.Dom_Componente_Electronico", "IdGprServicio", "dbo.Gpr_Servicio");
            DropForeignKey("dbo.Gpr_Servicio", "IdGprUnidadMedida", "dbo.Gpr_Unidad_Medida");
            DropForeignKey("dbo.Gpr_Servicio", "IdGprTipoServicio", "dbo.Gpr_Tipo_Servicio");
            DropForeignKey("dbo.Dom_Componente_Electronico", "IdGprGalpon", "dbo.Gpr_Galpon");
            DropForeignKey("dbo.Dom_Componente_Electronico", "IdDomTipoComponenteElectronico", "dbo.Dom_Tipo_Componente_Electronico");
            DropIndex("dbo.Seg_Usuario", new[] { "IdSegTipoUsuario" });
            DropIndex("dbo.Gpr_Peso_Promedio_Ave", new[] { "IdGprTemporada" });
            DropIndex("dbo.Gpr_Medicion_Horaria", new[] { "IdGprGalpon" });
            DropIndex("dbo.Gpr_Medicion_Horaria", new[] { "IdGprServicio" });
            DropIndex("dbo.Gpr_Medicion_Diaria", new[] { "IdGprGalpon" });
            DropIndex("dbo.Gpr_Medicion_Diaria", new[] { "IdGprServicio" });
            DropIndex("dbo.Gpr_Gasto_Diario", new[] { "IdGprCostoServicio" });
            DropIndex("dbo.Gpr_Gasto_Diario", new[] { "IdGprMedicionDiaria" });
            DropIndex("dbo.Gpr_Temporada", new[] { "IdGprGalpon" });
            DropIndex("dbo.Gpr_Estado_Ave", new[] { "IdGprTemporada" });
            DropIndex("dbo.Gpr_Estado_Ave", new[] { "IdGprTipoEstadoAve" });
            DropIndex("dbo.Gpr_Costo_Servicio", new[] { "IdGprServicio" });
            DropIndex("dbo.Dom_Control_Componente_Electronico", new[] { "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico" });
            DropIndex("dbo.Dom_Control_Componente_Electronico", new[] { "IdDomComponenteElectronico" });
            DropIndex("dbo.Gpr_Servicio", new[] { "IdGprTipoServicio" });
            DropIndex("dbo.Gpr_Servicio", new[] { "IdGprUnidadMedida" });
            DropIndex("dbo.Dom_Componente_Electronico", new[] { "IdGprServicio" });
            DropIndex("dbo.Dom_Componente_Electronico", new[] { "IdGprGalpon" });
            DropIndex("dbo.Dom_Componente_Electronico", new[] { "IdDomTipoComponenteElectronico" });
            DropTable("dbo.Seg_Usuario");
            DropTable("dbo.Seg_Tipo_Usuario");
            DropTable("dbo.Gpr_Raza");
            DropTable("dbo.Gpr_Peso_Promedio_Ave");
            DropTable("dbo.Gpr_Medicion_Horaria");
            DropTable("dbo.Gpr_Medicion_Diaria");
            DropTable("dbo.Gpr_Gasto_Diario");
            DropTable("dbo.Gpr_Tipo_Estado_Ave");
            DropTable("dbo.Gpr_Temporada");
            DropTable("dbo.Gpr_Estado_Ave");
            DropTable("dbo.Gpr_Costo_Servicio");
            DropTable("dbo.Dom_Tipo_Control_Componente_Electronico");
            DropTable("dbo.Dom_Control_Componente_Electronico");
            DropTable("dbo.Gpr_Unidad_Medida");
            DropTable("dbo.Gpr_Tipo_Servicio");
            DropTable("dbo.Gpr_Servicio");
            DropTable("dbo.Gpr_Galpon");
            DropTable("dbo.Dom_Tipo_Componente_Electronico");
            DropTable("dbo.Dom_Componente_Electronico");
        }
    }
}