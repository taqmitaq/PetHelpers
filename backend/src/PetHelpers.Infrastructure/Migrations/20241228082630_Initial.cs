using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelpers.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "species",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_species", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "volunteers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                years_of_experience = table.Column<int>(type: "integer", nullable: false),
                pets_found_home = table.Column<int>(type: "integer", nullable: false),
                pets_looking_for_home = table.Column<int>(type: "integer", nullable: false),
                pets_in_treatment = table.Column<int>(type: "integer", nullable: false),
                description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                social_medias = table.Column<string>(type: "text", nullable: false),
                requisites = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false),
                phone_number = table.Column<string>(type: "text", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_volunteers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "breeds",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                species_id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_breeds", x => x.id);
                table.ForeignKey(
                    name: "fk_breeds_species_species_id",
                    column: x => x.species_id,
                    principalTable: "species",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "pets",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                health_info = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                location = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                weight = table.Column<double>(type: "double precision", nullable: false),
                height = table.Column<double>(type: "double precision", nullable: false),
                is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                requisites = table.Column<string>(type: "text", nullable: false),
                help_status = table.Column<string>(type: "text", nullable: false),
                owners_phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                species_id = table.Column<Guid>(type: "uuid", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_pets", x => x.id);
                table.ForeignKey(
                    name: "fk_pets_volunteers_volunteer_id",
                    column: x => x.volunteer_id,
                    principalTable: "volunteers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_breeds_species_id",
            table: "breeds",
            column: "species_id");

        migrationBuilder.CreateIndex(
            name: "ix_pets_volunteer_id",
            table: "pets",
            column: "volunteer_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "breeds");

        migrationBuilder.DropTable(
            name: "pets");

        migrationBuilder.DropTable(
            name: "species");

        migrationBuilder.DropTable(
            name: "volunteers");
    }
}
