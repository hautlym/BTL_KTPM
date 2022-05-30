﻿using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImg>
    {
        public void Configure(EntityTypeBuilder<ProductImg> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200);
            builder.HasOne(x => x.Product).WithMany(x => x.productImgs).HasForeignKey(x => x.ProductId);
        }
    }
}
