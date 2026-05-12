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

Continuous integration and deployment for this project happens in a semi-automatic set of parallel steps. When a developer creates a pull request on the github repository, multiple actions and third-party tools begin checking the validity of the request. Below are the steps:

- Static analysis and security github action
    Is a compound action for linting, formatting, and security:
    - Hadolint: Dockerfile Linter
    
    Checks the linked dockerfile for linting issues
    - CSHarpier: C# Formatter

    Checks the entire C# codebase for formatting errors, including spaces
    - Roslyn: C# Linter

    Checks entire codebase for linting, especially typesafety
    - CodeQL: Static Code Security

    Checks code against a database of known security flaws
    - Docker Scout: Docker Image Vulnerability scanner

    Checks the docker image on the DockerHub for known vulnerabilities

- Codacy static analysis
    - asd

- SonarCloud code analysis
    - asd

- Build and Test action
    - asd

- Staging and Deployment action
    - asd

- Review and Automatic deployment
    - asd

## Monitoring
*Authors: *


## Logging
*Authors: *


## Security
*Authors: Sara*
- Reverse proxy, https using tls certif
- Firewall
- In CI/CD: security analysis tools 
- Docker hardened images: switching services to DHI
- Grafana as non-root user?


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