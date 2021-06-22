#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VkApiBot/VkApiBot.csproj", "VkApiBot/"]
RUN dotnet restore "VkApiBot/VkApiBot.csproj"
COPY . .
WORKDIR "/src/VkApiBot"
RUN dotnet build "VkApiBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VkApiBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VkApiBot.dll"]