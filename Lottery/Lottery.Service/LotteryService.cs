using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.View.Model;
using Lottery.Data;
using Lottery.Data.Model;
using System.Data.Entity;
using Lottery.Service.UoW;
using Lottery.Mapper;

namespace Lottery.Service
{
    public class LotteryService : ILotteryService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;



        public LotteryService(IRepository<Code> codeRepository, IRepository<Award> awardRepository, IRepository<UserCode> userCodeRepository, IRepository<UserCodeAward> userCodeAwardRepository, DbContext dbContext)
        {
            this._codeRepository = codeRepository;
            this._awardRepository = awardRepository;
            this._userCodeRepository = userCodeRepository;
            this._userCodeAwardRepository = userCodeAwardRepository;
            this._dbContext = dbContext;
        }

        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            using (var uow = new UnitOfWork(_dbContext))
            {
                var code = _codeRepository.GetAll().FirstOrDefault(x => x.CodeValue == userCodeModel.Code.CodeValue);
                if (code == null)
                {
                    throw new ApplicationException("Invalid code.");
                }
                if (code.IsUsed)
                {
                    throw new ApplicationException("Code used.");
                }
                var userCode = new UserCode
                {
                    Code = code,
                    Email = userCodeModel.Email,
                    FirstName = userCodeModel.FirstName,
                    LastName = userCodeModel.LastName,
                    SendAt = DateTime.Now
                };

                _userCodeRepository.Insert(userCode);

                Award award = null;
                if (code.IsWinning)
                {
                    award = GetRandomAward(RuffledType.Immediate);

                    var userCodeAward = new UserCodeAward
                    {
                        Award = award,
                        UserCode = userCode,
                        WonAt = DateTime.Now
                    };
                    _userCodeAwardRepository.Insert(userCodeAward);
                }

                code.IsUsed = true;
                uow.Commit();
                return award?.Map<Award, AwardModel>();
            }
        }

        private Award GetRandomAward(RuffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RuffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository.GetAll().Where(x => x.Award.RuffledType == (byte)type).Select(x => x.Award).GroupBy(x => x.Id).ToList();

            var availableAwards = new List<Award>();
            foreach (var award in awards)
            {
                var numberOfAwardedAwards = awardedAwards.FirstOrDefault(x => x.Key == award.Id)?.Count() ?? 0;
                var awardsLeft = award.Quantity - numberOfAwardedAwards;
                availableAwards.AddRange(Enumerable.Repeat(award, awardsLeft));

            }

            if(availableAwards.Count == 0)
            {
                throw new ApplicationException("We are aout of awards . Sorry");
                
            }

            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, availableAwards.Count);
            return availableAwards[randomAwardIndex];
         }
    }
}
