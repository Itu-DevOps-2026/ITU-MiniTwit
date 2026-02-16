# Prerequisites
You need to have vagrant version 2.4.9 & dotnet 8 installed.

# Cloning the repsository
Run `git clone git@github.com:Itu-DevOps-2026/ITU-MiniTwit.git`
cd ITU-MiniTwit.git 

# Setup environment variables
Create a .env file in root of repo:
`DIGITAL_OCEAN_TOKEN=<token>`
`SSH_KEY_NAME=<key_name>`

# Provision VM
Run `vagrant up --provider=digital_ocean`

# Running in development environment

Navigate to the `src/Chirp.Web` folder.
<br>
Run `dotnet run`

## Register user with OAuth

If you wish to use the application's OAuth functionality, you need to create an enviroment variables file and paste in the secrets.

In `src/Chirp.Web` create a `.env` file with the following contents: <br>
```
AUTHENTICATION_GITHUB_CLIENTID="[Put client ID here]"
AUTHENTICATION_GITHUB_CLIENTSECRET="[Put client secret here]"
```
Client ID and secrets are stored in the Azure for this web app, bdsa2024group8chirprazor2025, under settings/environment variables.

There should be a `env.sample` file in the same folder, you can use as a template.

# Run from release

Download the release for your operating system and run the `Chirp.Web` executable. <br>
OAuth Github functionality is NOT supported in the released version. If you want to register via Github use the deployed version detailed below.

# Deployed version

Go to https://bdsa2024group8chirprazor2025.azurewebsites.net/ <br>
This version supports login using OAuth Github.
