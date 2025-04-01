#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MusicBookingApp.Host/MusicBookingApp.Host.csproj", "src/MusicBookingApp.Host/"]
COPY ["src/MusicBookingApp.Infrastructure/MusicBookingApp.Infrastructure.csproj", "src/MusicBookingApp.Infrastructure/"]
COPY ["src/MusicBookingApp.Application/MusicBookingApp.Application.csproj", "src/MusicBookingApp.Application/"]
COPY ["src/MusicBookingApp.Domain/MusicBookingApp.Domain.csproj", "src/MusicBookingApp.Domain/"]
RUN dotnet restore "./src/MusicBookingApp.Host/MusicBookingApp.Host.csproj"
COPY . .
WORKDIR "/src/src/MusicBookingApp.Host"
RUN dotnet build "./MusicBookingApp.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MusicBookingApp.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Log the environment (debugging purposes)
RUN echo "Running in ASP.NET Core Environment: $ASPNETCORE_ENVIRONMENT"
ENTRYPOINT ["dotnet", "MusicBookingApp.Host.dll"]
