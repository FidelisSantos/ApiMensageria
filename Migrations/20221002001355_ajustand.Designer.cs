﻿// <auto-generated />
using System;
using ApiMensageria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiMensageria.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221002001355_ajustand")]
    partial class ajustand
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("ApiMensageria.Model.LoginModel", b =>
                {
                    b.Property<int>("LoginModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LoginModelId");

                    b.HasIndex("UserModelId")
                        .IsUnique();

                    b.ToTable("login", (string)null);
                });

            modelBuilder.Entity("ApiMensageria.Model.MessageModel", b =>
                {
                    b.Property<int>("MessageModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Sent")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserIssuerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserReceiverId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MessageModelId");

                    b.HasIndex("UserReceiverId");

                    b.ToTable("messagens", (string)null);
                });

            modelBuilder.Entity("ApiMensageria.Model.UserModel", b =>
                {
                    b.Property<int>("UserModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserModelId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ApiMensageria.Model.LoginModel", b =>
                {
                    b.HasOne("ApiMensageria.Model.UserModel", "User")
                        .WithOne("Login")
                        .HasForeignKey("ApiMensageria.Model.LoginModel", "UserModelId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiMensageria.Model.MessageModel", b =>
                {
                    b.HasOne("ApiMensageria.Model.UserModel", "UserReceiver")
                        .WithMany("Messages")
                        .HasForeignKey("UserReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserReceiver");
                });

            modelBuilder.Entity("ApiMensageria.Model.UserModel", b =>
                {
                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
