FROM microsoft/dotnet:3.0-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:3.0-sdk-stretch AS build
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