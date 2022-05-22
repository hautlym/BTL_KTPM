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
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
           builder.HasKey(x => x.Id);
           builder.Property(x=>x.Title).IsRequired();
           builder.Property(x => x.Author).IsRequired();
            builder.Property(x => x.DatePost).IsRequired();
        }
    }
}
