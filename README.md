# IOT-Project-2

Dataset:
https://data.world/databeats/household-power-consumption

Upuststva za pokretanje:
1. Nakon pullovanja pozicionirati se u IOT-Project-2\Sensor\sensor direkotrijum i u terminalu ukucati:docker image build -t sensor:1 .
2. Zatim se pozicionirati u IOT-Project-2\Analytics\Analytics i u terminalu ukucati docker image build -t analytics:1 .
3. Onda se pozicionirati u IOT-Project-2\EventInfoGo i u termianlu ukucati docker image build -t eventinfo:1 .
4. Nakon toga iz terminala kreirati 3 volume-a:
5. docker volume create influxdb2-config
6. docker volume create influxdb2-data
7. docker volume create mosquitto.conf
8. Zatim se pozicionirati u direktorijum IOT-Project-2 i u terminal uneti docker-compose up -d
9. Sada je potrebno otici na link localhost:8086 na kome se nalazi influxdb graficko okruzenje (username:aleksandar10 password:iotPassword10), i u okviru koga treba generisati token za pristup bazi.
10. Nakon toga u direktorijumu IOT-Project-2 u terminalu uneti docker-compose down
11. U okvriu istog direktorijuma izmeniti docker-compose.yml, tako da ENV promenljiva INFLUX_API_TOKEN ima vrednost novodobijenog tokena, nakon cega ponovo u teminral uneti: docker-compose up -d 
12. Nakon toga je potrebno uneti podatke u bazu (iz istog terminala):
13. docker cp ./Dataset/databeats-household-power-consumption/household_power_consumption.csv influxdb-compose:/var/lib/influxdb2
14. Pa onda:
15. docker exec -ti influxdb-compose bash
16. influx write -b PowerConsumption -f /var/lib/influxdb2/household_power_consumption.csv --header "#constant measurement,power_consum" --header "#datatype double,double,double,double,double,double,double,dateTime"
17. Takodje je potrebno podesiti konfiguraciju eclipse mosquitto-a, u direktorijumu IOT-Project-2\Mosquitto otvoriti terminal i unesti:
18. docker cp ./config/mosquitto.conf mosquitto:/mosquitto/config
19. Na kraju ponovo iz terminala u direktorijumu IOT-Project-2 stopirati docker compose:
20. docker-compose down
21. I ponovo ga pokrenuti:
22. docekr-compose up -d
23. SwaggerUI je na: http://localhost:9998/swagger/index.html
