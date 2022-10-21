using FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Infrastructure.Persistence.EntityConfigurations;

public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contact");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.BirthDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.OwnsOne(e => e.PersonalDetails, cfg =>
        {
            cfg.Property(e => e.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(75);

            cfg.Property(e => e.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(75);
            
            cfg.Property(e => e.CompanyName)
            .HasColumnName("CompanyName")
            .HasMaxLength(75);                
        });

        builder.OwnsMany(e=>e.Emails, cfg =>{
            cfg.WithOwner().HasForeignKey("ContactId");
            cfg.Property<Guid>("Id");
            cfg.HasKey("Id");

            cfg.Property(e => e.Email).HasMaxLength(150);
        });

        var emailsNavigation = builder.Metadata.FindNavigation(nameof(Contact.Emails));
        emailsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(e => e.PhoneNumbers, cfg => {
            cfg.WithOwner().HasForeignKey("ContactId");
            cfg.Property<Guid>("Id");
            cfg.HasKey("Id");

            cfg.Property(e => e.PhoneNumber).HasMaxLength(20);
            cfg.Property(e => e.Title).HasMaxLength(75);
        });

        var phoenNumberNavigation = builder.Metadata.FindNavigation(nameof(Contact.PhoneNumbers));
        phoenNumberNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        var tagsNavigation = builder.Metadata.FindNavigation(nameof(Contact.ContactTags));
        tagsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tag");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(75);
    }
}

public class ContactTagEntityTypeConfiguration : IEntityTypeConfiguration<ContactTag>
{
    public void Configure(EntityTypeBuilder<ContactTag> builder)
    {
        builder.ToTable("ContactTag");
        builder.HasKey(e => new { e.TagId, e.ContactId });

        builder.HasOne(w=>w.Contact)
            .WithMany(e => e.ContactTags) 
            .HasForeignKey(e=>e.ContactId);

        builder.HasOne(w => w.Tag)
            .WithMany()
            .HasForeignKey(e => e.TagId);
    }
}
