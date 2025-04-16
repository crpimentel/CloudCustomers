# ----------- Build Stage -------------
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build

WORKDIR /source

# Copy everything from the current directory (where the Dockerfile lives)
COPY . .

# Restore dependencies
RUN dotnet restore "CloudCustomer.API.csproj" --disable-parallel

# Publish the app
RUN dotnet publish "CloudCustomer.API.csproj" -c Release -o /app --no-restore

# ----------- Runtime Stage -------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime

WORKDIR /app

# Copy build output from previous stage
COPY --from=build /app .

# Expose port (optional)
EXPOSE 5000

# Run the app
ENTRYPOINT ["dotnet", "CloudCustomer.API.dll"]