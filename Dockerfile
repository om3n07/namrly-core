FROM microsoft/aspnetcore-build
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet namrly.dll