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
## Monitoring
*Authors: *


## Logging
*Authors: *


## Security
*Authors: *


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