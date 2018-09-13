﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Data.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LotteryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LotteryContext context)
        {
            var codes = new List<Code>()
            {
                new Code
                {
                    CodeValue = "CC8899",
                    IsWinning = true
                },
                new Code
                {
                    CodeValue = "CC7799",
                    IsWinning = true
                },
                new Code
                {
                    CodeValue = "CC6699",
                    IsWinning = true
                },
                new Code
                {
                    CodeValue = "CC5599",
                    IsWinning = true
                }
            };

            context.Codes.AddRange(codes);

            var awards = new List<Award>
            {
                new Award{AwardName = "Beer", AwardDescription = "You won a beer", Quantity = 100, RuffledType = (byte) RuffledType.Immediate},
                new Award{AwardName = "iPhoneX", AwardDescription = "You won an iPhoneX", Quantity = 2, RuffledType = (byte) RuffledType.PerDay},
                new Award{AwardName = "VW Polo", AwardDescription = "You won a Polo", Quantity = 1, RuffledType = (byte) RuffledType.Final}
            };

            context.Awards.AddRange(awards);
            context.SaveChanges();
        }
    }
}
