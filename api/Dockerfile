FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
# ENV ASPNETCORE_URLS=http://+:80 
ENV ASPNETCORE_URLS=http://+:80;https://+:443 \
    ASPNETCORE_Kestrel__Certificates__Default__Password=1111 \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/https/demo.pfx

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["api.csproj", "./"]
RUN dotnet restore "./api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
