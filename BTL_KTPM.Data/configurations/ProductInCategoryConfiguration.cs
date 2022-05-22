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
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.CategoryId });
            builder.HasOne(x => x.Product).WithMany(pc => pc.ProductInCategory).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Category).WithMany(pc => pc.ProductInCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}
