﻿// <auto-generated />
using System;
using FoxPhonebook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoxPhonebook.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221021132315_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.ContactTag", b =>
                {
                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("ContactTag", (string)null);
                });

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Contact", b =>
                {
                    b.OwnsMany("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.ContactEmail", "Emails", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.HasKey("Id");

                            b1.HasIndex("ContactId");

                            b1.ToTable("ContactEmail");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.OwnsOne("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.ContactPersonalDetails", "PersonalDetails", b1 =>
                        {
                            b1.Property<Guid>("ContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CompanyName")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("nvarchar(75)")
                                .HasColumnName("CompanyName");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("nvarchar(75)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("nvarchar(75)")
                                .HasColumnName("LastName");

                            b1.HasKey("ContactId");

                            b1.ToTable("Contact");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.OwnsMany("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.ContactPhoneNumber", "PhoneNumbers", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("nvarchar(75)");

                            b1.HasKey("Id");

                            b1.HasIndex("ContactId");

                            b1.ToTable("ContactPhoneNumber");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.Navigation("Emails");

                    b.Navigation("PersonalDetails")
                        .IsRequired();

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.ContactTag", b =>
                {
                    b.HasOne("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Contact", "Contact")
                        .WithMany("ContactTags")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel.Contact", b =>
                {
                    b.Navigation("ContactTags");
                });
#pragma warning restore 612, 618
        }
    }
}
