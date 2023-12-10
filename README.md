# WakeUpServer
This project implements a ASP.NET Core service that allows to wake up computers in the local network via Wake On Lan (WOL).

Use a script (batch or PowerShell) to wake up your computer like this:
>curl -H "Content-Type: application/json" -X PUT http://raspberrypi/api/WakeUpService -d "{\"macAddress\": \"00:11:22:33:44:55\"}"

This project was introduced in my office to prevent that people let computers run over night to access it next day from home. Connect your company using VPN connection and wake up your computer with the command above. No cloud connection required. Read the docs or ask the search service of your choice to find out, how to configure WOL on your computer.

## Features

### WakeUpServer API
By sending a PUT to the url /api/WakeUpService allows to wake up a computer in the local network.
Just send the computers MAC address in the body of the request.

> PUT /api/WakeUpServer
> BODY
> { 
>    "macAddress": "11:22:33:44:55:66"
> }

### Reporting UI
The service provides a little (and ugly) Angular frontend with monthly reporting.

### Swagger Support
All APIs can be accessed and tried out by Swagger. Just use the IP address of the server and add /swagger.
