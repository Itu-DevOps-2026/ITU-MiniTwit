#!/usr/bin/env bash

cd /minitwit

echo ".ENV CONTENTS:"
cat .env

docker compose pull
docker compose up -d