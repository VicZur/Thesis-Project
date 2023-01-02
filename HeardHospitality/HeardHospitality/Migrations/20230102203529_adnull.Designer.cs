﻿// <auto-generated />
using System;
using HeardHospitality.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HeardHospitality.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230102203529_adnull")]
    partial class adnull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HeardHospitality.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"), 1L, 1);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusinessID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EirCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("BusinessID");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Business", b =>
                {
                    b.Property<int>("BusinessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusinessID"), 1L, 1);

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LoginDetailsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusinessID");

                    b.HasIndex("LoginDetailsId");

                    b.ToTable("Business", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesiredJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpBio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSearching")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginDetailsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("LoginDetailsId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.EmployeeExperience", b =>
                {
                    b.Property<int>("EmployeeExperienceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeExperienceID"), 1L, 1);

                    b.Property<int?>("BusinessID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DisplayOnProfile")
                        .HasColumnType("bit");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeExperienceID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeExperience", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.EmpSkill", b =>
                {
                    b.Property<int>("EmpSkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpSkillID"), 1L, 1);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("SkillID")
                        .HasColumnType("int");

                    b.HasKey("EmpSkillID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SkillID");

                    b.ToTable("EmpSkill", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.JobApplication", b =>
                {
                    b.Property<int>("JobApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobApplicationID"), 1L, 1);

                    b.Property<DateTime>("DateApplied")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("bit");

                    b.Property<int?>("JobInfoID")
                        .HasColumnType("int");

                    b.HasKey("JobApplicationID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("JobInfoID");

                    b.ToTable("JobApplication", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.JobInfo", b =>
                {
                    b.Property<int>("JobInfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobInfoID"), 1L, 1);

                    b.Property<int>("BusinessID")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReported")
                        .HasColumnType("bit");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinExperience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PositionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobInfoID");

                    b.HasIndex("BusinessID");

                    b.ToTable("JobInfo", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.JobPerk", b =>
                {
                    b.Property<int>("JobPerkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobPerkID"), 1L, 1);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobInfoID")
                        .HasColumnType("int");

                    b.Property<int>("PerkID")
                        .HasColumnType("int");

                    b.HasKey("JobPerkID");

                    b.HasIndex("JobInfoID");

                    b.HasIndex("PerkID");

                    b.ToTable("JobPerk", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.LoginDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("isBusinessAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("isEmployeeAccount")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("LoginDetail", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Perk", b =>
                {
                    b.Property<int>("PerkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerkID"), 1L, 1);

                    b.Property<string>("PerkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PerkID");

                    b.ToTable("Perk", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Rating", b =>
                {
                    b.Property<int>("RatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingID"), 1L, 1);

                    b.Property<int?>("BusinessID")
                        .HasColumnType("int");

                    b.Property<int>("ClienteleRating")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeExperienceID")
                        .HasColumnType("int");

                    b.Property<int>("FairnessRating")
                        .HasColumnType("int");

                    b.Property<bool>("IsDisplayed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReported")
                        .HasColumnType("bit");

                    b.Property<int>("ManagementRating")
                        .HasColumnType("int");

                    b.Property<int>("OverallRating")
                        .HasColumnType("int");

                    b.Property<int>("SalaryRating")
                        .HasColumnType("int");

                    b.Property<bool>("UnpaidTrialShift")
                        .HasColumnType("bit");

                    b.Property<bool>("WouldWorkAgain")
                        .HasColumnType("bit");

                    b.HasKey("RatingID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("EmployeeExperienceID");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Reply", b =>
                {
                    b.Property<int>("ReplyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReplyID"), 1L, 1);

                    b.Property<int?>("BusinessID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisplayed")
                        .HasColumnType("bit");

                    b.Property<int?>("RatingID")
                        .HasColumnType("int");

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReplysReplyID")
                        .HasColumnType("int");

                    b.HasKey("ReplyID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("RatingID");

                    b.HasIndex("ReplysReplyID");

                    b.ToTable("Reply", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.ReportedJobDetail", b =>
                {
                    b.Property<int>("ReportedJobDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportedJobDetailID"), 1L, 1);

                    b.Property<bool>("AdDetailsIncorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("IllegalExpectations")
                        .HasColumnType("bit");

                    b.Property<int?>("JobInfoID")
                        .HasColumnType("int");

                    b.Property<bool>("PerksListedIncorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("SalaryIncorrect")
                        .HasColumnType("bit");

                    b.HasKey("ReportedJobDetailID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("JobInfoID");

                    b.ToTable("ReportedJobDetail", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillID"), 1L, 1);

                    b.Property<string>("SkillDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillID");

                    b.ToTable("Skill", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HeardHospitality.Models.Address", b =>
                {
                    b.HasOne("HeardHospitality.Models.Business", "Businesses")
                        .WithMany("Addresses")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Businesses");
                });

            modelBuilder.Entity("HeardHospitality.Models.Business", b =>
                {
                    b.HasOne("HeardHospitality.Models.LoginDetail", "LoginDetails")
                        .WithMany()
                        .HasForeignKey("LoginDetailsId");

                    b.Navigation("LoginDetails");
                });

            modelBuilder.Entity("HeardHospitality.Models.Employee", b =>
                {
                    b.HasOne("HeardHospitality.Models.LoginDetail", "LoginDetails")
                        .WithMany()
                        .HasForeignKey("LoginDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginDetails");
                });

            modelBuilder.Entity("HeardHospitality.Models.EmployeeExperience", b =>
                {
                    b.HasOne("HeardHospitality.Models.Business", "Business")
                        .WithMany("EmployeeExperiences")
                        .HasForeignKey("BusinessID");

                    b.HasOne("HeardHospitality.Models.Employee", "Employee")
                        .WithMany("EmployeeExperiences")
                        .HasForeignKey("EmployeeID");

                    b.Navigation("Business");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HeardHospitality.Models.EmpSkill", b =>
                {
                    b.HasOne("HeardHospitality.Models.Employee", "Employee")
                        .WithMany("EmpSkills")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeardHospitality.Models.Skill", "Skill")
                        .WithMany("EmpSkills")
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("HeardHospitality.Models.JobApplication", b =>
                {
                    b.HasOne("HeardHospitality.Models.Employee", "Employee")
                        .WithMany("JobApplications")
                        .HasForeignKey("EmployeeID");

                    b.HasOne("HeardHospitality.Models.JobInfo", "JobInfo")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobInfoID");

                    b.Navigation("Employee");

                    b.Navigation("JobInfo");
                });

            modelBuilder.Entity("HeardHospitality.Models.JobInfo", b =>
                {
                    b.HasOne("HeardHospitality.Models.Business", "Business")
                        .WithMany("JobInfos")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("HeardHospitality.Models.JobPerk", b =>
                {
                    b.HasOne("HeardHospitality.Models.JobInfo", "JobInfo")
                        .WithMany("JobPerks")
                        .HasForeignKey("JobInfoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeardHospitality.Models.Perk", "Perk")
                        .WithMany("JobPerks")
                        .HasForeignKey("PerkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobInfo");

                    b.Navigation("Perk");
                });

            modelBuilder.Entity("HeardHospitality.Models.Rating", b =>
                {
                    b.HasOne("HeardHospitality.Models.Business", null)
                        .WithMany("Ratings")
                        .HasForeignKey("BusinessID");

                    b.HasOne("HeardHospitality.Models.EmployeeExperience", "Experience")
                        .WithMany("Ratings")
                        .HasForeignKey("EmployeeExperienceID");

                    b.Navigation("Experience");
                });

            modelBuilder.Entity("HeardHospitality.Models.Reply", b =>
                {
                    b.HasOne("HeardHospitality.Models.Business", "Business")
                        .WithMany("Replys")
                        .HasForeignKey("BusinessID");

                    b.HasOne("HeardHospitality.Models.Rating", null)
                        .WithMany("Replys")
                        .HasForeignKey("RatingID");

                    b.HasOne("HeardHospitality.Models.Reply", "Replys")
                        .WithMany()
                        .HasForeignKey("ReplysReplyID");

                    b.Navigation("Business");

                    b.Navigation("Replys");
                });

            modelBuilder.Entity("HeardHospitality.Models.ReportedJobDetail", b =>
                {
                    b.HasOne("HeardHospitality.Models.Employee", "Employees")
                        .WithMany("ReportedJobDetails")
                        .HasForeignKey("EmployeeID");

                    b.HasOne("HeardHospitality.Models.JobInfo", "JobInfos")
                        .WithMany("ReportedJobDetails")
                        .HasForeignKey("JobInfoID");

                    b.Navigation("Employees");

                    b.Navigation("JobInfos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HeardHospitality.Models.LoginDetail", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HeardHospitality.Models.LoginDetail", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeardHospitality.Models.LoginDetail", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HeardHospitality.Models.LoginDetail", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeardHospitality.Models.Business", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("EmployeeExperiences");

                    b.Navigation("JobInfos");

                    b.Navigation("Ratings");

                    b.Navigation("Replys");
                });

            modelBuilder.Entity("HeardHospitality.Models.Employee", b =>
                {
                    b.Navigation("EmpSkills");

                    b.Navigation("EmployeeExperiences");

                    b.Navigation("JobApplications");

                    b.Navigation("ReportedJobDetails");
                });

            modelBuilder.Entity("HeardHospitality.Models.EmployeeExperience", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("HeardHospitality.Models.JobInfo", b =>
                {
                    b.Navigation("JobApplications");

                    b.Navigation("JobPerks");

                    b.Navigation("ReportedJobDetails");
                });

            modelBuilder.Entity("HeardHospitality.Models.Perk", b =>
                {
                    b.Navigation("JobPerks");
                });

            modelBuilder.Entity("HeardHospitality.Models.Rating", b =>
                {
                    b.Navigation("Replys");
                });

            modelBuilder.Entity("HeardHospitality.Models.Skill", b =>
                {
                    b.Navigation("EmpSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
