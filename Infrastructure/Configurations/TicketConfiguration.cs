using Domain.TicketAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();

        builder.OwnsOne(t => t.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(p => p.Value)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnName("PhoneNumber");
        });

        builder.OwnsOne(t => t.Location, location =>
        {
            location.Property(l => l.Governorate).HasMaxLength(100).IsRequired();
            location.Property(l => l.City).HasMaxLength(100).IsRequired();
            location.Property(l => l.District).HasMaxLength(100).IsRequired();
        });

        builder.Property(t => t.IsHandled)
            .IsRequired();

        builder.Property(t => t.Colour)
            .IsRequired();
    }
}