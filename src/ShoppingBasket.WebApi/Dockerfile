FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/ShoppingBasket.WebApi/ShoppingBasket.WebApi.csproj", "src/ShoppingBasket.WebApi/"]
COPY ["src/ShoppingBasket.Core/ShoppingBasket.Core.csproj", "src/ShoppingBasket.Core/"]
COPY ["src/ShoppingBasket.Application/ShoppingBasket.Application.csproj", "src/ShoppingBasket.Application/"]
COPY ["src/ShoppingBasket.Infrastructure/ShoppingBasket.Infrastructure.csproj", "src/ShoppingBasket.Infrastructure/"]
RUN dotnet restore "src/ShoppingBasket.WebApi/ShoppingBasket.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ShoppingBasket.WebApi"
RUN dotnet build "ShoppingBasket.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ShoppingBasket.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShoppingBasket.WebApi.dll"]