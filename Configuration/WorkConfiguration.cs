using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Models;

namespace TaskManager.Configuration
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.Property(p => p.Status).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }

    }
}
