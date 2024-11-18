FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blogs/Blogs.csproj", "Blogs/"]
RUN dotnet restore "./Blogs/Blogs.csproj"
COPY . .
WORKDIR "/src/Blogs"
RUN dotnet build "./Blogs.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blogs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM build as migrationbuilder
ENV PATH $PATH:/root/.dotnet/tools
WORKDIR /src
RUN dotnet tool install --global dotnet-ef
RUN dotnet ef migrations bundle -p ./Blogs/Blogs.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=migrationbuilder /src/efbundle .
ENTRYPOINT ["dotnet", "Blogs.dll"]
