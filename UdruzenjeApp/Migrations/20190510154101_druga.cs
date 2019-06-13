using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UdruzenjeApp.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drzava",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "grad",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    DrzavaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_grad_drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    RoleClaimID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleID = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.RoleClaimID);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(maxLength: 50, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    GradID = table.Column<int>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    SlikaURL = table.Column<string>(nullable: true),
                    brojTelefona = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_grad_GradID",
                        column: x => x.GradID,
                        principalTable: "grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "anketa",
                columns: table => new
                {
                    AnketaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nazivAnkete = table.Column<string>(nullable: true),
                    UpraviteljID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anketa", x => x.AnketaID);
                    table.ForeignKey(
                        name: "FK_anketa_User_UpraviteljID",
                        column: x => x.UpraviteljID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dogadjaj",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    VrijemeOdrzavanja = table.Column<DateTime>(nullable: false),
                    opis = table.Column<string>(nullable: true),
                    OgranicenoMjesta = table.Column<bool>(nullable: false),
                    brojMjesta = table.Column<int>(nullable: false),
                    UpraviteljID = table.Column<int>(nullable: false),
                    GradID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dogadjaj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dogadjaj_grad_GradID",
                        column: x => x.GradID,
                        principalTable: "grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dogadjaj_User_UpraviteljID",
                        column: x => x.UpraviteljID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "obavijest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivObavijesti = table.Column<string>(nullable: true),
                    TextObavijesti = table.Column<string>(nullable: true),
                    datumObjave = table.Column<DateTime>(nullable: false),
                    UpraviteljID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obavijest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_obavijest_User_UpraviteljID",
                        column: x => x.UpraviteljID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "predlozeniDogadjaj",
                columns: table => new
                {
                    PredlozeniDogadjajID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivPrijedloga = table.Column<string>(nullable: true),
                    OpisPredlozenogDogadjaja = table.Column<string>(nullable: true),
                    ClanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_predlozeniDogadjaj", x => x.PredlozeniDogadjajID);
                    table.ForeignKey(
                        name: "FK_predlozeniDogadjaj_User_ClanID",
                        column: x => x.ClanID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    UserClaimID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.UserClaimID);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserID, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pitanje",
                columns: table => new
                {
                    PitanjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextPitanja = table.Column<string>(nullable: true),
                    AnketaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pitanje", x => x.PitanjeID);
                    table.ForeignKey(
                        name: "FK_pitanje_anketa_AnketaID",
                        column: x => x.AnketaID,
                        principalTable: "anketa",
                        principalColumn: "AnketaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "odgovor",
                columns: table => new
                {
                    OdgovorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextOdgovora = table.Column<string>(nullable: true),
                    PitanjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_odgovor", x => x.OdgovorID);
                    table.ForeignKey(
                        name: "FK_odgovor_pitanje_PitanjeID",
                        column: x => x.PitanjeID,
                        principalTable: "pitanje",
                        principalColumn: "PitanjeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anketa_UpraviteljID",
                table: "anketa",
                column: "UpraviteljID");

            migrationBuilder.CreateIndex(
                name: "IX_dogadjaj_GradID",
                table: "dogadjaj",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_dogadjaj_UpraviteljID",
                table: "dogadjaj",
                column: "UpraviteljID");

            migrationBuilder.CreateIndex(
                name: "IX_grad_DrzavaID",
                table: "grad",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_obavijest_UpraviteljID",
                table: "obavijest",
                column: "UpraviteljID");

            migrationBuilder.CreateIndex(
                name: "IX_odgovor_PitanjeID",
                table: "odgovor",
                column: "PitanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_pitanje_AnketaID",
                table: "pitanje",
                column: "AnketaID");

            migrationBuilder.CreateIndex(
                name: "IX_predlozeniDogadjaj_ClanID",
                table: "predlozeniDogadjaj",
                column: "ClanID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleID",
                table: "RoleClaim",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GradID",
                table: "User",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserID",
                table: "UserClaim",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserID",
                table: "UserLogin",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dogadjaj");

            migrationBuilder.DropTable(
                name: "obavijest");

            migrationBuilder.DropTable(
                name: "odgovor");

            migrationBuilder.DropTable(
                name: "predlozeniDogadjaj");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "pitanje");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "anketa");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "grad");

            migrationBuilder.DropTable(
                name: "drzava");
        }
    }
}
