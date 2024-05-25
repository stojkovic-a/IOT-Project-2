package controllers

import (
	"EventInfo/internal/services"

	"github.com/gin-gonic/gin"
)

type EventInfoController struct {
	EventService services.EventService
}

func New(eventService services.EventService) EventInfoController {
	return EventInfoController{
		EventService: eventService,
	}
}

// @Summary Returns the latest info from analysis
// @Produce json
// @Success 200
// @Router /event/latestInfo [get]
func (e *EventInfoController) GetLatestInfo(ctx *gin.Context) {
	data, err := e.EventService.GetLatestInfo()
	if err != nil {
		ctx.JSON(400, err)
		return
	}
	ctx.JSON(200, data)
}

// @Summary Returns last time each event happened from analysis
// @Produce json
// @Success 200
// @Router /event/lastEvents [get]
func (e *EventInfoController) GetLastEvents(ctx *gin.Context) {
	data, err := e.EventService.GetLastEvents()
	if err != nil {
		ctx.JSON(400, err)
		return
	}
	ctx.JSON(200, data)
}

func (e *EventInfoController) RegisterEventInfoRoutes(rg *gin.RouterGroup) {
	eventRoute := rg.Group("/event")
	eventRoute.GET("/latestInfo", e.GetLatestInfo)
	eventRoute.GET("/lastEvents", e.GetLastEvents)
}
