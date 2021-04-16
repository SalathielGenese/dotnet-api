#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
RUN apk add --no-cache bash
WORKDIR /opt/app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS test
WORKDIR /opt/app/TestProgrammationConformitTest
COPY TestProgrammationConformitTest/TestProgrammationConformitTest.csproj .
RUN dotnet restore
COPY . ..
RUN dotnet test --verbosity minimal

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /opt/app/TestProgrammationConformit
COPY TestProgrammationConformit/TestProgrammationConformit.csproj .
RUN dotnet restore TestProgrammationConformit.csproj
COPY TestProgrammationConformit .
RUN dotnet build -c Release -o /opt/build

FROM build AS publish
RUN dotnet publish -c Release -o /opt/publish

FROM base AS final
CMD ["dotnet", "TestProgrammationConformit.dll"]
COPY entrypoint.sh wait-for-it.sh /opt/app/
ENTRYPOINT ["sh", "entrypoint.sh"]
WORKDIR /opt/app
COPY --from=publish /opt/publish .
