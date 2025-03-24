dotnet ef migrations add Init --project ./Infrastructure --startup-project ./Server --output-dir ./Data/Migrations

dotnet ef database update --context ApplicationDbContext --project ./Infrastructure --startup-project ./Server

docker-compose up

http://localhost:8888/browser/
Username: user-name@domain-name.com
Password: postgres123

Servers -> Register -> Server...

General
Name: db_host

Connection
Host name/address: db_host
Port: 5432
Maintenance database: postgres
Username: postgres
Password: postgres123