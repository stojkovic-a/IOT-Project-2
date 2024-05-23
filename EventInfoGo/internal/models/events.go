package models

// type Events struct {
// 	GlobalActivePowerTimestamp   time.Time `json:"globalActivePowerTimestamp"`
// 	GlobalActivePowerEvent       string    `json:"globalActivePowerEvent"`
// 	GlobalReactivePowerTimestamp time.Time `json:"globalReactivePowerTimestamp"`
// 	GlobalReactivePowerEvent     string    `json:"globalReactivePowerEvent"`
// 	VoltageTimestamp             time.Time `json:"voltageTimestamp"`
// 	VoltageEvent                 string    `json:"voltageEvent"`
// 	GlobalIntensityTimestamp     time.Time `json:"globalIntensityTimestamp"`
// 	GlobalIntensityEvent         string    `json:"globalIntensityEvent"`
// 	SubMetering_1Timestamp       time.Time `json:"subMetering_1Timestamp"`
// 	SubMetering_1Event           string    `json:"subMetering_1Event"`
// 	SubMetering_2Timestamp       time.Time `json:"subMetering_2Timestamp"`
// 	SubMetering_2Event           string    `json:"subMetering_2Event"`
// 	SubMetering_3Timestamp       time.Time `json:"subMetering_3Timestamp"`
// 	SubMetering_3Event           string    `json:"subMetering_3Event"`
// }

type Events struct {
	GlobalActivePowerTimestamp   string `json:"globalActivePowerTimestamp"`
	GlobalActivePowerEvent       string `json:"globalActivePowerEvent"`
	GlobalReactivePowerTimestamp string `json:"globalReactivePowerTimestamp"`
	GlobalReactivePowerEvent     string `json:"globalReactivePowerEvent"`
	VoltageTimestamp             string `json:"voltageTimestamp"`
	VoltageEvent                 string `json:"voltageEvent"`
	GlobalIntensityTimestamp     string `json:"globalIntensityTimestamp"`
	GlobalIntensityEvent         string `json:"globalIntensityEvent"`
	SubMetering_1Timestamp       string `json:"subMetering_1Timestamp"`
	SubMetering_1Event           string `json:"subMetering_1Event"`
	SubMetering_2Timestamp       string `json:"subMetering_2Timestamp"`
	SubMetering_2Event           string `json:"subMetering_2Event"`
	SubMetering_3Timestamp       string `json:"subMetering_3Timestamp"`
	SubMetering_3Event           string `json:"subMetering_3Event"`
}
