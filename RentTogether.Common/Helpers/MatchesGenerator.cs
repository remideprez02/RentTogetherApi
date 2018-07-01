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

        List<Match> IMatchesGenerator.GenerateMatchesForUser(User user, List<User> users)
        {
            try
            {
                var matchProcessDto = new List<MatchProcessDto>();
                var percentPerValue = new List<Tuple<int, int>>();

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
                                percentPerValue = new List<Tuple<int, int>>();
                            }
                            if(matchFail != true)
                            {
                                var tuple = new Tuple<int, int>(userValue.PersonalityReferencial.PersonalityReferencialId, result);

                                percentPerValue.Add(tuple);
                            }
                            result = 0;
                        }
                    }
                    if (matchFail != true)
                    {
                        user.Matches.Add(new Match()
                        {
                            User = user,
                            TargetUser = users.SingleOrDefault(x => x.UserId == userTarget.UserId)
                        });
                    }

                    matchFail = false;
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
