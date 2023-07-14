#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Apparently this image is only for production deployments
# Do we need to use a different image for development? use local, with no containers?

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80 443
COPY .env /app/
# Dev ports
# EXPOSE 53526 5030 7219 5030

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./AccountOrganizationService.csproj", "AccountOrganizationService/"]
RUN dotnet restore "AccountOrganizationService/AccountOrganizationService.csproj"
COPY [".", "AccountOrganizationService/"]
WORKDIR "/src/AccountOrganizationService"
RUN dotnet build "AccountOrganizationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountOrganizationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountOrganizationService.dll"]
