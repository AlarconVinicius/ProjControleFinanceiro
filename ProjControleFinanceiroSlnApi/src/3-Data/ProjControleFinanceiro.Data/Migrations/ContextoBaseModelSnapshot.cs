﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjControleFinanceiro.Data.Configuracao;

#nullable disable

namespace ProjControleFinanceiro.Data.Migrations
{
    [DbContext(typeof(ContextoBase))]
    partial class ContextoBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjControleFinanceiro.Entities.Entidades.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("ProjControleFinanceiro.Entities.Entidades.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Pago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("QtdRepeticao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("Repete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("TipoTransacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Transacoes", null, t =>
                        {
                            t.HasCheckConstraint("CK_Categoria_NaoZero", "Categoria > 0");

                            t.HasCheckConstraint("CK_TipoTransacao_NaoZero", "TipoTransacao > 0");

                            t.HasCheckConstraint("CK_Valor_NaoNegativo", "Valor >= 0");
                        });
                });

            modelBuilder.Entity("ProjControleFinanceiro.Entities.Entidades.Transacao", b =>
                {
                    b.HasOne("ProjControleFinanceiro.Entities.Entidades.Cliente", "Cliente")
                        .WithMany("Transacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ProjControleFinanceiro.Entities.Entidades.Cliente", b =>
                {
                    b.Navigation("Transacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
