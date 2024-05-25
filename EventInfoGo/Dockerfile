FROM golang:alpine AS builder
WORKDIR /app
COPY . .
RUN go mod download
WORKDIR /app/cmd/EventInfo/
RUN go build -o /app/EventInfoGo

FROM alpine:latest
WORKDIR /app
COPY --from=builder /app/EventInfoGo .
COPY --from=builder /app/config.json .
CMD ["./EventInfoGo"]

