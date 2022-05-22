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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.ShipAddress).IsRequired();
            builder.Property(x => x.ShipNumberPhone).IsRequired();
            builder.Property(x => x.ShipName).IsRequired();
        }
    }
}
