using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FnolApiVersion2.O.Migrations
{
    /// <inheritdoc />
    public partial class intialcrst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FnolDetails",
                columns: table => new
                {
                    FnolID = table.Column<string>(name: "Fnol_ID", type: "nvarchar(450)", nullable: false),
                    PolicyID = table.Column<string>(name: "Policy_ID", type: "nvarchar(max)", nullable: true),
                    ReporterName = table.Column<string>(name: "Reporter_Name", type: "nvarchar(max)", nullable: true),
                    ReportedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveCase = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FnolDetails", x => x.FnolID);
                    table.ForeignKey(
                        name: "FK_FnolDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentDetails",
                columns: table => new
                {
                    IncidentID = table.Column<string>(name: "Incident_ID", type: "nvarchar(450)", nullable: false),
                    CauseOfIncident = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncidentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartsDamaged = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AddressOfIncident = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    FnolID = table.Column<string>(name: "Fnol_ID", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentDetails", x => x.IncidentID);
                    table.ForeignKey(
                        name: "FK_IncidentDetails_FnolDetails_Fnol_ID",
                        column: x => x.FnolID,
                        principalTable: "FnolDetails",
                        principalColumn: "Fnol_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverDetails",
                columns: table => new
                {
                    DriverID = table.Column<string>(name: "Driver_ID", type: "nvarchar(450)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverLicenseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncidentID = table.Column<string>(name: "Incident_ID", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDetails", x => x.DriverID);
                    table.ForeignKey(
                        name: "FK_DriverDetails_IncidentDetails_Incident_ID",
                        column: x => x.IncidentID,
                        principalTable: "IncidentDetails",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentPictures",
                columns: table => new
                {
                    PictureID = table.Column<string>(name: "Picture_ID", type: "nvarchar(450)", nullable: false),
                    FrontImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BackImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LeftImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RightImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IncidentID = table.Column<string>(name: "Incident_ID", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentPictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_IncidentPictures_IncidentDetails_Incident_ID",
                        column: x => x.IncidentID,
                        principalTable: "IncidentDetails",
                        principalColumn: "Incident_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDetails",
                columns: table => new
                {
                    VehicleID = table.Column<string>(name: "Vehicle_ID", type: "nvarchar(450)", nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleMaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationCertificateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverID = table.Column<string>(name: "Driver_ID", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDetails", x => x.VehicleID);
                    table.ForeignKey(
                        name: "FK_VehicleDetails_DriverDetails_Driver_ID",
                        column: x => x.DriverID,
                        principalTable: "DriverDetails",
                        principalColumn: "Driver_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverDetails_Incident_ID",
                table: "DriverDetails",
                column: "Incident_ID",
                unique: true,
                filter: "[Incident_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FnolDetails_UserID",
                table: "FnolDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetails_Fnol_ID",
                table: "IncidentDetails",
                column: "Fnol_ID",
                unique: true,
                filter: "[Fnol_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentPictures_Incident_ID",
                table: "IncidentPictures",
                column: "Incident_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleID",
                table: "User_Roles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_UserID",
                table: "User_Roles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_Driver_ID",
                table: "VehicleDetails",
                column: "Driver_ID",
                unique: true,
                filter: "[Driver_ID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentPictures");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "VehicleDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DriverDetails");

            migrationBuilder.DropTable(
                name: "IncidentDetails");

            migrationBuilder.DropTable(
                name: "FnolDetails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
