FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet NetCoreStack.Localization.Test.Hosting.dll