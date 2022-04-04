using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWebmotors.Dominio.Entidade;

namespace TesteWebmotors.Infraestrutura.Map
{
    public class AnuncioWebmotorsMap : IEntityTypeConfiguration<AnuncioWebmotors>
    {
        public void Configure(EntityTypeBuilder<AnuncioWebmotors> builder)
        {
            builder.HasKey(t => t.Id);
            
            builder.ToTable("tb_AnuncioWebmotors");

            builder.Property(t => t.Id).HasColumnName("ID").HasColumnType("INT");
            builder.Property(t => t.Marca).HasColumnName("marca").HasColumnType("varchar(45)").HasMaxLength(45);
            builder.Property(t => t.Modelo).HasColumnName("modelo").HasColumnType("varchar(45)").HasMaxLength(45);
            builder.Property(t => t.Versao).HasColumnName("versao").HasColumnType("varchar(45)").HasMaxLength(45);
            builder.Property(t => t.Ano).HasColumnName("ano").HasColumnType("INT");
            builder.Property(t => t.Quilometragem).HasColumnName("quilometragem").HasColumnType("INT");
            builder.Property(t => t.Observacao).HasColumnName("observacao").HasColumnType("Text");

        }
    }
}
