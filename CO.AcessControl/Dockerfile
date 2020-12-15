FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /ac

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ./CO.AcessControl/*.csproj ./CO.AcessControl/
COPY ./CO.AccessControl.Core/*.csproj ./CO.AccessControl.Core/
COPY ./CO.AcessControl.AcessClient/*.csproj ./CO.AcessControl.AcessClient/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /ac
COPY --from=build-env /ac/out .
ENTRYPOINT ["dotnet", "CO.AcessControl.dll"]