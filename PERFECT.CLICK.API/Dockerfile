FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57374
EXPOSE 44332

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY PERFECT.CLICK.API/PERFECT.CLICK.API.csproj PERFECT.CLICK.API/
RUN dotnet restore PERFECT.CLICK.API/PERFECT.CLICK.API.csproj
COPY . .
WORKDIR /src/PERFECT.CLICK.API
RUN dotnet build PERFECT.CLICK.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PERFECT.CLICK.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PERFECT.CLICK.API.dll"]
