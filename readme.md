docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
   -p 1433:1433 --name sql2025 --hostname sql2025 \
   -d mcr.microsoft.com/mssql/server:2025-latest

docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest

# latest RabbitMQ 4.x
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management

