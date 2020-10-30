using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tasker.Models;

namespace TaskManager.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            //builder.Property(p => p.DateOfLastChange).ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.Status).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        }



    }
}
