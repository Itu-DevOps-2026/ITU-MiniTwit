# ITU-MiniTwit
#### Group e 2026
---
Welcome to the ITU Minitwit platform for group e of the DevOPs elective course at the IT University in Copenhagen 2026. This project is mainly focused around the continious development, upkeep and deployment of an active IT system.

# Description
The Minitwit app is a Twitter-like platform that is an extension of the Chirp! application from the third semester Software design and architecture course. It is a deployed platform, having been deployed to digital ocean as a droplet VM. It contains both the business logic, as well as containers for monitoring and other operations needs.

The development of the MiniTwit application includes workflows for continuous integration and deployment (CI/CD), including external tools like SonarCloud and Codacy.

Monitoring is done with a combination of Prometheus and grafana.

# Prerequisites

Before being able to work on the MiniTwit application, a few requisites are needed. 

* You need to have vagrant version 2.4.9 & dotnet 8 installed.

This is necessary for the using the development work environment of the MiniTwit platform on your device.

* Ensure you have a Digital Ocean token and SSH key.

As the MiniTwit application is deployed to DO (Digital Ocean) as a virtual machine droplet on their remote servers.

# Cloning the repository
The MiniTwit application can be found on the group-e Minitwit github page. To clone the repository to the local machine,
run 

`git clone git@github.com:Itu-DevOps-2026/ITU-MiniTwit.git`

And if necessary, navigate to

`cd ITU-MiniTwit` 

in a terminal.

# Setup environment variables
To start a development environment for the MiniTwit platform, a special environment file containing some keys and tokens is required. To set these up, do the following:

Create a .env file in root of the repository and include these associations
`DIGITAL_OCEAN_TOKEN=<token>`
`SSH_KEY_NAME=<key_name>`

And then load the variables

`source .env`

# Provision VM
To provision the VM for deployment, vagrant is used to do so easily. Only one command is needed:

Run `vagrant up`

This will set up a provisioned VM for use in production.
# Running locally
Navigate to the `src/MiniTwit.Web` folder.
<br>
Run `dotnet run`

# Video Demonstrations

## Monitoring Dashboards

![](report/images/group_e_monitoring_demo.gif)

## Logging Dashboards

![](report/images/group_e_logging_demo.gif)

## IaC - `vagrant up`

![](report/images/MiniTwit_vagrant_up_demo.gif)

## CI/CD
