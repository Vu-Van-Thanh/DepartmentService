FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DepartmentService/DepartmentService.csproj", "DepartmentService/"]
RUN dotnet restore "DepartmentService/DepartmentService.csproj"
COPY . .
WORKDIR "/src/DepartmentService"
RUN dotnet build "DepartmentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DepartmentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepartmentService.dll"] 