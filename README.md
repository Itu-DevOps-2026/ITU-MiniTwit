# Prerequisites
You need to have vagrant version 2.4.9 & dotnet 8 installed.
<br>
Ensure you have a Digital Ocean token and SSH key.

# Cloning the repsository
Run `git clone git@github.com:Itu-DevOps-2026/ITU-MiniTwit.git`
<br>
`cd ITU-MiniTwit` 

# Setup environment variables
Create a .env file in root of the repository:
<br>
`DIGITAL_OCEAN_TOKEN=<token>`
<br>
`SSH_KEY_NAME=<key_name>`
<br>
Load the variables
<br>
`source .env`

# Provision VM
Run `vagrant up`

# Running locally
Navigate to the `src/MiniTwit.Web` folder.
<br>
Run `dotnet run`
