using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_09092023.Migrations
{
    /// <inheritdoc />
    public partial class InsertGoalsValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Teams Set GoalsScored = 22, GoalsConceded = 20 where Id=1");
            migrationBuilder.Sql("update Teams Set GoalsScored = 23, GoalsConceded = 28 where Id=2");
            migrationBuilder.Sql("update Teams Set GoalsScored = 15, GoalsConceded = 11 where Id=3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
