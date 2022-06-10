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
    public class UserConnfiguration:IEntityTypeConfiguration<User>
    {
       

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x=>x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=>x.Address).IsRequired();
            builder.Property(x=>x.Roles).IsRequired();
        }
    }
}
