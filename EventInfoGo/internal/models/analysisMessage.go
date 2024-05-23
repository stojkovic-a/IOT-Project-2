package models

type AnalysisMessage struct {
	Pattern string `json:"pattern"`
	Data    Fields `json:"data"`
}
