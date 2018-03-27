using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Business;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Interfaces.Business;
using RentTogetherApi.Interfaces.Dal;
using RentTogetherApi.Interfaces.Helpers;

namespace RentTogetherApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IDal _dal;
        private readonly IMapperHelper _mapperHelper;

        public UserService(IDal dal, IMapperHelper mapperHelper)
        {
            _dal = dal;
            _mapperHelper = mapperHelper;
        }

        public async Task<User> CreateUserAsync(UserRegisterDto userRegisterDto){
            var user = _mapperHelper.MapUserRegisterDtoToUser(userRegisterDto);

            if(await _dal.CheckIfUserIsValideAsync(user)){
                await _dal.CreateUserAsync(user);
                return user;
            }
            throw new Exception();
        }
    }
}
