using Lottery.Service;
using Lottery.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Lottery.Web.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class LotteryController : ApiController
    {
        private readonly ILotteryService _lotteryService;
        public LotteryController(ILotteryService lotteryService)
        {
            _lotteryService = lotteryService;
        }

        [HttpPost]
        public  AwardModel SubmitCode([FromBody] UserCodeModel userCodeModel)
        {
            return _lotteryService.CheckCode(userCodeModel);
        }

        [HttpGet]
        public List<UserCodeAwardModel> GetAllWinners()
        {
            return _lotteryService.GetAllWinners().ToList();
        }
    }
}
