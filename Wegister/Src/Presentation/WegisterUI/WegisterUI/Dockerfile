FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Src/Presentation/WegisterUI/WegisterUI/WegisterUI.csproj", "Src/Presentation/WegisterUI/WegisterUI/"]
COPY ["Src/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/"]
COPY ["Src/Application/Application.csproj", "Src/Application/"]
COPY ["Src/Domain/Domain.csproj", "Src/Domain/"]
COPY ["Src/Common/Common.csproj", "Src/Common/"]
COPY ["Src/Persistence/Persistence.csproj", "Src/Persistence/"]
RUN dotnet restore "Src/Presentation/WegisterUI/WegisterUI/WegisterUI.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/WegisterUI/WegisterUI"
RUN dotnet build "WegisterUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WegisterUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WegisterUI.dll"]