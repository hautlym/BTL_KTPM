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
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Carts).HasForeignKey(x => x.AppUserId);
            //builder.HasOne(x=>x.Order).WithMany(x=>x.ListCart).HasForeignKey(x => x.OrderId);
            //builder.HasOne(x => x.Users).WithMany(x => x.Carts).HasForeignKey(x => x.UserId);
        }
    }
}
