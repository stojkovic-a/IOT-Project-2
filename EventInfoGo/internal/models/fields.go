package models

type Fields struct {
	GlobalActivePower   string `json:"globalActivePower"`
	GlobalReactivePower string `json:"globalReactivePower"`
	Voltage             string `json:"voltage"`
	GlobalIntensity     string `json:"globalIntensity"`
	SubMetering_1       string `json:"subMetering_1"`
	SubMetering_2       string `json:"subMetering_2"`
	SubMetering_3       string `json:"subMetering_3"`
	Timestamp           string `json:"timestamp"`
}

// type Fields struct {
// 	GlobalActivePower   string   `json:"globalActivePower"`
// 	GlobalReactivePower string   `json:"globalReactivePower"`
// 	Voltage             string   `json:"voltage"`
// 	GlobalIntensity     string   `json:"globalIntensity"`
// 	SubMetering_1       string   `json:"subMetering_1"`
// 	SubMetering_2       string   `json:"subMetering_2"`
// 	SubMetering_3       string   `json:"subMetering_3"`
// 	Timestamp           time.Time `json:"timestamp"`
// }
