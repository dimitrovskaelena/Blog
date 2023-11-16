using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class relatedBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedBlogs",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    RelatedBlogsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedBlogs", x => new { x.BlogPostId, x.RelatedBlogsId });
                    table.ForeignKey(
                        name: "FK_RelatedBlogs_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedBlogs_BlogPosts_RelatedBlogsId",
                        column: x => x.RelatedBlogsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 16, 11, 46, 94, DateTimeKind.Local).AddTicks(3409));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 16, 11, 46, 94, DateTimeKind.Local).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 16, 11, 46, 94, DateTimeKind.Local).AddTicks(3453));

            migrationBuilder.CreateIndex(
                name: "IX_RelatedBlogs_RelatedBlogsId",
                table: "RelatedBlogs",
                column: "RelatedBlogsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedBlogs");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9700));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9704));
        }
    }
}
