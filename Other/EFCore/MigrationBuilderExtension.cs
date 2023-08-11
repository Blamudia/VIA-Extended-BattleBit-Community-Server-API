using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace BBR.Community.API.Other.EFCore
{
    public static class MigrationBuilderExtensions
    {
        public static OperationBuilder<SqlOperation> RenameConstraint(this MigrationBuilder migrationBuilder, string name, string newName, string table, string? schema = null)
            => migrationBuilder.Sql($"ALTER TABLE {schema ?? "public"}.{table} RENAME CONSTRAINT {name} TO {newName};");
    }
}
