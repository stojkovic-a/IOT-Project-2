package services

import (
	"EventInfo/internal/config"
	"EventInfo/internal/models"
	"encoding/json"

	mqtt "github.com/eclipse/paho.mqtt.golang"
)

type EventServiceImpl struct {
	cfg         config.Config
	mqttService MqttService
	data        *models.Fields
	events      *models.Events
}

func NewEventService(
	cfg *config.Config,
	mqtt *MqttService) EventService {
	return &EventServiceImpl{
		cfg:         *cfg,
		mqttService: *mqtt,
		data:        new(models.Fields),
		events:      new(models.Events),
	}
}

func (e *EventServiceImpl) ReceiveDataAsync() error {
	topic := e.cfg.MQTT.ToApiTopic
	var data models.Fields
	var messageHandler mqtt.MessageHandler = func(client mqtt.Client, msg mqtt.Message) {
		err := json.Unmarshal(msg.Payload(), &data)
		if err != nil {
			println(err.Error())
		}
		println(data.GlobalActivePower)
		println(data.GlobalReactivePower)
		println(data.GlobalIntensity)
		println(data.Voltage)
		println(data.SubMetering_1)
		println(data.SubMetering_2)
		println(data.SubMetering_3)

		e.MqttHandlerWrapper(&data)
	}
	err := e.mqttService.SubscribeToTopicAsync(&topic, messageHandler)
	if err != nil {
		return err
	}
	return nil
}

func (e *EventServiceImpl) GetLatestInfo() (*models.Fields, error) {
	return e.data, nil
}

func (e *EventServiceImpl) GetLastEvents() (*models.Events, error) {
	return e.events, nil
}

func (e *EventServiceImpl) MqttHandlerWrapper(dataFields *models.Fields) error {
	e.SetLastEvent(dataFields)
	e.SetLatestInfo(dataFields)
	return nil
}

func (e *EventServiceImpl) SetLatestInfo(dataFields *models.Fields) error {
	if dataFields != nil {
		e.data = dataFields
	}
	return nil
}

func (e *EventServiceImpl) SetLastEvent(data *models.Fields) error {
	if data == nil {
		return nil
	}

	if data.GlobalActivePower != "" {
		e.events.GlobalActivePowerEvent = data.GlobalActivePower
		e.events.GlobalActivePowerTimestamp = data.Timestamp
	}

	if data.GlobalReactivePower != "" {
		e.events.GlobalReactivePowerEvent = data.GlobalReactivePower
		e.events.GlobalReactivePowerTimestamp = data.Timestamp
	}

	if data.Voltage != "" {
		e.events.VoltageEvent = data.Voltage
		e.events.VoltageTimestamp = data.Timestamp
	}

	if data.GlobalIntensity != "" {
		e.events.GlobalIntensityEvent = data.GlobalIntensity
		e.events.GlobalIntensityTimestamp = data.Timestamp
	}

	if data.SubMetering_1 != e.events.SubMetering_1Event {
		e.events.SubMetering_1Event = data.SubMetering_1
		e.events.SubMetering_1Timestamp = data.Timestamp
	}

	if data.SubMetering_2 != e.events.SubMetering_2Event {
		e.events.SubMetering_2Event = data.SubMetering_2
		e.events.SubMetering_2Timestamp = data.Timestamp
	}

	if data.SubMetering_3 != e.events.SubMetering_3Event {
		e.events.SubMetering_3Event = data.SubMetering_3
		e.events.SubMetering_3Timestamp = data.Timestamp
	}
	return nil
}
