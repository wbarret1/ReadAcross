using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAcross.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalyst",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalyst", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compound",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CasNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compound", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Smarts = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Temp2 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solvent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NamedReaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReactantA = table.Column<string>(type: "TEXT", nullable: true),
                    ReactantB = table.Column<string>(type: "TEXT", nullable: true),
                    ReactantC = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Heat = table.Column<string>(type: "TEXT", nullable: true),
                    AcidBase = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    CatalystId = table.Column<long>(type: "INTEGER", nullable: false),
                    FunctionalGroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    SolventId = table.Column<long>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedReaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamedReaction_Catalyst_CatalystId",
                        column: x => x.CatalystId,
                        principalTable: "Catalyst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedReaction_FunctionalGroup_FunctionalGroupId",
                        column: x => x.FunctionalGroupId,
                        principalTable: "FunctionalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedReaction_Solvent_SolventId",
                        column: x => x.SolventId,
                        principalTable: "Solvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NamedReactionByProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamedreactionId = table.Column<long>(type: "INTEGER", nullable: false),
                    ReactantId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedReactionByProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamedReactionByProducts_NamedReaction_NamedreactionId",
                        column: x => x.NamedreactionId,
                        principalTable: "NamedReaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedReactionByProducts_Reactant_ReactantId",
                        column: x => x.ReactantId,
                        principalTable: "Reactant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NamedReactionReactants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamedreactionId = table.Column<long>(type: "INTEGER", nullable: false),
                    ReactantId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedReactionReactants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamedReactionReactants_NamedReaction_NamedreactionId",
                        column: x => x.NamedreactionId,
                        principalTable: "NamedReaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedReactionReactants_Reactant_ReactantId",
                        column: x => x.ReactantId,
                        principalTable: "Reactant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Risdata = table.Column<string>(type: "TEXT", nullable: true),
                    FunctionalGroupId = table.Column<long>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reference_FunctionalGroup_FunctionalGroupId",
                        column: x => x.FunctionalGroupId,
                        principalTable: "FunctionalGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reference_NamedReaction_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "NamedReaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NamedReaction_CatalystId",
                table: "NamedReaction",
                column: "CatalystId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReaction_FunctionalGroupId",
                table: "NamedReaction",
                column: "FunctionalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReaction_SolventId",
                table: "NamedReaction",
                column: "SolventId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReactionByProducts_NamedreactionId",
                table: "NamedReactionByProducts",
                column: "NamedreactionId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReactionByProducts_ReactantId",
                table: "NamedReactionByProducts",
                column: "ReactantId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReactionReactants_NamedreactionId",
                table: "NamedReactionReactants",
                column: "NamedreactionId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedReactionReactants_ReactantId",
                table: "NamedReactionReactants",
                column: "ReactantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_FunctionalGroupId",
                table: "Reference",
                column: "FunctionalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_ReactionId",
                table: "Reference",
                column: "ReactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compound");

            migrationBuilder.DropTable(
                name: "NamedReactionByProducts");

            migrationBuilder.DropTable(
                name: "NamedReactionReactants");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "Reactant");

            migrationBuilder.DropTable(
                name: "NamedReaction");

            migrationBuilder.DropTable(
                name: "Catalyst");

            migrationBuilder.DropTable(
                name: "FunctionalGroup");

            migrationBuilder.DropTable(
                name: "Solvent");
        }
    }
}
