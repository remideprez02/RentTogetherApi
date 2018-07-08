using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.SearchLocation;

namespace RentTogether.Interfaces.Business
{
    public interface ISearchLocationService
    {
        Task<List<SearchLocationApiDto>> GetSearchLocationsAsync(SearchLocationDto searchLocationDto);
    }
}
