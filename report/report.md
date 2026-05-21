---
title: "ITU-MiniTwit - Group e - Report"
subtitle: |
  DevOps, Software Evolution and Software Maintenance, BSc (Spring 2026)
  **Course code:** BSDSESM1KU
author: |
  Charlotte Planteig <cpla@itu.dk>  
  Frederik Hørup Petersen <frap@itu.dk>  
  Marie Johansen <majoh@itu.dk>  
  Nikolej Lundquist <nivl@itu.dk>  
  Sara Bagger <salb@itu.dk>
  Vitus Brodersen <vitb>
date: \today
---

# Introduction

_Authors: _

# System

## Architecture and Design

_Authors: _

## Dependencies

_Authors: _

## System States

_Authors: _

# Process

## CI/CD

_Authors: Vitus, Frederik, Nikolej_

The CI/CD process is based on GitHub Actions workflows that run on the `main` branch or on a scheduled timer. The MiniTwit repository currently uses the following active workflows to complete CI/CD:

- `static-analysis` — runs on push and pull request events for `main`. It performs:
  - Dockerfile linting with Hadolint against `Dockerfile-MiniTwit`.
  - .NET setup and tool restore.
  - C# formatting validation with `dotnet csharpier check .`.
  - CodeQL initialization for C#.
  - Dependency restore and a strict Roslyn build with `TreatWarningsAsErrors=true`.
  - CodeQL analysis to surface static security issues.
  - Docker Scout scanning for critical vulnerabilities on the DockerHub image.
- `build-and-test` — runs on push and pull request events for `main`. It restores dependencies, performs a clean build, and runs the test suite.
- `SonarCloud & Codacy` — provides external static analysis, code smell detection, and quality gating on Pull Requests.
- `Deploy to Staging VM` — runs on push and pull request events for `main` after the primary checks pass. Builds and pushes the Docker images and deploys them to the DigitalOcean staging Droplet.
- `Deploy To DO` — runs on pushes to `main` and can also be triggered manually. It builds and pushes the Docker images and deploys them to the DigitalOcean production Droplets.
- `automatic-weekly-release` — runs on schedule every Tuesday at 08:00 UTC and can be started manually. It builds the project, runs tests, packages release artifacts, and creates a GitHub release.

![CI-CD pipeline](images/CI-CD.png)

_CI-CD pipeline_

## Monitoring

_Authors: Marie, Sara, Nikolej_

Monitoring is set up using Prometheus [@prometheus_docs] and Grafana [@grafana_docs] for visualizing and querying metrics,
by adding the `app.MapMetrics();` and `app.UseHttpMetrics();` middleware the pipeline.
`app.MapMetrics();` exposes the HTTP endpoint for Prometheus to scrape and `app.UseHttpMetrics();` collects Prometheus metrics for processed HTTP requests (from documentation of UseHttpMetrics).

The setup is pull-based as the application exposes metrics which are then pulled by Prometheus.

In Grafana, the monitoring has been split up into two dashboards; application metrics and infrastructure metrics (see video [Monitoring Dashboards](../README.md:80)).
Application metrics focuses mainly on request rates. It displays CPU usage in seconds, the amount of HTTP request received, and statistics on different types of requests.
Infrastructure metrics focus on the server side and display dashboards containing information about: memory usage, CPU usage and process uptime.

The application's monitoring is at the reactive level [@turnbull2014art],
as only a limited set of primarily operational dashboards are provided, with the main focus being on measuring availability.
The monitoring does not supply data to measure user experience or data to benefit the business side.

There are many ways monitoring could be improved.
For one, database monitoring would have been especially beneficial both for the operational side and to provide metrics for the business side e.g. number of users in the system.

Lastly, the monitoring dashboards provided by DigitalOcean to monitor the Droplets has been regularly used.

## Logging

_Authors: Nikolej_

The system uses a minimal push-based logging stack utilizing Alloy [@alloy_docs], Loki [@loki_docs] and Grafana [@grafana_docs],
focusing on aggregation and visualization rather than analysis.
The Grafana-Loki stack was primarily chosen because it is cheap to run
and for its native support for Grafana, which the existing monitoring system was already using.

Alloy collects logs from running Docker containers through the Docker socket `host = unix:///var/run/docker.sock`,
and populates them with metadata like labels before forwarding to Loki for storage.
Lastly, the logs are queried by Grafana and presented visually.

Our chosen focus on aggregation and visualization is accomplished by grouping logs by containers in Grafana.
The default Grafana Logs Drilldown page is more than sufficient for this purpose,
hence there are no custom dashboards.

By providing a centralized view of all system logs,
this setup improves observability significantly, allowing easier searching and faster response in case of errors.

Even though the logging setup was used in a limited capacity during development,
it proved to be especially useful if for instance the simulation suddenly reports failed requests.
The MiniTwit container logs include errors, HTTP requests and database queries,
which allows us to pinpoint the problem quickly.
Or if the monitoring setup indicates something unexpected,
the aggregated logging platform allows us to ascertain whether it is a problem with the monitoring stack or the application itself.


## Security

_Authors: Sara_

We applied several security hardening measures to our system across infrastructure, network, CI/CD, and container configuration.

**Reverse proxy/TLS.**
First, we deployed a Nginx reverse proxy to the application and enabled HTTPS using TLS certificates.
The reverse proxy terminates incoming HTTPS traffic and forwards requests internally to the application containers.
This improves security by encrypting communication between clients and the server and by reducing direct exposure of the application itself.
We used Let’s Encrypt certificates together with automatic renewal through Certbot to avoid manual certificate management and ensure continued HTTPS availability.

**CI/CD.**
In the CI/CD pipeline, we integrated automated security analysis tools to support a shift-left security approach, where vulnerabilities are detected before deployment.
We added GitHub CodeQL analysis to statically scan the application source code for known security vulnerabilities and insecure coding patterns.
Additionally, we integrated Docker Scout to scan container images for known common vulnerabilities and exposures and vulnerable dependencies.
The pipeline was configured to fail on critical vulnerabilities, preventing insecure images from being deployed automatically.

**Docker hardened images.**
We also hardened our Docker environment by switching several production services to Docker Hardened Images (DHI).
Specifically, we replaced standard Grafana, Prometheus, and ASP.NET images with hardened variants from dhi.io.
These images are designed to reduce attack surfaces by minimizing unnecessary packages and dependencies.

**Container execution.**
Beyond changing base images, we further hardened container execution.
For example, Grafana was configured to run as a non-root user instead of running with root privileges.  
We also introduced initialization steps to ensure correct permissions on mounted volumes without granting unnecessary privileges to the running application containers.

A key lesson from the course was that security should not rely on a single mechanism.
Therefore, our approach combined multiple layers of protection: encrypted communication through TLS, restricted network access through firewalls, automated vulnerability scanning in CI/CD, hardened container images, and safer runtime configurations.
This follows a defense-in-depth strategy, where multiple independent security mechanisms reduce the likelihood that a single vulnerability compromises the entire system.

## Availability and Scaling

_Authors: Frederik, Nikolej, Sara, Marie_

To increase the availability of the system, a simple load balancing setup was implemented (see the diagram in the deployment section).
One primary load balancer initially handles all traffic, but is automatically replaced by a secondary backup in case of failure.
Additionally, the system was scaled to run two application instances simultaneously on seperate DigitalOcean Droplets.

This could be improved by running at least 2 instances of the application on each of the two Droplets,
and subsequently implementing an update strategy in the CI/CD pipeline. For instance, the blue-green upgrade strategy.

# Reflection

## Evolution and refactoring

_Authors: Frederik, Sara, Nikolej_

For the simulator to function correctly, several new endpoints specified in the Swagger documentation had to be implemented. 
To simplify this process, the OpenAPI Generator CLI tool was used to generate the initial endpoint structure, after which the individual endpoints were adapted to meet the required functionality and expected request behavior. Furthermore, token-based authorization was implemented for the endpoints that required authentication.

The system used a SQLite database running in the MiniTwit application. As a consequence, all persisted data was lost whenever a new deployment of the application occurred. There was therefore a need to migrate to a new database type to enable persitance. It was chosen to migrate to a MySQL database which was hosted on DigitalOcean. This was chosen due to its relative simplicity and time effectiveness.

## Operations

_Authors: Frederik, Marie_

Monitoring dashboards and application logs were checked regularly to detect excessive system strain and identify any runtime errors. In addition, the simulator status page was monitored to identify failures or inconsistencies that were not captured by the existing logging infrastructure.

## Maintenance

_Authors: Frederik, Sara_

During the period following the start of the simulator, several errors and faults were identified. User registration performed by the simulator was not functioning as intended, authentication for multiple endpoints behaved incorrectly, and several endpoints returned invalid response types or response bodies. To fix these issues, swarming was used to quickly discover where things were going wrong. A complete overview of the issues can be found at [#29](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/29), [#32](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/32), [#34](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/34) and [#41](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/41)

We were notified that Grafana was returning an internal server error when users attempted to log in. Investigation revealed that Prometheus had exhausted the storage capacity of the droplet, preventing Grafana from accessing its internal volume correctly.

To resolve the issue, a Docker data directory used for temporary storage was cleared, after which the Docker containers on the virtual machine were restarted. To prevent similar incidents in the future, storage retention policies were configured for Prometheus, and Prometheus was assigned a dedicated volume for persistent storage. To see the whole bug report see issue [#81](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/81).

After discovering that the application was unavailable, the first step was to inspect the logs.  The logs revealed that the outage was caused by thread pool starvation. Further investigation showed that certain requests to the application was taking upwards of 46 minutes. By analyzing at the database activity, it became evident that queries were scanning more than 200,000 rows per second. It was therefore decided that the database required proper indexing to mitigate the issue. After the necessary indexes were added and the application was restarted, the system returned to stable operation and performed as expected. The full bug report can be found on issue [#99](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/issues/99)

![Database load](images/database_fetch.png)

_Database load before indexing._

## Reflect and describe what was the "DevOps" style of work

_Authors: Sara, Marie, Nikolej_

Compared to earlier software projects, this course introduced us to several DevOps practices that changed both our workflow and our understanding of software development. Many of these practices reflected the principles behind the Three Ways of DevOps[@kim2021devops]: improving flow, enabling fast feedback, and encouraging continuous improvement through automation, monitoring, and maintenance.
A major difference compared to earlier projects was the amount of automation involved in the workflow. Tasks such as testing, linting, building containers, generating reports, and deployment were automated through pipelines and scripts. This reduced repetitive manual work and improved consistency across the project.

**Continuous Integration (CI)**
Automatically running tests/linting on every pull request was a major change compared to earlier projects. 
Previously, broken code would mainly be picked up manually during pull request reviews, which depended heavily on reviewers noticing issues. 
Now, with CI pipelines in place, running automatic tests, linting, static analysis tools etc. caught issues early with fast automated feedback.
This improved confidence when merging code and reduced the risk of introducing bugs into production.

**Continuous Deployment (CD)**
Automated deployment significantly improved the deployment process compared to earlier projects, where the steps toward deployment were manual and inconsistent.
Using CD made our deployments faster, easily reproducable and less error-prone. At the same time, setting up this deployment infrastructure was not without faults.

**Monitoring & Software Maintenance** 
Monitoring through collecting logs and metrics made a big difference for us compared to earlier projects. Using tools such as Grafana and Prometheus gave us a much better understading of the system's runtime behaviour, and once set up correctly, the logs made a big difference in debugging.
Identifying bottlenecks or failures that would otherwise have been difficult to detect, became significantly easier and made us focus more on maintaining the software in production, rather than only focussing on implementing functionality. Reliability and stability became important parts of the development process rather than something considered only at the end.

**Infrastructure as Code (IaC)**
Luckily, we were never forced to take the whole system down and bring it back online.
Towards the end of the project, we did attempt to ensure `vagrant up` could do exactly that, recreating our infrastructure from scratch.
Ultimately, there were some issues that we did not have time to fix, hence the [pull request #90](https://github.com/Itu-DevOps-2026/ITU-MiniTwit/pull/90) remains open.
A video demo of `vagrant up` can be found [here](images/group_e_vagrant_up_demo.gif).

# Use of Generative AI
_Authors: Nikolej, Sara_

During the development of this project we used ChatGPT in three main ways:

**Debugging.** When encountering unexpected bugs and unclear error messages,
we often consulted ChatGPT about possible causes and fixes.
While it rarely solved issues entirely on its own, it frequently helped narrow down the problem space and suggested relevant debugging strategies.

**Research.** This course presents challenges involving numerous technologies,
many of which we were unfamiliar with, including monitoring tools, docker and load balancing.
ChatGPT was used to summarize lengthy documentation, explain unfamiliar concepts, and provide concrete guides tailored to our situation.

**Generating boilerplate / configurations.** ChatGPT was also used for simple, repetitive tasks
such as generating boilerplate code or writing Github Actions Workflow files.
For example, the `build-report.yml` workflow, that converts the markdown source into a pdf.

The use of AI has made these tasks easier, spared us many frustrations during troubleshooting and saved considerable time during development.
At the same time, we met certain limitations when using AI. Suggested fixes sometimes appeared plausible while being incorrect or incompatible with our setup. 
Additionally, relying on AI summaries may have reduced some of the deeper understanding that can come from manually reading documentation or solving problems independently, like spending hours solving a tiny bug. 
As a result, we found that generative AI was most useful as a supporting tool rather than a replacement for critical thinking, testing, and technical understanding.

# References

# Appendix
## .NET dependencies <a name="NET"></a>

Dependencies of ITU-MiniTwit according to `dotnet list package`:

Project 'MiniTwit.Core' has the following package references
[net8.0]: No packages were found for this framework.

Project 'MiniTwit.Infrastructure' has the following package references  
 [net8.0]:
Top-level Package Requested Resolved

> Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.21 8.0.21  
> Microsoft.Data.Sqlite 8.0.8 8.0.8  
> Microsoft.Data.Sqlite.Core 8.0.8 8.0.8  
> Microsoft.EntityFrameworkCore 9.0.0 9.0.0  
> Microsoft.EntityFrameworkCore.Design 9.0.0 9.0.0  
> Microsoft.EntityFrameworkCore.Sqlite 8.0.8 8.0.8  
> Microsoft.EntityFrameworkCore.Tools 9.0.0 9.0.0  
> Pomelo.EntityFrameworkCore.MySql 9.0.0 9.0.0  
> SQLitePCLRaw.bundle_e_sqlite3 3.0.2 3.0.2

Project 'MiniTwit.Web' has the following package references
[net8.0]:
Top-level Package Requested Resolved

> AspNet.Security.OAuth.GitHub 8.3.0 8.3.0  
> DotNetEnv 3.1.1 3.1.1  
> Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.21 8.0.21  
> Microsoft.AspNetCore.Identity.UI 8.0.21 8.0.21  
> Microsoft.Data.Sqlite 8.0.8 8.0.8  
> Microsoft.Data.Sqlite.Core 8.0.8 8.0.8  
> Microsoft.EntityFrameworkCore 9.0.0 9.0.0  
> Microsoft.EntityFrameworkCore.Design 9.0.0 9.0.0  
> Microsoft.EntityFrameworkCore.Sqlite 8.0.8 8.0.8  
> Microsoft.EntityFrameworkCore.Tools 9.0.0 9.0.0  
> Microsoft.Playwright.NUnit 1.43.0 1.43.0  
> Microsoft.VisualStudio.Web.CodeGeneration.Design 9.0.0 9.0.0  
> Pomelo.EntityFrameworkCore.MySql 9.0.0 9.0.0  
> prometheus-net.AspNetCore 8.2.1 8.2.1  
> Serilog.AspNetCore 8.0.3 8.0.3  
> Serilog.Sinks.Console 6.1.1 6.1.1  
> Serilog.Sinks.Grafana.Loki 8.3.2 8.3.2  
> SQLitePCLRaw.bundle_e_sqlite3 3.0.2 3.0.2  
> Swashbuckle.AspNetCore 10.1.2 10.1.2  
> Swashbuckle.AspNetCore.Annotations 10.1.2 10.1.2  
> Swashbuckle.AspNetCore.SwaggerGen 10.1.2 10.1.2
