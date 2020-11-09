FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
COPY ./publish .
ENTRYPOINT ["dotnet", "Validations.dll"]
