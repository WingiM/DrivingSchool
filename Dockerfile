FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY DrivingSchool.sln ./
COPY DrivingSchool.BlazorWebClient/*.csproj ./DrivingSchool.BlazorWebClient/
COPY DrivingSchool.Domain/*.csproj ./DrivingSchool.Domain/
COPY DrivingSchool.Data/*.csproj ./DrivingSchool.Data/
COPY DrivingSchool.GridFS/*.csproj ./DrivingSchool.GridFS/

RUN dotnet restore
COPY . .
WORKDIR "/src/DrivingSchool.Domain"
RUN dotnet build "DrivingSchool.Domain.csproj" -c Release -o /app/build

WORKDIR "/src/DrivingSchool.Data"
RUN dotnet build "DrivingSchool.Data.csproj" -c Release -o /app/build

WORKDIR "/src/DrivingSchool.GridFS"
RUN dotnet build "DrivingSchool.GridFS.csproj" -c Release -o /app/build

WORKDIR "/src/DrivingSchool.BlazorWebClient"
RUN dotnet build "DrivingSchool.BlazorWebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DrivingSchool.BlazorWebClient.dll"]
