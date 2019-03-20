FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["webhook.csproj", ""]
RUN dotnet restore "webhook.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "webhook.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "webhook.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Webhook.dll"]