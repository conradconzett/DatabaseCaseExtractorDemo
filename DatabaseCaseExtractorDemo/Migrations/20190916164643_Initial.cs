using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseCaseExtractorDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalDataTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameAdditional = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalDataTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableOnes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameOne = table.Column<string>(nullable: true),
                    DateOne = table.Column<DateTime>(nullable: false),
                    IntOne = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableOnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableSeconds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameSecond = table.Column<string>(nullable: true),
                    DateSecond = table.Column<DateTime>(nullable: false),
                    IntSecond = table.Column<int>(nullable: false),
                    TableOneId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableSeconds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableSeconds_TableOnes_TableOneId",
                        column: x => x.TableOneId,
                        principalTable: "TableOnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableThirds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NameThird = table.Column<string>(nullable: true),
                    DateThird = table.Column<DateTime>(nullable: false),
                    IntThird = table.Column<int>(nullable: false),
                    TableSecondId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableThirds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableThirds_TableSeconds_TableSecondId",
                        column: x => x.TableSecondId,
                        principalTable: "TableSeconds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdditionalDataTables",
                columns: new[] { "Id", "NameAdditional" },
                values: new object[] { new Guid("88ddf83c-9eaf-4f4a-abb3-2f3277096d17"), "Test1" });

            migrationBuilder.InsertData(
                table: "AdditionalDataTables",
                columns: new[] { "Id", "NameAdditional" },
                values: new object[] { new Guid("64925c3c-8ceb-490b-8735-6fdcc1f47c96"), "Test2" });

            migrationBuilder.InsertData(
                table: "TableOnes",
                columns: new[] { "Id", "DateOne", "IntOne", "NameOne" },
                values: new object[] { new Guid("79946776-b133-4d6e-892b-97d8dbbc26d8"), new DateTime(2019, 9, 16, 18, 46, 42, 835, DateTimeKind.Local).AddTicks(1745), 1, "A" });

            migrationBuilder.InsertData(
                table: "TableSeconds",
                columns: new[] { "Id", "DateSecond", "IntSecond", "NameSecond", "TableOneId" },
                values: new object[] { 1, new DateTime(2019, 9, 16, 18, 46, 42, 839, DateTimeKind.Local).AddTicks(9832), 1, "A", new Guid("79946776-b133-4d6e-892b-97d8dbbc26d8") });

            migrationBuilder.InsertData(
                table: "TableSeconds",
                columns: new[] { "Id", "DateSecond", "IntSecond", "NameSecond", "TableOneId" },
                values: new object[] { 2, new DateTime(2019, 9, 16, 18, 46, 42, 840, DateTimeKind.Local).AddTicks(2577), 1, "A", new Guid("79946776-b133-4d6e-892b-97d8dbbc26d8") });

            migrationBuilder.InsertData(
                table: "TableThirds",
                columns: new[] { "Id", "DateThird", "IntThird", "NameThird", "TableSecondId" },
                values: new object[,]
                {
                    { "A", new DateTime(2019, 9, 16, 18, 46, 42, 840, DateTimeKind.Local).AddTicks(5196), 1, "A", 1 },
                    { "B", new DateTime(2019, 9, 16, 18, 46, 42, 840, DateTimeKind.Local).AddTicks(6750), 1, "A", 1 },
                    { "C", new DateTime(2019, 9, 16, 18, 46, 42, 840, DateTimeKind.Local).AddTicks(6797), 1, "A", 2 },
                    { "D", new DateTime(2019, 9, 16, 18, 46, 42, 840, DateTimeKind.Local).AddTicks(6816), 1, "A", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableSeconds_TableOneId",
                table: "TableSeconds",
                column: "TableOneId");

            migrationBuilder.CreateIndex(
                name: "IX_TableThirds_TableSecondId",
                table: "TableThirds",
                column: "TableSecondId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalDataTables");

            migrationBuilder.DropTable(
                name: "TableThirds");

            migrationBuilder.DropTable(
                name: "TableSeconds");

            migrationBuilder.DropTable(
                name: "TableOnes");
        }
    }
}
