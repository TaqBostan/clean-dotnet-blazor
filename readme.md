dotnet ef migrations add Init --project ./Infrastructure --startup-project ./Server --output-dir ./Data/Migrations

dotnet ef database update --context ApplicationDbContext --project ./Infrastructure --startup-project ./Server

docker-compose up