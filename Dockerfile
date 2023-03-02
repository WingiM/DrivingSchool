FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DrivingSchool/DrivingSchool.csproj", "DrivingSchool/"]
RUN dotnet restore "DrivingSchool/DrivingSchool.csproj"
COPY . .
WORKDIR "/src/DrivingSchool"
RUN dotnet build "DrivingSchool.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "DrivingSchool.csproj" -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DrivingSchool.dll"]
