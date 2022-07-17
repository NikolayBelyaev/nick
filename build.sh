#!/usr/bin/env bash

IMAGE="kids-server"
REMOTE_IMAGE="ryzhik/kids"
DOCKER_HUB_USERNAME="ryzhik"
DOCKER_HUB_PASSWORD="mZ3H2QLs!"
SERVER="root@45.144.66.140"
SERVER_IP="45.144.66.140"
BITBUCKET_BRANCH="development"

set -e

echo -e '\r\n\r\n--- cd Kids'
cd Kids-Server

echo -e '\r\n\r\n--- build'
docker-compose -f docker-compose.development.yml -f docker-compose.override.yml build $IMAGE
echo -e '\r\n\r\n--- build complete'

echo -e '\r\n\r\n--- docker hub login'
docker login --username $DOCKER_HUB_USERNAME --password $DOCKER_HUB_PASSWORD

echo -e '\r\n\r\n--- tag'
docker tag $IMAGE:latest $REMOTE_IMAGE

echo -e '\r\n\r\n--- push'
docker push $REMOTE_IMAGE

echo -e '\r\n\r\n--- add known_hosts'
ssh-keyscan -H $SERVER_IP >> known_hosts

echo -e '\r\n\r\n--- set permissions'
chmod 600 kids

echo -e '\r\n\r\n--- copy compose'
scp -i kids -o UserKnownHostsFile=known_hosts docker-compose.development.yml $SERVER:~/
scp -i kids -o UserKnownHostsFile=known_hosts docker-compose.deploy.yml $SERVER:~/

echo -e '\r\n\r\n--- docker hub login remote'
ssh -i kids -o UserKnownHostsFile=known_hosts -t $SERVER "docker login --username $DOCKER_HUB_USERNAME --password $DOCKER_HUB_PASSWORD"

echo -e '\r\n\r\n--- remote pull'
ssh -i kids -o UserKnownHostsFile=known_hosts -t $SERVER "export DEPLOY_BRANCH=$BITBUCKET_BRANCH && export REMOTE_IMAGE=$REMOTE_IMAGE && docker-compose -f docker-compose.development.yml -f docker-compose.deploy.yml pull"

echo -e '\r\n\r\n--- remote up'
ssh -i kids -o UserKnownHostsFile=known_hosts -t $SERVER "export DEPLOY_BRANCH=$BITBUCKET_BRANCH && export REMOTE_IMAGE=$REMOTE_IMAGE  && docker-compose -f docker-compose.development.yml -f docker-compose.deploy.yml up -d"