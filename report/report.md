---
title: "ITU-MiniTwit - Group e - Report"
author: "Frederik Hørup <frap>, Marie Johansen <majoh>, Nikolej Lundquist <nivl>, Sara Bagger <salb>, Vitus Brodersen <>"
date: \today
---

# Introduction
*Authors: *



# System


## Architecture and Design
*Authors: *


## Dependencies
*Authors: *


## System States
*Authors: *



# Process


## CI/CD
*Authors: Vitus*

Continuous integration and deployment for this project happens in a semi-automatic set of parallel steps. When a developer creates a pull request on the github repository, multiple actions and third-party tools begin checking the validity of the request. Below are the tools:

- Static analysis and security
    - asd

- Codacy static analysis
    - asd

- SonarCloud code analysis
    - asd

- Build and Test
    - asd

- Staging and Deployment
    - asd
    
## Monitoring
*Authors: Marie
The systems monitorting is setup using the open-source monitoring system Prometheus in colaboration with Grafana for visualizing and quering the metrics.
The `app.MapMetrics();` and `app.UseHttpMetrics();` middleware were added to the pipline. `app.MapMetrics();` exposes the HTTP endpoint for Prometheus to scrape and
`app.UseHttpMetrics();` collect Prometheus metrics for processed HTTP requests (from documentation of UseHttpMetrics).

The monitorting is pull based as the application exposes metrics which are then pulled by Prometheus.

The monitoring as been split up into two dashboards; application metrics and infrastructure metrics.
Application metrics focueses mostly on request rates and displays: CPU Usage in Seconds and HTTP Request Recieved in total as well as split into different types of requests

Infratructure metrics focuses on the server side and displays dashboards contaning information about: memory usage, CPU usage and process uptime

Reactive monitoring because we provide a small amount of dashboard that are mostly operationally-focused and the broad focus has been on measuring availability.
The monitoring has however not moved towards monitoring data to measure user experience or that the business side would benefit from.

There are many ways monitoring could have been improved. For one, database monitoring would have been especially beneficial both both for the operational side and to provide metrics for the business side e.g. number of users in the system

Lastly, the monitoring dashboards provided by Digital Ocean to monitor the VMs has been regullary used.


## Logging
*Authors: *


## Security
*Authors: *
What we did to security hardened the app:
- switched to security hardened docker images

## Availability and Scaling
*Authors: *



# Reflection
*Authors: *



# Use of Generative AI
*Authors: Nikolej*

During the development of this project we used ChatGPT in two main ways:

**Debugging.** Often when encountering unexpected bugs and error messages,
we would consult ChatGPT about the cause and potential fixes
While it could rarely fix the issues entirely by itself, more often than not,
it pointed us in the right direction.

**Research.** This course presents challenges involving numerous technologies,
most of which we were unfamiliar with. As such, ChatGPT was used to summarize lengthy documentation
and provide concrete guides tailored to our situation.

**Generating boilerplate / configurations.** ChatGPT was also used for simple tasks
like generating boilerplate code or writing Github Actions Workflow files.
For example, the `build-report.yml` workflow, that converts the markdown source into a pdf.

The use of AI has made these tasks easier, spared us many frustrations and saved consiberable time during development.
On the flip side, AI may have deprived us the extensive knowledge and experience you gain by,
for example, painstaking reading of documentation or spending hours resolving a tiny bug.