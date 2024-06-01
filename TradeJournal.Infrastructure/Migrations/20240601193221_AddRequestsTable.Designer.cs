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
    [Migration("20240601193221_AddRequestsTable")]
    partial class AddRequestsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnalysisAnalysisTag", b =>
                {
                    b.Property<int>("AnalysisId")
                        .HasColumnType("int");

                    b.Property<int>("AnalysisTagsId")
                        .HasColumnType("int");

                    b.HasKey("AnalysisId", "AnalysisTagsId");

                    b.HasIndex("AnalysisTagsId");

                    b.ToTable("AnalysisAnalysisTag");
                });

            modelBuilder.Entity("ImageImageTag", b =>
                {
                    b.Property<int>("ImageTagsId")
                        .HasColumnType("int");

                    b.Property<int>("ImagesId")
                        .HasColumnType("int");

                    b.HasKey("ImageTagsId", "ImagesId");

                    b.HasIndex("ImagesId");

                    b.ToTable("ImageImageTag");
                });

            modelBuilder.Entity("JournalJournalTag", b =>
                {
                    b.Property<int>("JournalTagsId")
                        .HasColumnType("int");

                    b.Property<int>("JournalsId")
                        .HasColumnType("int");

                    b.HasKey("JournalTagsId", "JournalsId");

                    b.HasIndex("JournalsId");

                    b.ToTable("JournalJournalTag");
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

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.JournalAggregate.JournalTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

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

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.AnalysisTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AnalysisTags");
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

            modelBuilder.Entity("TradeJournal.Domain.Aggregates.TradeAggregate.ImageTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ImageTags");
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

            modelBuilder.Entity("AnalysisAnalysisTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Analysis", null)
                        .WithMany()
                        .HasForeignKey("AnalysisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.AnalysisTag", null)
                        .WithMany()
                        .HasForeignKey("AnalysisTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ImageImageTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.ImageTag", null)
                        .WithMany()
                        .HasForeignKey("ImageTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.TradeAggregate.Image", null)
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JournalJournalTag", b =>
                {
                    b.HasOne("TradeJournal.Domain.Aggregates.JournalAggregate.JournalTag", null)
                        .WithMany()
                        .HasForeignKey("JournalTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeJournal.Domain.Aggregates.JournalAggregate.Journal", null)
                        .WithMany()
                        .HasForeignKey("JournalsId")
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
