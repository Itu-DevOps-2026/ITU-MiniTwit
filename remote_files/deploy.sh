#!/usr/bin/env bash
source ~/.bash_profile

cd /minitwit

docker compose -f /minitwit/docker-compose.yml pull
docker compose -f /minitwit/docker-compose.yml up -d
