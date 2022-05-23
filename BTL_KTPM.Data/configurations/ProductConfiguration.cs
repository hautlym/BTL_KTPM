using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.ProductPrice).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.ProductTitle).IsRequired();
            builder.Property(x => x.Discount).HasDefaultValue(0);
            builder.Property(x => x.ProductOriginalPrice).IsRequired();
            builder.Property(x => x.ProductTitle).IsRequired();
            builder.HasOne(x => x.Producers).WithMany(x => x.Products).HasForeignKey(x => x.ProducerId);
            builder.HasOne(x => x.category).WithMany(x => x.products).HasForeignKey(x => x.CategoryId);
        }
    }
}
