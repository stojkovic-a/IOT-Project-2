# IOT-Project-2

Dataset:
https://data.world/databeats/household-power-consumption

Upuststva za pokretanje:
1. Nakon pullovanja pozicionirati se u iot-project1\nest\crud-service direkotrijum i u terminalu ukucati:docker image build -t nestjs-service-auto:1 .
2. Zatim se pozicionirati u iot-project1\client service i u terminalu ukucati docker image build -t client-service-compose:1 .
3. Nakon toga iz terminala kreirati 2 volume-a:
4. docker volume create influxdb2-config
5. docker volume create influxdb2-data
6. Zatim se pozicionirati u direktorijum iot-project1 i u terminal uneti docker-compose up -d
7. Sada je potrebno otici na link localhost:8086 na kome se nalazi influxdb graficko okruzenje (username:aleksandar10 password:iotPassword10), i u okviru koga treba generisati token za pristup bazi.
8. Nakon toga u direktorijumu iot-project1 u terminalu uneti docker-compose down
9. U okvriu istog direktorijuma izmeniti docker-compose.yml, tako da ENV promenljiva INFLUX_API_TOKEN ima vrednost novodobijenog tokena, nakon cega ponovo u teminral uneti: docker-compose up -d 
10. Nakon toga je potrebno uneti podatke u bazu (iz istog terminala):
11. docker cp ./Dataset/databeats-household-power-consumption/household_power_consumption.csv influxdb-compose:/var/lib/influxdb2
12. Pa onda:
13. docker exec -ti influxdb-compose bash
14. influx write -b PowerConsumption -f /var/lib/influxdb2/household_power_consumption.csv --header "#constant measurement,power_consum" --header "#datatype double,double,double,double,double,double,double,dateTime"
15. API je na portu 7777
