using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiteTest.Helpers;
using SiteTest.Helpers.TouristApp.Helpers;

namespace SiteTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaImageController : ControllerBase
    {
        private const int ImageWidth = 200, ImageHight = 70;
        private const string FontFamily = "Arial";
        private readonly static Brush Foreground = Brushes.Navy;
        private readonly static Color Background = Color.Silver;
        //деформация текста
        private const int WarpFactory = 5;
        private const Double xAmp = WarpFactory * ImageWidth / 150;
        private const Double yAmp = WarpFactory * ImageHight / 60;
        private const Double xFreq = 2.0 * Math.PI / ImageWidth;
        private const Double yFreq = 2.0 * Math.PI / ImageHight;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public CaptchaImageController(IConfiguration configuration,
            IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }
        [HttpPost("post-guid-captcha")]
        public async Task<IActionResult> GuidCaptcha()
        {
            //await _emailSender.SendEmailAsync("novakvova@gmail.com", "Confirm Email",
            //   $"Please confirm your email by clicking here: " +
            //   $"<a href='jon.jpg'>link</a>");

            //Console.WriteLine(RandomPasswordGenerator.GenerateRandomPassword());
            //return Ok(new { semen = "Peter" });

            string challengeGuid = Guid.NewGuid().ToString();
            string key = CaptchaHelper
                .SessionKeyPrefix + challengeGuid;
            this.HttpContext.Session.SetString(key, CaptchaHelper.MakeRandomSolution());
            return Ok(challengeGuid);
        }

        //[HttpGet]
        //public IActionResult Test()
        //{  

        //    Console.WriteLine(RandomPasswordGenerator.GenerateRandomPassword());
        //    return Ok(new { semen = "Peter" });
        //}
    }
}