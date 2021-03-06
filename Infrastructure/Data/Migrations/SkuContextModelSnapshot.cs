// <auto-generated />
using System;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(SkuContext))]
    partial class SkuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Core.Entidades.LogSku", b =>
                {
                    b.Property<int>("IdLog")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IX_LogSkus_IdSkuNumber")
                        .HasColumnType("int");

                    b.Property<int?>("IdStatus1")
                        .HasColumnType("int");

                    b.HasKey("IdLog");

                    b.HasIndex("IX_LogSkus_IdSkuNumber");

                    b.HasIndex("IdStatus1");

                    b.ToTable("LogSkus");
                });

            modelBuilder.Entity("Core.Entidades.Orden", b =>
                {
                    b.Property<int>("IdOrden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IX_Ordens_IdSkuNumber")
                        .HasColumnType("int");

                    b.Property<int?>("IdStatus1")
                        .HasColumnType("int");

                    b.HasKey("IdOrden");

                    b.HasIndex("IX_Ordens_IdSkuNumber");

                    b.HasIndex("IdStatus1");

                    b.ToTable("Ordens");
                });

            modelBuilder.Entity("Core.Entidades.Sku", b =>
                {
                    b.Property<int>("SkuNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("SkuNumber");

                    b.ToTable("Skus");
                });

            modelBuilder.Entity("Core.Entidades.Status", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StatusDesc")
                        .HasColumnType("longtext");

                    b.HasKey("IdStatus");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Core.Entidades.LogSku", b =>
                {
                    b.HasOne("Core.Entidades.Sku", "IdSkuNumber")
                        .WithMany()
                        .HasForeignKey("IX_LogSkus_IdSkuNumber");

                    b.HasOne("Core.Entidades.Status", "IdStatus")
                        .WithMany()
                        .HasForeignKey("IdStatus1");

                    b.Navigation("IdSkuNumber");

                    b.Navigation("IdStatus");
                });

            modelBuilder.Entity("Core.Entidades.Orden", b =>
                {
                    b.HasOne("Core.Entidades.Sku", "IdSkuNumber")
                        .WithMany()
                        .HasForeignKey("IX_Ordens_IdSkuNumber");

                    b.HasOne("Core.Entidades.Status", "IdStatus")
                        .WithMany()
                        .HasForeignKey("IdStatus1");

                    b.Navigation("IdSkuNumber");

                    b.Navigation("IdStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
