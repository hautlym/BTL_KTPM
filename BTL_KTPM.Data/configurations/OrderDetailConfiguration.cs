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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(1);
            //builder.HasOne(x=>x.Order).WithMany(x=>x.OrderDetail).HasForeignKey(x=>x.OrderId);
            builder.HasOne(x=>x.Products).WithMany(x=>x.OrderDetails).HasForeignKey(x=>x.ProductId);
        }
    }
}
