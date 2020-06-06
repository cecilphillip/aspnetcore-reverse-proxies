FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80;https://+:443 \
    ASPNETCORE_Kestrel__Certificates__Default__Password=1111 \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/https/demo.pfx

EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["apiinsights.csproj", "./"]
RUN dotnet restore "./apiinsights.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "apiinsights.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apiinsights.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apiinsights.dll"]
