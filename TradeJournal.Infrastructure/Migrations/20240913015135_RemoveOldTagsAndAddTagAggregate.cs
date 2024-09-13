using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TradeJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldTagsAndAddTagAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisAnalysisTag");

            migrationBuilder.DropTable(
                name: "ImageImageTag");

            migrationBuilder.DropTable(
                name: "JournalJournalTag");

            migrationBuilder.DropTable(
                name: "AnalysisTags");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.AddColumn<int>(
                name: "AnalysisId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JournalId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagType",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AnalysisId",
                table: "Tags",
                column: "AnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ImageId",
                table: "Tags",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_JournalId",
                table: "Tags",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Analysis_AnalysisId",
                table: "Tags",
                column: "AnalysisId",
                principalTable: "Analysis",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Images_ImageId",
                table: "Tags",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Journals_JournalId",
                table: "Tags",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Analysis_AnalysisId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Images_ImageId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Journals_JournalId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_AnalysisId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ImageId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_JournalId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "AnalysisId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagType",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "AnalysisTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisTags", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JournalJournalTag",
                columns: table => new
                {
                    JournalTagsId = table.Column<int>(type: "int", nullable: false),
                    JournalsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalJournalTag", x => new { x.JournalTagsId, x.JournalsId });
                    table.ForeignKey(
                        name: "FK_JournalJournalTag_Journals_JournalsId",
                        column: x => x.JournalsId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalJournalTag_Tags_JournalTagsId",
                        column: x => x.JournalTagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnalysisAnalysisTag",
                columns: table => new
                {
                    AnalysisId = table.Column<int>(type: "int", nullable: false),
                    AnalysisTagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisAnalysisTag", x => new { x.AnalysisId, x.AnalysisTagsId });
                    table.ForeignKey(
                        name: "FK_AnalysisAnalysisTag_AnalysisTags_AnalysisTagsId",
                        column: x => x.AnalysisTagsId,
                        principalTable: "AnalysisTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisAnalysisTag_Analysis_AnalysisId",
                        column: x => x.AnalysisId,
                        principalTable: "Analysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageImageTag",
                columns: table => new
                {
                    ImageTagsId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageImageTag", x => new { x.ImageTagsId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_ImageImageTag_ImageTags_ImageTagsId",
                        column: x => x.ImageTagsId,
                        principalTable: "ImageTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageImageTag_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisAnalysisTag_AnalysisTagsId",
                table: "AnalysisAnalysisTag",
                column: "AnalysisTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageImageTag_ImagesId",
                table: "ImageImageTag",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalJournalTag_JournalsId",
                table: "JournalJournalTag",
                column: "JournalsId");
        }
    }
}
