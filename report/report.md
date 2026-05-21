---
title: "ITU-MiniTwit - Group e - Report"
author: "Frederik Hørup <frap>, Marie Johansen <majoh>, Nikolej Lundquist <nivl>, Sara Bagger <salb>, Vitus Brodersen <>"
date: \today
---

# Introduction

_Authors: _

# System

## Architecture and Design

_Authors: Nikolej_

This project is a forked repository of the Chirp! project [@chirp] developed for the course
"Analysis, Design and Software Architecture (Autumn 2025)".
The MiniTwit application architecture, namely the domain model and codebase structure,
is inherited unchanged from the Chirp! project.
Hence, we refer to the diagrams compiled in the Chirp! project report, inserted below for convenience.

![Illustration of the _Chirp!_ data model as UML class diagram.](docs/images/domain_model.png)
_Illustration of the *Chirp!* Domain Model (reused from the Chirp! project - not compiled during this course)._

![Illustration of code base](docs/images/onion_architecture.png)
_Illustration of the Chirp! app codebase structure - based on onion architecture (reused from the Chirp! project - not compiled during this course)._

## Deployment

_Authors: Nikolej_

Below is a diagram of the overall deployment architecture of the MiniTwit application.
The system uses two load balancers and two production server instances each running all containerized services and
both read and write to the same DigitalOcean Managed Database.

This setup is designed to prioritize availability.
One can imagine the stakeholders of a social media platform pushing for availability,
since every second of downtime is potential earnings lost.

The dual load balancer (LB) setup was chosen for this reason.
The primary load balancer initially handles all traffic, but is replaced by the secondary in case of failure.
This is enabled through the heartbeat messages exchanged between the two using VRRP.
The secondary then takes ownership of the reserved IP and resumes operation.
Essentially, one LB is active while the other is on standby,
which keeps the system online and reduces one potential single point of failure.

The same principle is applied to the application layer, by using two MiniTwit production servers.
Both run the exact same services and both are connected to the same database,
which enables traffic to be served by either instance.
This provides redundancy in case one server fails,
allowing the team to bring it back up while the other instance continues to handle traffic.

However, there is one major drawback to this setup. The monitoring stack is duplicated as well.
Logs and metrics for each server are separate, which compromises observability consistency.
If one server goes down, logs and metrics may be lost.
This could be fixed by having a centralized monitoring stack e.g. on a separate device that all application nodes
export to.

Conversely, since both production serves run identical containerized environments,
the system is easily reproducible and scalable.

While the monitoring and logging data is not strictly centralized, the app database itself is.
DigitalOcean provides a service for database hosting reducing the maintenance burden on the group.
This allows focus on other aspects of the project rather than database administration,
in exchange for some loss of control.

![MiniTwit Deployment Diagram](report/images/MiniTwit_deployment_diagram.png)
_MiniTwit Deployment Diagram_

## Dependencies

_Authors: Marie, Frederik, Nikolej, Sara_

Besides the .NET packages used to make the MiniTwit application run,
which can be seen [here](#net-dependencies).
The most important dependencies can be seen below, categorized by type.

*Note: Some, like Docker, can fall into more than one category.*

**Workflow**

- GitHub Actions - CI/CD automation platform to run workflows
- rsync - File synchronization tool

**Deployed**

- DigitalOcean - Cloud hosting provider used to host servers and the database
- Docker - Containerization platform
- Docker Hub - Cloud-based registry for storing and sharing Docker Images
- Nginx - High performance web server and reverse proxy
- Keepalived - High availability and failover service
- Certbot - Tool for automatically issuing and renewing SSL/TLS certificates
- Grafana - Visualization platform for logs and monitoring
- Prometheus - Monitoring service used to collect and store metrics from infrastructure and applications
- Loki - Log aggregation and storage system
- Alloy - Telemetry collector used to collect and forward logs
- Ubuntu - Linux distribution used on our servers
- MySQL - Relational database management system
- Vagrant - Tool for creating and managing reproducible virtualized development environments

**Static Analysis**

- Hadolint - Linter for Dockerfiles
- CSharpier - C# code formatter
- CodeQL - Static analysis engine used to identify security vulnerabilities and code quality issues
- Roslyn - .NET compiler platform for code analysis
- Docker Scout - Security and vulnerability analysis tool for Docker
- SonarCloud - Cloud-based code quality and security analysis platform
- Codacy - Automated code review and quality monitoring platform

**Development**

- Git - Distributed version control system
- GitHub - Web-based platform for hosting Git repositories
- .NET - Microsoft’s development platform and runtime used to make the minitwit application

## System States

_Authors: Vitus_

The project is currently in a good state quality wise.
We have from the beginning focused on always having our tests and static analysis tools accept incoming changes before being allowed to be merged into a production state. 
Even going as far as to set multiple analysis tools to strict. 
As of now, the entire project is compliant with these criteria, and can thus be considered stable. 
Any crashes we have encountered in this project have been interogated and the leading issues fixed.

There have been a few compromises and temporary bending of these quality gating rules. 
The project is developed on top of the Chirp! platform, which was specifically made for dotnet 8, that has become deprecated and outdated since then, having moved onto dotnet 10.
This means that the Docker Scout scan of the image strongly recommends us to update. We have therefore chosen to make the analysis ignore said category explicitly. 
This is not a breaking issue, as long as we keep this in mind.

Below is the latest static analysis checks at the time of writing.
![MiniTwit Deployment Diagram](images/Analysis_checks.png)

_System checks on the recent-most change_

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

_Authors: _

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
