# Rabbit Alert

```
docker run \
  -e min=5 \
  -e max=10 \
  -e slack_url=https://hooks.slack.com/services/XXXX/XXXX/XXXX \
  -e pushover_user=xxx \ 
  -e pushover_token=xxx -e rabbit_user=reader \
  -e rabbit_pass=xxx \
  -e rabbit_host=http://localhost:15672 \
  -e rabbit_virtual_host=/ \
  -e rabbit_queue=test_queue \
  thiagobarradas/rabbit-alert:latest
```
