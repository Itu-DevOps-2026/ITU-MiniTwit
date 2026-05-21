# ITU-MiniTwit
#### Group-e 2026
---
Welcome to the ITU Minitwit platform for group e of the "DevOps, Software Evolution and Software Maintenance, BSc (Spring 2026)" elective course at the IT University of Copenhagen. This project is mainly focused around the continious development, upkeep and deployment of an active IT system.

# Description
The Minitwit app is a Twitter-like platform that is an extension of the Chirp! application from the third semester Analysis, Design and Software Architecture (Autumn 2025) course. It is a deployed platform, having been deployed to DigitalOcean as a droplet Virtual Machine (VM). It contains both the business logic, as well as containers for monitoring and other operations needs.

The development of the MiniTwit application includes workflows for Continuous Integration and Deployment (CI/CD), including external tools like SonarCloud and Codacy.

Monitoring is done with a combination of Prometheus and Grafana.

Alloy, Loki and Grafana constitutes the logging stack.

# Prerequisites

Before being able to work on the MiniTwit application, a few requisites are needed. 

* You need to have vagrant version 2.4.9 & dotnet 8 installed.

This is necessary for the using the development work environment of the MiniTwit platform on your device.

* Ensure you have a DigitalOcean token and SSH key.

As the MiniTwit application is deployed to DO (DigitalOcean) as a virtual machine droplet on their remote servers.

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
To provision the VM for deployment, vagrant is used to do so easily.

Run `vagrant up`

This will set up a provisioned VM for use in production.

# Running locally
Running locally is easily done, as MiniTwit can be launched directly as a dotnet application. To do so, navigate to the `src/MiniTwit.Web` folder.

Run `dotnet run`

MiniTwit will then be hosted on your machine under a localhost port.
# How to contribute

The Minitwit development workflow is designed to follow CI/CD principles. To make a contribution or update a repository file, a list of steps must be fulfilled before making it to the production. 

After having a contribution done and ready, make sure to have done the following:

- checked the code with `Roslyn`, `Csharpier`, `CodeQL`, and `Hadolint`. These will be checked on a pull request automatically.



- Make sure it can build, tests do not fail, and that it can be put to staging.

- Get acceptance from `Codacy`, `SonarCloud`, and `Docker Scout`.

- Finally, the Pull Request is set to require a different developer to review the work done, before it is accepted into the main branch, and subsequently automatically released and deployed accordingly.

All above steps are automatically tested for and will if necessary block pushes or Pull Requests on github via Github Actions and external tools.
# Video Demonstrations

## Monitoring Dashboards

![](report/images/group_e_monitoring_demo.gif)

## Logging Dashboards

![](report/images/group_e_logging_demo.gif)

## IaC - `vagrant up`

![](report/images/group_e_vagrant_up_demo.gif)

## CI/CD

![](report/images/group_e_CI-CD_demo.gif)
