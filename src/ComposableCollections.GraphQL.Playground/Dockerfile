FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ComposableCollections.GraphQL.Playground/ComposableCollections.GraphQL.Playground.csproj", "ComposableCollections.GraphQL.Playground/"]
RUN dotnet restore "ComposableCollections.GraphQL.Playground/ComposableCollections.GraphQL.Playground.csproj"
COPY . .
WORKDIR "/src/ComposableCollections.GraphQL.Playground"
RUN dotnet build "ComposableCollections.GraphQL.Playground.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComposableCollections.GraphQL.Playground.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComposableCollections.GraphQL.Playground.dll"]
