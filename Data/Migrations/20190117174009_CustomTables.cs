using Microsoft.EntityFrameworkCore.Migrations;

namespace Production.Data.Migrations
{
    public partial class CustomTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
             migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new {
                    id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                },
                constraints: table => {
                    table.PrimaryKey("PK_Manufacturer", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new {
                    id = table.Column<int>(nullable: false),
                    materialname = table.Column<string>(maxLength: 100),
                    amount = table.Column<int>(nullable: false),
                    m_id = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Materials", x => x.id);
                    table.ForeignKey(
                        name:"FK_Materials",
                        column: x => x.m_id,
                        principalTable: "Manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );
            
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new {
                    id = table.Column<int>(nullable: false),
                    p_name = table.Column<string>(maxLength: 100, nullable: false),
                    descriptionription = table.Column<string>(maxLength: 5000, nullable: true),
                    amount = table.Column<int>(nullable: false, defaultValue: 0),
                    price = table.Column<float>(nullable: false),
                    m_id = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Product", x=> x.id);
                    table.ForeignKey(
                        name: "FK_Manufacturer_id",
                        column: x => x.m_id,
                        principalTable: "Manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );

                }
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
