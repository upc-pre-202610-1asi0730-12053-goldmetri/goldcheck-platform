FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "GoldMetrics.GoldCheck.Platform.dll"]
