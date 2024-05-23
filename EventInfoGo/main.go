package main

import (
	"EventInfo/internal/api/controllers"
	"EventInfo/internal/config"
	"EventInfo/internal/services"
	"log"

	"github.com/gin-gonic/gin"

	ginSwagger "github.com/swaggo/gin-swagger"

	swaggerFiles "github.com/swaggo/files"

	docs "github.com/IOT-Project-2/EventInfoGo/docs"
)

var (
	server          *gin.Engine
	eventService    services.EventService
	mqttService     services.MqttService
	cfg             config.Config
	eventController controllers.EventInfoController
)

func init() {
	cfg = *config.Load()
	mqttService = services.NewMqttService(&cfg)
	err1 := mqttService.ConnectAsync()
	if err1 != nil {
		log.Fatal(err1.Error())
	}
	eventService = services.NewEventService(&cfg, &mqttService)
	err2 := eventService.ReceiveDataAsync()
	if err2 != nil {
		log.Fatalf(err2.Error())
	}
	eventController = controllers.New(eventService)
	server = gin.Default()
	server.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler, url))

}

func main() {

	defer mqttService.DisconnectAsync()
	
	basePath := server.Group("/v1")
	eventController.RegisterEventInfoRoutes(basePath)

	log.Fatal(server.Run(":9998"))
}
