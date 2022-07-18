FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Monitor2.csproj", "Monitor2/"]
RUN dotnet restore "Monitor2/Monitor2.csproj"
COPY . ./Monitor2
WORKDIR "/src/Monitor2"
RUN dotnet build "Monitor2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Monitor2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Monitor2.dll"]