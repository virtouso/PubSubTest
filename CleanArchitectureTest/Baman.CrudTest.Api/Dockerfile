FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest.Api/Mc2.CrudTest.Api.csproj", "Mc2.CrudTest.Api/"]
COPY ["Mc2.CrudTest.Common.Swagger/Mc2.CrudTest.Common.Swagger.csproj", "Mc2.CrudTest.Common.Swagger/"]
COPY ["Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "Mc2.CrudTest.Application/"]
COPY ["Mc2.CrudTest.Infrastructure/Mc2.CrudTest.Infrastructure.csproj", "Mc2.CrudTest.Infrastructure/"]
COPY ["Mc2.CrudTest.Domain/Mc2.CrudTest.Domain.csproj", "Mc2.CrudTest.Domain/"]
RUN dotnet restore "Mc2.CrudTest.Api/Mc2.CrudTest.Api.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest.Api"
RUN dotnet build "Mc2.CrudTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Api.dll"]
