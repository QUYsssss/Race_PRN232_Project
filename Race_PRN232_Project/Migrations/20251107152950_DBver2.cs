using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Race_PRN232_Project.Migrations
{
    public partial class DBver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK__Race__LocationId__31EC6D26",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(201)", maxLength: 201, nullable: false, computedColumnSql: "(([FirstName]+' ')+[LastName])", stored: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__User__RoleId__2D27B809",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK__Image__RaceId__38996AB5",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId");
                });

            migrationBuilder.CreateTable(
                name: "RaceDistance",
                columns: table => new
                {
                    RaceDistanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    DistanceKm = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CutoffTimeHours = table.Column<decimal>(type: "decimal(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceDistance", x => x.RaceDistanceId);
                    table.ForeignKey(
                        name: "FK__RaceDista__RaceI__34C8D9D1",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId");
                });

            migrationBuilder.CreateTable(
                name: "SupportTeam",
                columns: table => new
                {
                    SupportTeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    LeaderId = table.Column<int>(type: "int", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTeam", x => x.SupportTeamId);
                    table.ForeignKey(
                        name: "FK__SupportTe__Leade__3C69FB99",
                        column: x => x.LeaderId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__SupportTe__RaceI__3B75D760",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId");
                });

            migrationBuilder.CreateTable(
                name: "RaceParticipant",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DistanceId = table.Column<int>(type: "int", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RacePart__D4835A70A3903942", x => new { x.RaceId, x.UserId });
                    table.ForeignKey(
                        name: "FK__RaceParti__Dista__47DBAE45",
                        column: x => x.DistanceId,
                        principalTable: "RaceDistance",
                        principalColumn: "RaceDistanceId");
                    table.ForeignKey(
                        name: "FK__RaceParti__RaceI__45F365D3",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId");
                    table.ForeignKey(
                        name: "FK__RaceParti__UserI__46E78A0C",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SupportTeamMember",
                columns: table => new
                {
                    SupportTeamMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportTeamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleInTeam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsLeader = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTeamMember", x => x.SupportTeamMemberId);
                    table.ForeignKey(
                        name: "FK__SupportTe__Suppo__412EB0B6",
                        column: x => x.SupportTeamId,
                        principalTable: "SupportTeam",
                        principalColumn: "SupportTeamId");
                    table.ForeignKey(
                        name: "FK__SupportTe__UserI__4222D4EF",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_RaceId",
                table: "Image",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_LocationId",
                table: "Race",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceDistance_RaceId",
                table: "RaceDistance",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceParticipant_DistanceId",
                table: "RaceParticipant",
                column: "DistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceParticipant_UserId",
                table: "RaceParticipant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTeam_LeaderId",
                table: "SupportTeam",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTeam_RaceId",
                table: "SupportTeam",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTeamMember_SupportTeamId",
                table: "SupportTeamMember",
                column: "SupportTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTeamMember_UserId",
                table: "SupportTeamMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D105344A7A851D",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "RaceParticipant");

            migrationBuilder.DropTable(
                name: "SupportTeamMember");

            migrationBuilder.DropTable(
                name: "RaceDistance");

            migrationBuilder.DropTable(
                name: "SupportTeam");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
