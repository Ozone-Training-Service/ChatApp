﻿// <auto-generated />
using System;
using ChatApp2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatApp2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210113033128_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ChatApp.Models.Message", b =>
                {
                    b.Property<string>("clientuniqueid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("clientuniqueid");

                    b.ToTable("Message");
                });
#pragma warning restore 612, 618
        }
    }
}