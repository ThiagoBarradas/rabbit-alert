FROM microsoft/dotnet:2.2-sdk AS builder

WORKDIR /source
COPY ./ ./

RUN ls
RUN dotnet restore
RUN dotnet publish -c Release --output /app/

FROM microsoft/dotnet:2.2-runtime

WORKDIR /app

COPY --from=builder /app .

RUN ls
ENTRYPOINT ["dotnet", "RabbitAlerts.dll"]