# MattermostSlashCommandHost
Generic .NET Core host for Mattermost Slash commands. Includes a sample command for blizz meetings.

# How to setup
In Mattermost, add a new slash command (Integrations > Slash Commands) and make it point to the application's URL. The port is currently wired to 23456 in config.json and may be changed there. Also, make sure to adapt the token check in the BlizzController to match your token.

# Usage
The sample controller allows to quickly share a link to a blizz meeting by just typing /blizz [MeetingId]. The project would easily allow to add additional controllers (i.e. slash commands) and can be used as a template for simple commands. 
