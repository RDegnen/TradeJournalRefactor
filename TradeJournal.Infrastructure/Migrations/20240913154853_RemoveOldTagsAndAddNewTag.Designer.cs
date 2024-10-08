﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradeJournal.Infrastructure;

#nullable disable

namespace TradeJournal.Infrastructure.Migrations
{
    [DbContext(typeof(TradeJournalContext))]
    [Migration("20240913154853_RemoveOldTagsAndAddNewTag")]
    partial class RemoveOldTagsAndAddNewTag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnalysisTag", b =>
                {
                    b.Property<int>("AnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("AnalysisId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("AnalysisTag");
                });

            modelBuilder.Entity("ImageTag", b =>
                {
                    b.Property<int>("ImagesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ImagesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ImageTag");
                });

            modelBuilder.Entity("JournalTag", b =>
                {
                    b.Property<int>("JournalsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("JournalsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("JournalTag");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.JournalAggregate.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("double");

                    b.Property<int>("JournalId")
                        .HasColumnType("int");

                    b.Property<double>("RealizedPnL")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("JournalId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TagAggregate.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TagType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Analysis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("StrategyId")
                        .HasColumnType("int");

                    b.Property<int>("TradeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StrategyId");

                    b.HasIndex("TradeId")
                        .IsUnique();

                    b.ToTable("Analysis");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TradeId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("TradeId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Strategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Strategies");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<double>("EntryPrice")
                        .HasColumnType("double");

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("ExitPrice")
                        .HasColumnType("double");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("JournalId")
                        .HasColumnType("int");

                    b.Property<double?>("ProfitOrLoss")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double?>("Risk")
                        .HasColumnType("double");

                    b.Property<double?>("StopLoss")
                        .HasColumnType("double");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double?>("TakeProfit")
                        .HasColumnType("double");

                    b.Property<double?>("TrailingStopLoss")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("JournalId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("TradeJournal.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("requests", (string)null);
                });

            modelBuilder.Entity("AnalysisTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Analysis", null)
                        .WithMany()
                        .HasForeignKey("AnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.TagAggregate.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImageTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Image", null)
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.TagAggregate.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JournalTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", null)
                        .WithMany()
                        .HasForeignKey("JournalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.TagAggregate.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.JournalAggregate.Account", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", null)
                        .WithOne("Account")
                        .HasForeignKey("TradeJournal.Domain.Aggregates.JournalAggregate.Account", "JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Analysis", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Strategy", "Strategy")
                        .WithMany("Analysis")
                        .HasForeignKey("StrategyId");

                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Trade", null)
                        .WithOne("Analysis")
                        .HasForeignKey("TradeJournal.Domain.Aggregates.TradeAggregate.Analysis", "TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Strategy");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Image", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Trade", null)
                        .WithMany("Images")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Trade", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", null)
                        .WithMany("Trades")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Trades");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Strategy", b =>
                {
                    b.Navigation("Analysis");
                });

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.Trade", b =>
                {
                    b.Navigation("Analysis");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
