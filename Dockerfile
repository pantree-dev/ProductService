

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
WORKDIR /App/src/ProductService/
# Build and publish a release
RUN dotnet publish -c Release -o ../../out -r linux-x64 --self-contained

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "ProductService.dll"]