FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /usr/src/app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV MQTTServer=mosquitto
ENV MQTTPort=1883
ENV FromSensorTopic=sensor
ENV ToApiTopic=api:To
ENV eKuiperServer=http://localhost:9081
ENV ToEkuiperTopic=eKuiper:To
ENV FromEkuiperTopic=eKuiper:From
COPY ./*.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /usr/src/app
COPY --from=build /usr/src/app/out ./
ENV ASPNETCORE_ENVIRONMENT=Development
ENV MQTTServer=mosquitto
ENV MQTTPort=1883
ENV FromSensorTopic=sensor
ENV ToApiTopic=api:To
ENV eKuiperServer=http://localhost:9081
ENV ToEkuiperTopic=eKuiper:To
ENV FromEkuiperTopic=eKuiper:From
EXPOSE 4444
ENTRYPOINT ["dotnet", "Analytics.dll"]