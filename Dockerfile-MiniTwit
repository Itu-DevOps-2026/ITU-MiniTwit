FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MiniTwit.Web/MiniTwit.Web.csproj", "src/MiniTwit.Web/"]
COPY ["src/MiniTwit.Core/MiniTwit.Core.csproj", "src/MiniTwit.Core/"]
COPY ["src/MiniTwit.Infrastructure/MiniTwit.Infrastructure.csproj", "src/MiniTwit.Infrastructure/"]
RUN dotnet restore "src/MiniTwit.Web/MiniTwit.Web.csproj"
COPY . .
WORKDIR "/src/src/MiniTwit.Web"
RUN dotnet build "./MiniTwit.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniTwit.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniTwit.Web.dll"]
