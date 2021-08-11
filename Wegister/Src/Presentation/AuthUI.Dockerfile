FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AuthUI/AuthUI.csproj", "AuthUI/"]
RUN dotnet restore "AuthUI/AuthUI.csproj"
COPY . .
WORKDIR "/src/AuthUI"
RUN dotnet build "AuthUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuthUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthUI.dll"] 