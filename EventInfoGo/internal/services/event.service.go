package services

import (
	"EventInfo/internal/models"
)

type EventService interface {
	ReceiveDataAsync() error
	GetLatestInfo() (*models.Fields, error)
	GetLastEvents() (*models.Events, error)
	MqttHandlerWrapper(*models.Fields) error
	SetLatestInfo(*models.Fields) error
	SetLastEvent(*models.Fields) error
}
