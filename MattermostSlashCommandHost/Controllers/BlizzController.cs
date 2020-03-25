using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HttpUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MattermostSlashCommandHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class BlizzController : ControllerBase
    {
        private readonly ILogger<BlizzController> _logger;

        public BlizzController(ILogger<BlizzController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Use Post");
        }

        [HttpPost]
        public IActionResult Post()
        {
            HttpContentParser parser = new HttpContentParser(Request.Body);
            Task.Run(() => parser.Parse()).Wait();
            if (parser.Success)
            {
                if (parser.Parameters["token"] != "zx6ym4b3sbre5dkytg3b4yd4sw")
                {
                    return BadRequest();
                }

                SlashCommandResponse response = new SlashCommandResponse() { response_type = "in_channel" };
                string rawMeetingId = parser.Parameters["text"];
                string meetingId = rawMeetingId.Replace("-", String.Empty);
                if (Regex.IsMatch(meetingId, @"m\d+"))
                {
                    string meetingUrl = $"https://go.blizz.com/join/{meetingId}";
                    response.text = $"{parser.Parameters["user_name"]} invites you to join [blizz Meeting {rawMeetingId}]({meetingUrl})";
                    return Ok(response);
                }
                else
                {
                    response.response_type = "";
                    response.text = "Invalid syntax, use `/blizz <meeting-id>` as trigger";
                    return Ok(response);
                }
            }
            return BadRequest();
        }
    }
}
