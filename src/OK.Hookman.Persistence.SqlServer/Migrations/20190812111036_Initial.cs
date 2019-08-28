using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OK.Hookman.Persistence.SqlServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hmx");

            migrationBuilder.CreateTable(
                name: "Actions",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Headers = table.Column<string>(nullable: true),
                    QueryStrings = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    SenderId = table.Column<int>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: false),
                    ActionId = table.Column<int>(nullable: false),
                    Method = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    QueryStrings = table.Column<string>(nullable: true),
                    Headers = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    RetryCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Actions_ActionId",
                        column: x => x.ActionId,
                        principalSchema: "hmx",
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Receivers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "hmx",
                        principalTable: "Receivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Senders_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "hmx",
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hooks",
                schema: "hmx",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    RequestUrl = table.Column<string>(nullable: true),
                    RequestHeaders = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<int>(nullable: true),
                    ResponseBody = table.Column<string>(nullable: true),
                    ResponseHeaders = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hooks_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "hmx",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hooks_Senders_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "hmx",
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hooks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "hmx",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "hmx",
                table: "Statuses",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Created", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Sending", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Sent", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Failed", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Canceled", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActionId",
                schema: "hmx",
                table: "Events",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ReceiverId",
                schema: "hmx",
                table: "Events",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SenderId",
                schema: "hmx",
                table: "Events",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Hooks_EventId",
                schema: "hmx",
                table: "Hooks",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Hooks_SenderId",
                schema: "hmx",
                table: "Hooks",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Hooks_StatusId",
                schema: "hmx",
                table: "Hooks",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hooks",
                schema: "hmx");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "hmx");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "hmx");

            migrationBuilder.DropTable(
                name: "Actions",
                schema: "hmx");

            migrationBuilder.DropTable(
                name: "Receivers",
                schema: "hmx");

            migrationBuilder.DropTable(
                name: "Senders",
                schema: "hmx");
        }
    }
}
