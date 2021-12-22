using System;
using System.Collections.Generic;
using RPA.UiPath.Classlib.Models.B2B;
using RPA.UiPath.Classlib.Activities.B2B;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var tmps = new List<ExportTemp> { new ExportTemp() };
            tmps.First().MicrosoftDailyOrigins = new List<MicrosoftDailyOrigin> {  new MicrosoftDailyOrigin
            {
                ChannelName = "渠道",
                CompanyName = "公司",
                IsRegistered = true,
                RegistrationTime = "2021-12-35 12:34:35",
                Title = "职位"
            } };

            tmps.First().MicrosoftDailyStatistics = new List<MicrosoftDailyStatistics> {  new MicrosoftDailyStatistics
            {
                ChannelName = "渠道",
                NumberOfRegistration = 123
            },new MicrosoftDailyStatistics
            {
                ChannelName = "渠道1",
                NumberOfRegistration = 456
            } };

            tmps.First().MicrosoftDailyOrigins = new List<MicrosoftDailyOrigin>();

            var compose = new B2B.Export();
            compose.Excute(tmps);
        }
    }
}
