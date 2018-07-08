using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.SearchLocation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
    public class SearchLocationService : ISearchLocationService
    {
        private readonly IDal _dal;
        public SearchLocationService(IDal dal)
        {
            _dal = dal;
        }

        public async Task<List<SearchLocationApiDto>> GetSearchLocationsAsync(SearchLocationDto searchLocationDto){
            return await _dal.GetSearchLocationsAsync(searchLocationDto);
        }
    }
}
