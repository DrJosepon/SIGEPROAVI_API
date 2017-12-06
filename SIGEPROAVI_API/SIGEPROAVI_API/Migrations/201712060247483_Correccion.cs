namespace SIGEPROAVI_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Correccion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico", "dbo.Dom_Tipo_Componente_Electronico");
            DropIndex("dbo.Dom_Control_Componente_Electronico", new[] { "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico" });
            CreateIndex("dbo.Dom_Control_Componente_Electronico", "IdDomTipoControlComponenteElectronico");
            AddForeignKey("dbo.Dom_Control_Componente_Electronico", "IdDomTipoControlComponenteElectronico", "dbo.Dom_Tipo_Control_Componente_Electronico", "IdDomTipoControlComponenteElectronico", cascadeDelete: true);
            //DropColumn("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico");
        }

        public override void Down()
        {
            AddColumn("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico", c => c.Int());
            DropForeignKey("dbo.Dom_Control_Componente_Electronico", "IdDomTipoControlComponenteElectronico", "dbo.Dom_Tipo_Control_Componente_Electronico");
            DropIndex("dbo.Dom_Control_Componente_Electronico", new[] { "IdDomTipoControlComponenteElectronico" });
            CreateIndex("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico");
            AddForeignKey("dbo.Dom_Control_Componente_Electronico", "Dom_Tipo_Componente_Electronico_IdDomTipoComponenteElectronico", "dbo.Dom_Tipo_Componente_Electronico", "IdDomTipoComponenteElectronico");
        }
    }
}