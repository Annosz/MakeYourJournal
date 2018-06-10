using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MakeYourJournal.DAL.Migrations
{
    public partial class VolumeNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Issue_Volume_Number",
                table: "Issue",
                columns: new[] { "Volume", "Number" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Issue_Volume_Number",
                table: "Issue");
        }
    }
}
