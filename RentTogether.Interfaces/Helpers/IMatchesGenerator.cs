using System;
using System.Collections.Generic;
using RentTogether.Entities;

namespace RentTogether.Interfaces.Helpers
{
    public interface IMatchesGenerator
    {
        List<Match> GenerateMatchesForUser(User user, List<User> users);
    }
}
