using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto.Match;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Common.Helpers
{
    public class MatchesGenerator : IMatchesGenerator
    {
        public MatchesGenerator()
        {
        }

        public List<Match> GenerateMatchesForUser(User user, List<User> users)
        {
            try
            {
                var matchProcessDto = new List<MatchProcessDto>();

                var result = 0;
                var matchFail = false;
                foreach (var userTarget in users)
                {
                    foreach (var userTargetPersonalityValue in userTarget.Personality.PersonalityValues)
                    {

                        var userValue = user.Personality.PersonalityValues.FirstOrDefault(x => x.PersonalityReferencial.PersonalityReferencialId == userTargetPersonalityValue.PersonalityReferencial.PersonalityReferencialId);
                        if (userValue != null)
                        {
                            var variation = 100 * (userTargetPersonalityValue.Value - userValue.Value) / userValue.Value;

                            //If variation is negative
                            if (variation <= 0)
                            {
                                result = 100 - (variation * -1);
                            }
                            else
                            {
                                result = (variation - 100) * -1;
                            }

                            if (result < 50)
                            {
                                matchFail = true;
                            }
                            result = 0;
                        }
                    }
                    if (matchFail != true)
                        matchProcessDto.Add(new MatchProcessDto()
                        {
                            MatchPercent = result,
                            UserTargetId = userTarget.UserId
                        });
                    matchFail = false;
                }

                foreach (var match in matchProcessDto.OrderByDescending(x => x.MatchPercent))
                {

                    user.Matches.Add(new Match()
                    {
                        User = user,
                        TargetUser = users.SingleOrDefault(x => x.UserId == match.UserTargetId)
                    });
                }
                return user.Matches;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
