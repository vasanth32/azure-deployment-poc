#!/bin/bash
# Bash script to run Entity Framework migrations
# Usage: ./scripts/migrate-database.sh [CONNECTION_STRING]

CONNECTION_STRING=${1:-""}

echo "Running Entity Framework migrations..."

if [ -z "$CONNECTION_STRING" ]; then
    echo "No connection string provided. Using default from appsettings.json"
    dotnet ef database update
else
    echo "Using provided connection string"
    export ConnectionStrings__DefaultConnection="$CONNECTION_STRING"
    dotnet ef database update
fi

echo "Migration completed!"

