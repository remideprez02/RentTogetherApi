using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;
using System.Linq;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Filters.Users;
using RentTogether.Entities.Dto.Conversation;

namespace RentTogether.Dal
{
	public class SqlService : IDal
	{
		private readonly RentTogetherDbContext _rentTogetherDbContext;
		private readonly IMapperHelper _mapperHelper;

		public SqlService(RentTogetherDbContext rentTogetherDbContext, IMapperHelper mapperHelper)
		{
			_rentTogetherDbContext = rentTogetherDbContext;
			_mapperHelper = mapperHelper;
		}
		#region Users
		/// <summary>
		/// Creates the user.
		/// </summary>
		/// <returns>The user async.</returns>
		/// <param name="user">User.</param>
		public async Task CreateUserAsync(User user)
		{
			try
			{
				user.CreateDate = DateTime.Now;
				await _rentTogetherDbContext.Users.AddAsync(user);
				await _rentTogetherDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Checks if user is valide.
		/// </summary>
		/// <returns>The if user is valide async.</returns>
		/// <param name="newUser">New user.</param>
		public async Task<bool> CheckIfUserAlreadyExistAsync(User newUser)
		{
			try
			{
				if (!await _rentTogetherDbContext.Users.AnyAsync(x => x.Email == newUser.Email))
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Gets the user async by identifier.
		/// </summary>
		/// <returns>The user async by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<User> GetUserAsyncById(int id)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Gets the user by basic authentication async.
		/// </summary>
		/// <returns>The user by basic authentication async.</returns>
		/// <param name="userLoginDto">User login dto.</param>
		public async Task<User> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto)
		{
			try
			{
				var user = await _rentTogetherDbContext
						.Users
						.FirstOrDefaultAsync(x => x.Email == userLoginDto.Email && x.Password == userLoginDto.Password);
				if (user != null)
				{
					return user;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Gets the user token expiration date.
		/// </summary>
		/// <returns>The user token expiration date.</returns>
		/// <param name="token">Token.</param>
		public async Task<DateTime> GetUserTokenExpirationDateAsync(string token)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users.FirstOrDefaultAsync(x => x.Token == token);
				return user.TokenExpirationDate;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Deletes the user by identifier async.
		/// </summary>
		/// <returns>The user by identifier async.</returns>
		/// <param name="userId">User identifier.</param>
		public async Task<bool> DeleteUserByIdAsync(int userId)
		{
			try
			{
				var user = await _rentTogetherDbContext
					.Users
					.FirstOrDefaultAsync(x => x.UserId == userId);

				if (user != null)
				{
					_rentTogetherDbContext.Users.Remove(user);
					await _rentTogetherDbContext.SaveChangesAsync();

					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userApiDto.UserId);

				if (user != null)
				{
					var updateUser = _mapperHelper.MapUpdateUserApiDtoToUser(userApiDto, user);
					_rentTogetherDbContext.Users.Update(updateUser);

					await _rentTogetherDbContext.SaveChangesAsync();

					return _mapperHelper.MapUserToUserApiDto(updateUser);
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<UserApiDto>> GetAllUserAsync(UserFilters userFilters)
		{
			try
			{

				var usersIQ = _rentTogetherDbContext.Users.Select(u => u);

				switch (userFilters.DateSort)
				{
					case "date_desc":
						usersIQ = usersIQ.OrderByDescending(s => s.CreateDate);
						break;
					case "date_asc":
						usersIQ = usersIQ.OrderBy(s => s.CreateDate);
						break;
					default:
						break;
				}



				var users = new List<User>();
				var usersApiDto = new List<UserApiDto>();

				//If CityFilter
				if (!String.IsNullOrEmpty(userFilters.CityFilter))
				{
					users = await usersIQ.AsTracking().Where(x => x.City == userFilters.CityFilter).ToListAsync();

					foreach (var usr in users)
					{
						usersApiDto.Add(_mapperHelper.MapUserToUserApiDto(usr));
					}

					if (!String.IsNullOrEmpty(userFilters.DateFilter))
					{
						usersApiDto = usersApiDto.Where(x => x.CreateDate == DateTime.Now.AddDays(double.Parse(userFilters.DateFilter))).ToList();
					}
					return usersApiDto;
				}

				users = await usersIQ.AsTracking().ToListAsync();

				foreach (var usr in users)
				{
					usersApiDto.Add(_mapperHelper.MapUserToUserApiDto(usr));
				}

				if (!String.IsNullOrEmpty(userFilters.CityFilter))
				{
					usersApiDto = usersApiDto.Where(x => x.CreateDate == DateTime.Now.AddDays(double.Parse(userFilters.DateFilter))).ToList();
				}

				return usersApiDto;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserApiDto> GetUserAsyncByToken(string token)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users.SingleOrDefaultAsync(x => x.Token == token && x.IsAdmin == 1);
				if (user != null)
				{
					return _mapperHelper.MapUserToUserApiDto(user);
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
        
		#endregion

		#region Messages

		public async Task<List<MessageApiDto>> GetMessagesAsyncByUserId(int userId)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users
													   .Include(x => x.Messages)
													   .SingleOrDefaultAsync(x => x.UserId == userId);
				if (user != null)
				{
					var messages = user.Messages.Select(x => new MessageApiDto
					{
						MessageId = x.MessageId,
						MessageText = x.MessageText,
						UserId = x.Editor.UserId

					}).ToList();

					return messages;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task AddMessageAsync(MessageDto messageDto)
		{
			try
			{
				var user = await _rentTogetherDbContext.Users
													   .Include(x => x.Messages)
													   .SingleOrDefaultAsync(x => x.UserId == messageDto.UserId);

				if (user != null)
				{
					var message = new Message
					{
						MessageText = messageDto.MessageText,
						Editor = new User { UserId = messageDto.UserId }
					};
					if (user.Messages == null)
					{
						user.Messages = new List<Message>();
					}
					user.Messages.Add(message);
					_rentTogetherDbContext.Users.Update(user);
					await _rentTogetherDbContext.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		#endregion

		#region Conversations
		public async Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto)
        {
            try
            {
				var conversation = new Conversation()
				{
					CreatedDate = DateTime.Now,
					Type = conversationDto.Type
				};

				await _rentTogetherDbContext.Conversations.AddAsync(conversation);
                await _rentTogetherDbContext.SaveChangesAsync();

				var conversationApiDto = new ConversationApiDto()
                {
                    ConversationId = conversation.ConversationId,
                    CreatedDate = conversation.CreatedDate,
                    Type = conversation.Type,
                };
				return conversationApiDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		public async Task<ConversationApiDto> GetConversationAsyncById(int conversationId)
        {
            try
            {
				var conversation = await _rentTogetherDbContext.Conversations
				                                               .FirstOrDefaultAsync(x => x.ConversationId == conversationId);
				if(conversation == null){
					return null;
				}

				var conversationApiDto = new ConversationApiDto()
				{
					ConversationId = conversation.ConversationId,
					CreatedDate = conversation.CreatedDate,
					Type = conversation.Type,
				};

				return conversationApiDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		//public async Task GetAllConversationsAsync(){
		//	try
		//	{
		//		var messages =  _rentTogetherDbContext.Messages.Select(x => x);
		//		var result = messages.Join(_rentTogetherDbContext.Conversations,
		//		                           m => m.Conversation.ConversationId,
		//		                           c => c.ConversationId, (m, c) => new ConversationCompleteApiDto {
		//			ConversationId = m.Conversation.ConversationId,
		//			CreatedDate = m.CreatedDate,
		//			Messages = m,
		//			Type = m.Conversation.Type
					
		//		})
		//		var conversations = new List<ConversationCompleteApiDto>();
		//		foreach (var message in messages)
		//		{
		//			conversations.Add(new ConversationCompleteApiDto(){
		//				ConversationId
		//			})
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new Exception(ex.Message);
		//	}
		//}
        #endregion

	}

}
