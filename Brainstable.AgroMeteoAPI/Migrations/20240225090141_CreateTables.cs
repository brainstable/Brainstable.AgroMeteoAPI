﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Brainstable.AgroMeteoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meteo_stations",
                columns: table => new
                {
                    meteo_station_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: true),
                    lon = table.Column<double>(type: "double precision", nullable: true),
                    alt = table.Column<double>(type: "double precision", nullable: true),
                    name_rus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name_eng = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country_alpha_2 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meteo_stations", x => x.meteo_station_id);
                });

            migrationBuilder.CreateTable(
                name: "meteo_days",
                columns: table => new
                {
                    meteo_station_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    t = table.Column<double>(type: "double precision", nullable: true),
                    tmin = table.Column<double>(type: "double precision", nullable: true),
                    tmax = table.Column<double>(type: "double precision", nullable: true),
                    r = table.Column<double>(type: "double precision", nullable: true),
                    sh = table.Column<double>(type: "double precision", nullable: true),
                    h = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meteo_days", x => new { x.meteo_station_id, x.date });
                    table.ForeignKey(
                        name: "FK_meteo_days_meteo_stations_meteo_station_id",
                        column: x => x.meteo_station_id,
                        principalTable: "meteo_stations",
                        principalColumn: "meteo_station_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meteo_days");

            migrationBuilder.DropTable(
                name: "meteo_stations");
        }
    }
}
