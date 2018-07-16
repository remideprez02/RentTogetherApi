//
//Author : Déprez Rémi
//Version : 1.0
//

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
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Dto.Personality;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.TargetLocation;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.SearchLocation;
using RentTogether.Entities.Dto.BuildingHistory;
using RentTogether.Entities.Dto.FavoriteBuilding;
using Microsoft.Extensions.Logging;

namespace RentTogether.Dal
{
    public class SqlService : IDal
    {
        private readonly RentTogetherDbContext _rentTogetherDbContext;
        private readonly IMapperHelper _mapperHelper;
        private readonly ILogger<SqlService> _logger;
        private readonly ICustomEncoder _customEncoder;

        public SqlService(RentTogetherDbContext rentTogetherDbContext, IMapperHelper mapperHelper,
                          ILogger<SqlService> logger, ICustomEncoder customEncoder)
        {
            _rentTogetherDbContext = rentTogetherDbContext;
            _mapperHelper = mapperHelper;
            _logger = logger;
            _customEncoder = customEncoder;
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
                _logger.LogError(ex, ex.Message, "An error occurred while inserting User.");
                throw new Exception(ex.Message, ex);
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
                _logger.LogError(ex, ex.Message, "An error occurred while checking if User exist.");
                throw new Exception(ex.Message, ex);
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
                _logger.LogError(ex, ex.Message, "An error occurred while getting User.");
                throw new Exception(ex.Message, ex);
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
                var pw = _customEncoder.Base64Encode(userLoginDto.Password);
                var user = await _rentTogetherDbContext
                        .Users
                        .FirstOrDefaultAsync(x => x.Email == userLoginDto.Email && x.Password == pw);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting User.");
                throw new Exception(ex.Message, ex);
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
                _logger.LogError(ex, ex.Message, "An error occurred while getting User token expiration.");
                throw new Exception(ex.Message, ex);
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
                _logger.LogError(ex, ex.Message, "An error occurred while deleting User.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userApiDto">User API dto.</param>
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
                _logger.LogError(ex, ex.Message, "An error occurred while updating User.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all user async.
        /// </summary>
        /// <returns>The all user async.</returns>
        public async Task<List<UserApiDto>> GetAllUserAsync()
        {
            try
            {
                var users = await _rentTogetherDbContext.Users.ToListAsync();

                var usersApiDto = new List<UserApiDto>();

                foreach (var usr in users)
                {
                    usersApiDto.Add(_mapperHelper.MapUserToUserApiDto(usr));
                }

                return usersApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting All Users.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the user async by token.
        /// </summary>
        /// <returns>The user async by token.</returns>
        /// <param name="token">Token.</param>
        public async Task<UserApiDto> GetUserAsyncByToken(string token)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users.SingleOrDefaultAsync(x => x.Token == token);
                if (user != null)
                {
                    return _mapperHelper.MapUserToUserApiDto(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting User.");
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<UserApiDto> PatchUser(UserPatchApiDto userPatchApiDto)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users.SingleOrDefaultAsync(x => x.UserId == userPatchApiDto.UserId);
                var patchUser = _mapperHelper.MapUserPatchApiDtoToUser(user, userPatchApiDto);

                _rentTogetherDbContext.Users.Update(patchUser);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapUserToUserApiDto(patchUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while patching User.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Messages

        /// <summary>
        /// Gets the messages async by user identifier.
        /// </summary>
        /// <returns>The messages async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
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
                _logger.LogError(ex, ex.Message, "An error occurred while getting Messages.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Adds the message async.
        /// </summary>
        /// <returns>The message async.</returns>
        /// <param name="messageDto">Message dto.</param>
        public async Task<MessageApiDto> AddMessageAsync(MessageDto messageDto)
        {
            try
            {
                var conversation = await _rentTogetherDbContext.Conversations
                                                               .Include(x => x.Messages)
                                                               .Include(x => x.Participants)
                                                               .ThenInclude(xx => xx.User)
                                                               .SingleOrDefaultAsync(x => x.ConversationId == messageDto.ConversationId);
                var editor = await _rentTogetherDbContext.Users
                                                         .SingleOrDefaultAsync(x => x.UserId == messageDto.UserId);

                if (conversation == null || editor == null)
                    return null;

                var message = new Message()
                {
                    Conversation = conversation,
                    CreatedDate = DateTime.Now,
                    Editor = editor,
                    IsReport = 0,
                    MessageText = messageDto.MessageText
                };

                conversation.Messages.Add(message);
                _rentTogetherDbContext.Conversations.Update(conversation);

                var isSuccess = await _rentTogetherDbContext.SaveChangesAsync();

                if (isSuccess <= 0)
                    return null;

                var messageApiDto = _mapperHelper.MapMessageToMessageApiDto(await _rentTogetherDbContext.Messages
                                                                            .SingleOrDefaultAsync(x => x.Editor.UserId == messageDto.UserId
                                                                                                  && x.Conversation.ConversationId == messageDto.ConversationId && x.MessageId == message.MessageId));
                return messageApiDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Message.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all messages async from conversation by conversation identifier.
        /// </summary>
        /// <returns>The all messages async from conversation by conversation identifier.</returns>
        /// <param name="conversationId">Conversation identifier.</param>
        public async Task<List<MessageApiDto>> GetAllMessagesAsyncFromConversationByConversationId(int conversationId)
        {
            try
            {
                var messages = await _rentTogetherDbContext.Messages
                                                           .Include(x => x.Conversation)
                                                           .Include(x => x.Editor)
                                                           .Where(x => x.Conversation.ConversationId == conversationId).ToListAsync();
                if (messages == null)
                    return null;

                var messagesApiDto = new List<MessageApiDto>();
                foreach (var message in messages)
                {
                    messagesApiDto.Add(_mapperHelper.MapMessageToMessageApiDto(message));
                }

                return messagesApiDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Messages.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Conversations

        /// <summary>
        /// Adds the conversation async.
        /// </summary>
        /// <returns>The conversation async.</returns>
        /// <param name="conversationDto">Conversation dto.</param>
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
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Conversation.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the conversation async by user identifier.
        /// </summary>
        /// <returns>The conversation async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<ConversationApiDto>> GetConversationsAsyncByUserId(int userId)
        {
            try
            {
                var conversations = await _rentTogetherDbContext.Conversations
                                                                .Include(x => x.Messages)
                                                                .Include(x => x.Participants)
                                                                .ThenInclude(xx => xx.User)
                                                                .Where(x => x.Participants.Any(xx => xx.User.UserId == userId))
                                                                .ToListAsync();
                if (conversations == null)
                    return null;

                var conversationsApiDto = new List<ConversationApiDto>();

                foreach (var conversation in conversations)
                {
                    conversationsApiDto.Add(new ConversationApiDto()
                    {
                        ConversationId = conversation.ConversationId,
                        CreatedDate = conversation.CreatedDate,
                        Messages = new List<MessageApiDto>(),
                        Participants = new List<ParticipantApiDto>(),
                        Type = conversation.Type
                    });
                }

                foreach (var conversationApiDto in conversationsApiDto)
                {
                    var message = conversations.SelectMany(x => x.Messages).LastOrDefault(x => x.Conversation.ConversationId == conversationApiDto.ConversationId);
                    if (message != null)
                        conversationApiDto.Messages.Add(new MessageApiDto()
                        {
                            UserId = message.Editor.UserId,
                            CreatedDate = message.CreatedDate,
                            IsReport = message.IsReport,
                            MessageId = message.MessageId,
                            MessageText = message.MessageText
                        });


                    foreach (var participant in conversations.SelectMany(x => x.Participants).Where(x => x.Conversation.ConversationId == conversationApiDto.ConversationId))
                    {
                        conversationApiDto.Participants.Add(new ParticipantApiDto()
                        {
                            UserApiDto = new UserApiDto()
                            {
                                Email = participant.User.Email,
                                CreateDate = participant.User.CreateDate,
                                FirstName = participant.User.FirstName,
                                LastName = participant.User.LastName,
                                City = participant.User.City,
                                PostalCode = participant.User.PostalCode,
                                IsOwner = participant.User.IsOwner,
                                IsRoomer = participant.User.IsRoomer,
                                IsAdmin = participant.User.IsAdmin,
                                PhoneNumber = participant.User.PhoneNumber,
                                Token = participant.User.Token,
                                UserId = participant.User.UserId,
                                TokenExpirationDate = participant.User.TokenExpirationDate
                            },
                            ConversationId = participant.Conversation.ConversationId,
                            EndDate = participant?.EndDate,
                            StartDate = participant.StartDate,
                            ParticipantId = participant.ParticipantId
                        });
                    }
                }

                return conversationsApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Conversations.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all conversations async.
        /// </summary>
        /// <returns>The all conversations async.</returns>
        public async Task<List<ConversationApiDto>> GetAllConversationsAsync()
        {
            try
            {
                var conversations = await _rentTogetherDbContext.Conversations
                                                            .Include(x => x.Messages)
                                                            .Include(x => x.Participants)
                                                                .ThenInclude(xx => xx.User)
                                                            .ToListAsync();
                if (conversations == null)
                    return null;

                var conversationsApiDto = new List<ConversationApiDto>();

                foreach (var conversation in conversations)
                {
                    conversationsApiDto.Add(new ConversationApiDto()
                    {
                        ConversationId = conversation.ConversationId,
                        CreatedDate = conversation.CreatedDate,
                        Messages = new List<MessageApiDto>(),
                        Participants = new List<ParticipantApiDto>(),
                        Type = conversation.Type
                    });
                }

                foreach (var conversationApiDto in conversationsApiDto)
                {
                    foreach (var message in conversations.SelectMany(x => x.Messages).Where(x => x.Conversation.ConversationId == conversationApiDto.ConversationId))
                    {
                        conversationApiDto.Messages.Add(new MessageApiDto()
                        {
                            UserId = message.Editor.UserId,
                            CreatedDate = message.CreatedDate,
                            IsReport = message.IsReport,
                            MessageId = message.MessageId,
                            MessageText = message.MessageText
                        });
                    }

                    foreach (var participant in conversations.SelectMany(x => x.Participants).Where(x => x.Conversation.ConversationId == conversationApiDto.ConversationId))
                    {
                        conversationApiDto.Participants.Add(new ParticipantApiDto()
                        {
                            UserApiDto = new UserApiDto()
                            {
                                Email = participant.User.Email,
                                CreateDate = participant.User.CreateDate,
                                FirstName = participant.User.FirstName,
                                LastName = participant.User.LastName,
                                City = participant.User.City,
                                PostalCode = participant.User.PostalCode,
                                IsOwner = participant.User.IsOwner,
                                IsRoomer = participant.User.IsRoomer,
                                IsAdmin = participant.User.IsAdmin,
                                PhoneNumber = participant.User.PhoneNumber,
                                Token = participant.User.Token,
                                UserId = participant.User.UserId,
                                TokenExpirationDate = participant.User.TokenExpirationDate
                            },
                            ConversationId = participant.Conversation.ConversationId,
                            EndDate = participant?.EndDate,
                            StartDate = participant.StartDate,
                            ParticipantId = participant.ParticipantId
                        });
                    }
                }

                return conversationsApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Conversations.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Participant

        /// <summary>
        /// Gets the participant async by user identifier.
        /// </summary>
        /// <returns>The participant async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId)
        {

            try
            {
                var participant = await _rentTogetherDbContext.Participants
                                                              .Include(x => x.User)
                                                              .Include(x => x.Conversation)
                                                              .SingleOrDefaultAsync(x => x.User.UserId == userId);
                if (participant == null)
                    return null;

                var participantApiDto = _mapperHelper.MapParticipantToParticipantApiDto(participant);
                return participantApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Participant.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all participant async.
        /// </summary>
        /// <returns>The all participant async.</returns>
        public async Task<List<ParticipantApiDto>> GetAllParticipantAsync()
        {

            try
            {
                var participants = await _rentTogetherDbContext.Participants
                                                         .Include(x => x.Conversation)
                                                         .Include(x => x.User)
                                                         .ToListAsync();

                if (participants == null)
                    return null;

                var participantsApiDto = new List<ParticipantApiDto>();

                foreach (var participant in participants)
                {
                    participantsApiDto.Add(_mapperHelper.MapParticipantToParticipantApiDto(participant));
                }

                return participantsApiDto;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Participants.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Post the async participant to existing conversation.
        /// </summary>
        /// <returns>The async participant to existing conversation.</returns>
        /// <param name="participantDtos">Participant dto.</param>
        public async Task<List<ParticipantApiDto>> PostAsyncParticipantToExistingConversation(List<ParticipantDto> participantDtos)
        {
            try
            {
                var conversation = await _rentTogetherDbContext.Conversations
                                                               .Include(x => x.Participants)
                                                               .ThenInclude(xx => xx.User)
                                                               .Include(x => x.Messages)
                                                               .SingleOrDefaultAsync(x => x.ConversationId == participantDtos.FirstOrDefault().ConversationId);

                var users = await _rentTogetherDbContext.Users
                                                        .Where(x => participantDtos.Any(xx => xx.UserId == x.UserId))
                                                        .ToListAsync();

                if (conversation == null || (users == null && users.Count > 0))
                    return null;

                var listParticipants = new List<Participant>();
                foreach (var participantDto in participantDtos)
                {

                    //Si l'utilisateur n'est pas déjà présent dans la conversation
                    if (!conversation.Participants.Any(x => x.User.UserId == participantDto.UserId))
                    {

                        var user = users.SingleOrDefault(x => x.UserId == participantDto.UserId);

                        var participant = new Participant()
                        {
                            Conversation = conversation,
                            StartDate = DateTime.Now,
                            EndDate = null,
                            User = user
                        };

                        listParticipants.Add(participant);

                        conversation.Participants.Add(participant);

                        var updatingConversation = _rentTogetherDbContext.Conversations
                                                                         .Update(conversation);

                        var isSuccess = await _rentTogetherDbContext.SaveChangesAsync();
                        if (isSuccess <= 0)
                            return null;
                    }

                }

                var participantsApiDtos = new List<ParticipantApiDto>();
                foreach (var participant in listParticipants)
                {
                    participantsApiDtos.Add(_mapperHelper.MapParticipantToParticipantApiDto(participant));
                }


                return participantsApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Participants.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Media

        /// <summary>
        /// Posts the async user picture.
        /// </summary>
        /// <returns>The async user picture.</returns>
        /// <param name="fileDto">File dto.</param>
        public async Task<FileApiDto> PostAsyncUserPicture(FileDto fileDto)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.UserPicture)
                                                       .SingleOrDefaultAsync(x => x.UserId == fileDto.UserId);
                //If User has picture
                if (user.UserPicture != null)
                {
                    user.UserPicture.FileToBase64 = fileDto.FileToBase64;
                    _rentTogetherDbContext.UserPictures.Update(user.UserPicture);

                    await _rentTogetherDbContext.SaveChangesAsync();
                    return _mapperHelper.MapUserPictureToFileApiDto(await _rentTogetherDbContext.UserPictures
                                              .Include(x => x.User)
                                              .SingleOrDefaultAsync(x => x.User.UserId == fileDto.UserId));

                }

                var userPicture = new UserPicture()
                {
                    User = user,
                    FileToBase64 = fileDto.FileToBase64,
                };

                await _rentTogetherDbContext.UserPictures.AddAsync(userPicture);
                var isSuccess = await _rentTogetherDbContext.SaveChangesAsync();

                if (isSuccess <= 0)
                    return null;

                var fileApiDto = _mapperHelper.MapUserPictureToFileApiDto(await _rentTogetherDbContext.UserPictures
                                              .Include(x => x.User)
                                              .SingleOrDefaultAsync(x => x.User.UserId == fileDto.UserId));
                if (fileApiDto == null)
                    return null;

                return fileApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting User Picture.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async user picture by user identifier.
        /// </summary>
        /// <returns>The async user picture by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<FileApiDto> GetAsyncUserPictureByUserId(int userId)
        {
            try
            {
                var userPicture = await _rentTogetherDbContext.UserPictures
                                                        .Include(x => x.User)
                                                        .SingleOrDefaultAsync(x => x.User.UserId == userId);
                if (userPicture == null)
                    return null;

                var fileApiDto = _mapperHelper.MapUserPictureToFileApiDto(userPicture);
                return fileApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting User Picture.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region Personality

        /// <summary>
        /// Posts the async detail personality.
        /// </summary>
        /// <returns>The async detail personality.</returns>
        /// <param name="detailPersonalityDto">Detail personality dto.</param>
        public async Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto)
        {
            try
            {
                var personalityReferencial = new PersonalityReferencial()
                {
                    Description1 = detailPersonalityDto.Description1,
                    Description2 = detailPersonalityDto.Description2,
                    Description3 = detailPersonalityDto.Description3,
                    Description4 = detailPersonalityDto.Description4,
                    Description5 = detailPersonalityDto.Description5,
                    Name = detailPersonalityDto.Name,
                };

                await _rentTogetherDbContext.PersonalityReferencials.AddAsync(personalityReferencial);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapPersonalityReferencialToDetailPersonalityApiDto(personalityReferencial);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Personality Detail.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async all personality referencials.
        /// </summary>
        /// <returns>The async all personality referencials.</returns>
        public async Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials()
        {
            try
            {
                var personalityReferencials = await _rentTogetherDbContext.PersonalityReferencials.ToListAsync();
                if (personalityReferencials == null)
                    return null;

                var detailPersonalitiesApiDto = new List<DetailPersonalityApiDto>();
                foreach (var personalityReferencial in personalityReferencials)
                {
                    detailPersonalitiesApiDto.Add(_mapperHelper.MapPersonalityReferencialToDetailPersonalityApiDto(personalityReferencial));
                }

                return detailPersonalitiesApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Personality Details.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Posts the async personality values.
        /// </summary>
        /// <returns>The async personality values.</returns>
        /// <param name="personalityValueDtos">Personality value dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.User)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);

                var personalityValues = new List<PersonalityValue>();

                foreach (var personalityValue in personalityValueDtos)
                {
                    var personalityReferencial = await _rentTogetherDbContext.PersonalityReferencials
                                                                             .SingleOrDefaultAsync(x => x.PersonalityReferencialId == personalityValue.PersonalityReferencialId);
                    if (user.Personality == null)
                    {
                        user.Personality = new Personality
                        {
                            PersonalityValues = new List<PersonalityValue>()
                        };
                    }


                    if (personalityReferencial != null && !user.Personality.PersonalityValues.Select(x => x.PersonalityReferencial).Any(x => x.PersonalityReferencialId != personalityValue.PersonalityReferencialId))
                    {
                        personalityValues.Add(new PersonalityValue()
                        {
                            PersonalityReferencial = personalityReferencial,
                            Value = personalityValue.Value
                        });
                    }
                }

                if (personalityValues == null || personalityValues.Count <= 0)
                    return null;

                await _rentTogetherDbContext.PersonalityValues.AddRangeAsync(personalityValues);
                await _rentTogetherDbContext.SaveChangesAsync();

                if (user.Personality == null)
                {
                    //Set Personality User
                    var personality = new Personality()
                    {
                        PersonalityValues = personalityValues,
                        User = user
                    };

                    await _rentTogetherDbContext.Personnalities.AddAsync(personality);
                    await _rentTogetherDbContext.SaveChangesAsync();
                }
                else
                {
                    var personality = await _rentTogetherDbContext.Personnalities
                                                            .Include(x => x.PersonalityValues)
                                                            .Include(x => x.User)
                                                                  .SingleOrDefaultAsync(x => x.PersonalityId == user.Personality.PersonalityId);

                    personality.PersonalityValues.AddRange(personalityValues);

                    _rentTogetherDbContext.Personnalities.Update(personality);
                    await _rentTogetherDbContext.SaveChangesAsync();
                }

                //Return List
                var personalityValueApiDtos = new List<PersonalityValueApiDto>();

                foreach (var personalityValue in personalityValues)
                {
                    personalityValueApiDtos.Add(_mapperHelper.MapPersonalityValueToPersonalityValueApiDto(personalityValue));
                }

                return personalityValueApiDtos;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Personality Values.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the personality async by user identifier.
        /// </summary>
        /// <returns>The personality async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId)
        {
            try
            {
                var personality = await _rentTogetherDbContext.Personnalities
                                                              .Include(x => x.PersonalityValues)
                                                              .ThenInclude(xx => xx.PersonalityReferencial)
                                                              .Include(x => x.User)
                                                              .SingleOrDefaultAsync(x => x.User.UserId == userId);

                if (personality == null || personality.PersonalityValues == null)
                    return null;

                var personalityApiDto = new PersonalityApiDto
                {
                    PersonalityValueApiDtos = new List<PersonalityValueApiDto>()
                };

                foreach (var personalityValue in personality.PersonalityValues)
                {
                    personalityApiDto.PersonalityValueApiDtos.Add(_mapperHelper.MapPersonalityValueToPersonalityValueApiDto(personalityValue));
                }

                personalityApiDto.UserId = personality.User.UserId;
                personalityApiDto.PersonalityId = personality.PersonalityId;

                return personalityApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Personality.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Patchs the async personality values by user identifier.
        /// </summary>
        /// <returns>The async personality values by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="personalityValuePatchDtos">Personality value patch dtos.</param>
        public async Task<List<PersonalityValueApiDto>> PatchAsyncPersonalityValuesByUserId(int userId, List<PersonalityValuePatchDto> personalityValuePatchDtos)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                       .Include(x => x.Personality)
                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .ThenInclude(xxx => xxx.PersonalityReferencial)
                                       .Include(x => x.Personality)
                                       .ThenInclude(xx => xx.User)
                                       .SingleOrDefaultAsync(x => x.UserId == userId);

                if (user.Personality.PersonalityValues == null || user.Personality.PersonalityValues.Count <= 0)
                    return null;

                var personalityValueApiDtos = new List<PersonalityValueApiDto>();

                foreach (var personalityValue in user.Personality.PersonalityValues)
                {
                    var data = personalityValuePatchDtos.FirstOrDefault(x => x.PersonalityReferencialId == personalityValue.PersonalityReferencial.PersonalityReferencialId);
                    if (data != null)
                    {
                        if (data.Value.HasValue)
                            personalityValue.Value = data.Value.Value;
                    }


                    personalityValueApiDtos.Add(_mapperHelper.MapPersonalityValueToPersonalityValueApiDto(personalityValue));
                }

                _rentTogetherDbContext.PersonalityValues.UpdateRange(user.Personality.PersonalityValues);
                await _rentTogetherDbContext.SaveChangesAsync();

                return personalityValueApiDtos;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while patching Personality Values.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Match

        /// <summary>
        /// Posts the async match.
        /// </summary>
        /// <returns>The async match.</returns>
        /// <param name="matchDto">Match dto.</param>
        public async Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto)
        {
            try
            {
                var match = await _rentTogetherDbContext.Matches
                                                        .Include(x => x.User)
                                                        .Include(x => x.TargetUser)
                                                        .Include(x => x.MatchDetails)
                                                        .ThenInclude(xx => xx.PersonalityReferencial)
                                                        .SingleOrDefaultAsync(x => x.MatchId == matchDto.MatchId);
                if (match == null)
                    return null;

                //Set user status and targetUser status
                if (matchDto.StatusUser == 1)
                    match.StatusUser = 1;

                if (matchDto.StatusUser == 2)
                    match.StatusUser = 2;

                _rentTogetherDbContext.Update(match);
                await _rentTogetherDbContext.SaveChangesAsync();

                var matchTargetUser = await _rentTogetherDbContext.Matches
                                                        .Include(x => x.User)
                                                        .Include(x => x.TargetUser)
                                                        .Include(x => x.MatchDetails)
                                                        .ThenInclude(xx => xx.PersonalityReferencial)
                                                                .SingleOrDefaultAsync(x => x.TargetUser.UserId == match.User.UserId);
                if (matchTargetUser != null)
                {
                    matchTargetUser.StatusTargetUser = match.StatusUser;
                    _rentTogetherDbContext.Update(matchTargetUser);
                    await _rentTogetherDbContext.SaveChangesAsync();
                }

                return _mapperHelper.MapMatchToMatchApiDto(match);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Match.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async all matches.
        /// </summary>
        /// <returns>The async all matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncAllMatches(int userId)
        {
            try
            {

                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.Matches)
                                                           .ThenInclude(xx => xx.User)
                                                           .Include(x => x.Matches)
                                                           .ThenInclude(xx => xx.TargetUser)
                                                           .Include(x => x.Matches)
                                                           .ThenInclude(xx => xx.MatchDetails)
                                                           .Include(x => x.Personality)
                                                           .ThenInclude(xx => xx.PersonalityValues)
                                                           .Include(x => x.Personality)
                                                           .ThenInclude(xx => xx.PersonalityValues)
                                                           .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                           .Include(x => x.TargetLocations)
                                                           .SingleOrDefaultAsync(x => x.UserId == userId);

                var matchApiDtos = new List<MatchApiDto>();

                if (user.Matches == null)
                    return null;

                foreach (var userMatch in user.Matches)
                {
                    matchApiDtos.Add(_mapperHelper.MapMatchToMatchApiDto(userMatch));
                }

                return matchApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Matches.");
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// Gets the async list matches.
        /// </summary>
        /// <returns>The async list matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncListMatches(int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.User)
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.TargetUser)
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.MatchDetails)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                       .Include(x => x.TargetLocations)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);
                if (user == null)
                    return null;

                if (user.Matches == null)
                {
                    user.Matches = new List<Match>();
                }

                var matchApiDtos = new List<MatchApiDto>();

                //If user has already matches
                if (user.Matches.Any(x => x.StatusUser == 0))
                {
                    foreach (var userMatch in user.Matches.Where(x => x.StatusUser == 0 && x.StatusTargetUser != 2))
                    {
                        matchApiDtos.Add(_mapperHelper.MapMatchToMatchApiDto(userMatch));
                    }
                    return matchApiDtos;
                }

                var users = await _rentTogetherDbContext.Users
                                                        .Include(x => x.Matches)
                                                        .ThenInclude(xx => xx.TargetUser)
                                                        .Include(x => x.Matches)
                                                        .ThenInclude(xx => xx.User)
                                                        .Include(x => x.Matches)
                                                        .ThenInclude(xx => xx.MatchDetails)
                                                        .Include(x => x.Personality)
                                                        .ThenInclude(xx => xx.PersonalityValues)
                                                        .Include(x => x.Personality)
                                                        .ThenInclude(xx => xx.PersonalityValues)
                                                        .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                        .Include(x => x.TargetLocations)
                                                        .Where(x => x.UserId != userId &&
                                                               x.Personality != null &&
                                                               x.TargetLocations != null &&
                                                               user.TargetLocations.Select(xx => xx.City).Any(b => x.TargetLocations.Select(bb => bb.City).Contains(b)) &&
                                                               user.TargetLocations.Select(xx => xx.PostalCode).Any(b => x.TargetLocations.Select(bb => bb.PostalCode).Contains(b)))
                                                        .ToListAsync();

                if (users.Count == 0)
                    return null;

                var usersToClean = new List<User>();

                //Retrieve users to clean where matches status 2
                foreach (var userClean in users)
                {
                    foreach (var m in userClean.Matches)
                    {
                        if (m.TargetUser.UserId == userId)
                        {
                            if (m.StatusTargetUser == 2)
                                usersToClean.Add(userClean);
                        }
                    }
                    if (user.Matches.Any(x => x.TargetUser.UserId == userClean.UserId && x.StatusUser == 1))
                    {
                        usersToClean.Add(userClean);
                    }
                }

                //Remove users where matches status 2
                foreach (var userCln in usersToClean)
                {
                    users.Remove(userCln);
                }

                if (users.Count == 0)
                    return null;

                var matchDetails = new List<MatchDetail>();
                var newUserMatches = new List<Match>();

                var result = 0;
                var matchFail = false;

                foreach (var userTarget in users)
                {
                    //Check if user is valid for match
                    foreach (var userTargetPersonalityValue in userTarget.Personality.PersonalityValues)
                    {
                        var userValue = user.Personality.PersonalityValues.FirstOrDefault(x => x.PersonalityReferencial.PersonalityReferencialId == userTargetPersonalityValue.PersonalityReferencial.PersonalityReferencialId);

                        if (userValue != null)
                        {
                            var variation = 100 * (userTargetPersonalityValue.Value - userValue.Value) / userValue.Value;

                            //Check variation
                            //If < 0
                            if (variation <= 0)
                            {
                                result = 100 - (variation * -1);
                            }
                            //Else variation > 0
                            else
                            {
                                result = (variation - 100) * -1;
                            }

                            //If percent match < 50, match fail
                            if(result < 50)
                                matchFail = true;

                            result = 0;
                        }
                    }

                    if (matchFail == false)
                    {
                        //Get values foreach personalityValues
                        foreach (var userTargetPersonalityValue in userTarget.Personality.PersonalityValues)
                        {

                            var userValue = user.Personality.PersonalityValues.FirstOrDefault(x => x.PersonalityReferencial.PersonalityReferencialId == userTargetPersonalityValue.PersonalityReferencial.PersonalityReferencialId);
                            if (userValue != null)
                            {
                                var variation = 100 * (userTargetPersonalityValue.Value - userValue.Value) / userValue.Value;

                                //Check variation
                                //If < 0
                                if (variation <= 0)
                                {
                                    result = 100 - (variation * -1);
                                }
                                //Else variation > 0
                                else
                                {
                                    result = (variation - 100) * -1;
                                }

                                //Add match details
                                matchDetails.Add(new MatchDetail()
                                {
                                    PersonalityReferencial = userValue.PersonalityReferencial,
                                    Percent = result,
                                    Value = userTargetPersonalityValue.Value
                                });

                                result = 0;
                            }
                        }

                        //Set status if target user has user in match
                        var status = users.SingleOrDefault(x => x.UserId == userTarget.UserId)?.Matches.FirstOrDefault(x => x?.User.UserId == userTarget.UserId)?.StatusUser;
                        var statusToAdd = status ?? 0;

                        var total = 0;
                        matchDetails.ForEach(m =>
                        {
                            total = total + m.Percent;
                        });

                        //Set average match between users
                        var average = total / matchDetails.Count;

                        newUserMatches.Add(new Match()
                        {
                            User = user,
                            TargetUser = users.SingleOrDefault(x => x.UserId == userTarget.UserId),
                            StatusTargetUser = statusToAdd,
                            StatusUser = 0,
                            MatchDetails = matchDetails,
                            Average = average
                        });
                    }
                    matchDetails = new List<MatchDetail>();
                    matchFail = false;
                }

                if (newUserMatches == null)
                    return null;

                //Add matches to user
                await _rentTogetherDbContext.Matches.AddRangeAsync(newUserMatches);
                await _rentTogetherDbContext.SaveChangesAsync();

                //Map match
                foreach (var match in newUserMatches)
                {
                    matchApiDtos.Add(_mapperHelper.MapMatchToMatchApiDto(match));
                }
                var matchDetailApiDtos = new List<MatchDetailApiDto>();

                //Map match details
                foreach (var matchDetail in newUserMatches.SelectMany(x => x.MatchDetails))
                {
                    matchDetailApiDtos.Add(_mapperHelper.MapMapDetailToMatchDetailApiDto(matchDetail));
                }

                //Add match delails inside matches
                matchApiDtos.SelectMany(x => x.MatchDetailApiDtos).ToList().AddRange(matchDetailApiDtos);

                return matchApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Matches.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async validate matches.
        /// </summary>
        /// <returns>The async validate matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncValidateMatches(int userId)
        {
            try
            {
                var matches = await _rentTogetherDbContext.Matches
                                                          .Include(x => x.TargetUser)
                                                          .Include(x => x.User)
                                                          .Include(x => x.MatchDetails)
                                                          .ThenInclude(xx => xx.PersonalityReferencial)
                                                          .Where(x => x.User.UserId == userId && x.StatusUser == 1 && x.StatusTargetUser == 1)
                                                          .ToListAsync();
                if (matches.Count == 0)
                    return null;

                var matchesApiDto = new List<MatchApiDto>();
                foreach (var match in matches)
                {
                    matchesApiDto.Add(_mapperHelper.MapMatchToMatchApiDto(match));
                }

                return matchesApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Matches.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Patchs the async matches.
        /// </summary>
        /// <returns>The async matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> PatchAsyncMatches(int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.User)
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.TargetUser)
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.MatchDetails)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                       .Include(x => x.TargetLocations)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);
                if (user == null)
                    return null;

                var matchesTargetUser = await _rentTogetherDbContext.Matches
                                                  .Include(x => x.TargetUser)
                                                  .Include(x => x.User)
                                                  .Include(x => x.MatchDetails)
                                                  .ThenInclude(xx => xx.PersonalityReferencial)
                                                  .Where(x => x.TargetUser.UserId == userId && x.StatusTargetUser == 2)
                                                  .ToListAsync();

                //Reset TargetUser matches
                if (matchesTargetUser != null)
                {
                    foreach (var matchTargetUser in matchesTargetUser)
                    {
                        matchTargetUser.StatusTargetUser = 0;
                        _rentTogetherDbContext.Update(matchTargetUser);
                        await _rentTogetherDbContext.SaveChangesAsync();
                    }
                }

                var matchApiDtos = new List<MatchApiDto>();

                //Reset User matches
                foreach (var match in user.Matches.Where(x => x.StatusUser == 2))
                {
                    match.StatusUser = 0;
                    matchApiDtos.Add(_mapperHelper.MapMatchToMatchApiDto(match));
                }

                _rentTogetherDbContext.Matches.UpdateRange(user.Matches);
                await _rentTogetherDbContext.SaveChangesAsync();

                return matchApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while patching Matches.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the async match.
        /// </summary>
        /// <returns>The async match.</returns>
        /// <param name="matchId">Match identifier.</param>
        public async Task<bool> DeleteAsyncMatch(int matchId)
        {
            try
            {
                var match = await _rentTogetherDbContext.Matches
                                                        .Include(x => x.TargetUser)
                                                        .Include(x => x.User)
                                                        .Include(x => x.MatchDetails)
                                                        .ThenInclude(xx => xx.PersonalityReferencial)
                                                        .SingleOrDefaultAsync(x => x.MatchId == matchId);
                if (match == null)
                    return false;

                var targetUserId = match.TargetUser.UserId;
                var userId = match.User.UserId;

                var matchTargetUser = await _rentTogetherDbContext.Matches
                                                                  .Include(x => x.TargetUser)
                                                                  .Include(x => x.User)
                                                                  .Include(x => x.MatchDetails)
                                                                  .ThenInclude(xx => xx.PersonalityReferencial)
                                                                  .SingleOrDefaultAsync(x => x.User.UserId == targetUserId && x.TargetUser.UserId == userId);

                if (matchTargetUser != null)
                {
                    _rentTogetherDbContext.Matches.Remove(matchTargetUser);
                    await _rentTogetherDbContext.SaveChangesAsync();
                }

                _rentTogetherDbContext.Matches.Remove(match);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Matches.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region TargetLocation

        /// <summary>
        /// Gets the async target locations by user identifier.
        /// </summary>
        /// <returns>The async target locations by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> GetAsyncTargetLocationsByUserId(int userId)
        {
            try
            {
                var targetLocations = await _rentTogetherDbContext.TargetLocations
                                                                  .Include(x => x.User)
                                                                  .Where(x => x.User.UserId == userId)
                                                                  .ToListAsync();

                if (!targetLocations.Any())
                    return null;

                var targetLocationApiDtos = new List<TargetLocationApiDto>();
                foreach (var targetLocation in targetLocations)
                {
                    targetLocationApiDtos.Add(_mapperHelper.MapTargetLocationToTargetLocationApiDto(targetLocation));
                }

                return targetLocationApiDtos;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Target Locations.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Posts the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationDtos">Target location dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> PostAsyncTargetLocation(List<TargetLocationDto> targetLocationDtos, int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                   .SingleOrDefaultAsync(x => x.UserId == userId);
                if (user == null)
                    return null;

                var targetLocations = new List<TargetLocation>();
                foreach (var targetLocation in targetLocationDtos)
                {
                    targetLocations.Add(new TargetLocation
                    {
                        City = targetLocation.City,
                        PostalCode = targetLocation.PostalCode,
                        User = user,
                        City2 = targetLocation.City2
                    });
                }

                await _rentTogetherDbContext.TargetLocations
                                            .AddRangeAsync(targetLocations);

                await _rentTogetherDbContext.SaveChangesAsync();

                var targetLocationApiDtos = new List<TargetLocationApiDto>();
                foreach (var targetLocation in targetLocations)
                {
                    targetLocationApiDtos.Add(_mapperHelper.MapTargetLocationToTargetLocationApiDto(targetLocation));
                }

                return targetLocationApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Target Locations.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Patchs the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationPatchDtos">Target location patch dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> PatchAsyncTargetLocation(List<TargetLocationPatchDto> targetLocationPatchDtos, int userId)
        {
            try
            {
                var targetLocations = await _rentTogetherDbContext.TargetLocations
                                                                 .Include(x => x.User)
                                                                 .Where(x => x.User.UserId == userId)
                                                                 .ToListAsync();

                if (!targetLocations.Any())
                    return null;

                foreach (var targetLocation in targetLocations)
                {
                    var data = targetLocationPatchDtos.FirstOrDefault(x => x.TargetLocationId == targetLocation.TargetLocationId);
                    if (data != null)
                    {

                        if (!string.IsNullOrEmpty(data.City))
                            targetLocation.City = data.City;

                        if (!string.IsNullOrEmpty(data.PostalCode))
                            targetLocation.PostalCode = data.PostalCode;

                        if (!string.IsNullOrEmpty(data.City2))
                            targetLocation.City2 = data.City2;

                        _rentTogetherDbContext.TargetLocations.Update(targetLocation);

                        await _rentTogetherDbContext.SaveChangesAsync();
                    }
                }

                var targetLocationApiDtos = new List<TargetLocationApiDto>();
                foreach (var targetLocation in targetLocations)
                {
                    targetLocationApiDtos.Add(_mapperHelper.MapTargetLocationToTargetLocationApiDto(targetLocation));
                }

                return targetLocationApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while patching Target Locations.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationId">Target location identifier.</param>
        public async Task<bool> DeleteAsyncTargetLocation(int targetLocationId)
        {
            try
            {
                var targetLocation = await _rentTogetherDbContext.TargetLocations
                                                             .Include(x => x.User)
                                                                 .SingleOrDefaultAsync(x => x.TargetLocationId == targetLocationId);
                if (targetLocation == null)
                    return false;

                _rentTogetherDbContext.TargetLocations.Remove(targetLocation);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Target Location.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Building

        /// <summary>
        /// Posts the async building.
        /// </summary>
        /// <returns>The async building.</returns>
        /// <param name="buildingDto">Building dto.</param>
        public async Task<BuildingApiDto> PostAsyncBuilding(BuildingDto buildingDto)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == buildingDto.OwnerId);

                if (user == null)
                    return null;

                var newBuilding = new Building()
                {
                    Address = buildingDto.Address,
                    Address2 = buildingDto.Address2,
                    Area = buildingDto.Area,
                    City = buildingDto.City,
                    Description = buildingDto.Description,
                    NbPiece = buildingDto.NbPiece,
                    NbRenters = buildingDto.NbRenters,
                    NbRoom = buildingDto.NbRoom,
                    Owner = user,
                    Parking = buildingDto.Parking,
                    PostalCode = buildingDto.PostalCode,
                    Price = buildingDto.Price,
                    Title = buildingDto.Title,
                    Type = buildingDto.Type
                };

                _rentTogetherDbContext.Buildings.Add(newBuilding);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapBuildingToBuildingApiDto(newBuilding);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async buildings of owner.
        /// </summary>
        /// <returns>The async buildings of owner.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingApiDto>> GetAsyncBuildingsOfOwner(int userId)
        {
            try
            {

                var buildings = await _rentTogetherDbContext.Buildings
                                                            .Include(x => x.BuildingPictures)
                                                            .ThenInclude(xx => xx.Building)
                                                            .Include(x => x.BuildingUsers)
                                                            .ThenInclude(xx => xx.User)
                                                            .Include(x => x.BuildingMessages)
                                                            .ThenInclude(xx => xx.Writer)
                                                            .Include(x => x.Owner)
                                                            .Where(x => x.Owner.UserId == userId)
                                                            .ToListAsync();
                if (buildings.Count == 0)
                    return null;

                var buildingApiDtos = new List<BuildingApiDto>();
                foreach (var building in buildings)
                {
                    buildingApiDtos.Add(_mapperHelper.MapBuildingToBuildingApiDto(building));
                }

                return buildingApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Buildings.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async building of renter.
        /// </summary>
        /// <returns>The async building of renter.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<BuildingApiDto> GetAsyncBuildingOfRenter(int userId)
        {
            try
            {
                var buildingUser = await _rentTogetherDbContext.BuildingUsers
                                                               .Include(x => x.Building)
                                                               .Include(x => x.User)
                                                               .SingleOrDefaultAsync(x => x.UserId == userId);
                if (buildingUser == null)
                    return null;

                var building = await _rentTogetherDbContext.Buildings
                                                            .Include(x => x.BuildingPictures)
                                                            .ThenInclude(xx => xx.Building)
                                                            .Include(x => x.BuildingUsers)
                                                            .ThenInclude(xx => xx.User)
                                                            .Include(x => x.BuildingMessages)
                                                            .ThenInclude(xx => xx.Writer)
                                                            .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingUser.BuildingId);
                if (building == null)
                    return null;

                return _mapperHelper.MapBuildingToBuildingApiDto(building);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the async building for renter.
        /// </summary>
        /// <returns>The async building for renter.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingApiDto>> GetAsyncBuildingForRenter(int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.BuildingHistories)
                                                       .ThenInclude(xx => xx.Building)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);

                var targetLocations = await _rentTogetherDbContext.TargetLocations
                                                                  .Include(x => x.User)
                                                                  .Where(x => x.User.UserId == userId)
                                                                  .ToListAsync();
                if (!targetLocations.Any() || user == null)
                    return null;

                var cities = new HashSet<string>(targetLocations.Select(item => item.City));
                var postalCodes = new HashSet<string>(targetLocations.Select(item => item.PostalCode));

                var query = _rentTogetherDbContext.Buildings
                                                  .Include(x => x.BuildingPictures)
                                                  .ThenInclude(xx => xx.Building)
                                                  .Include(x => x.BuildingUsers)
                                                  .ThenInclude(xx => xx.User)
                                                  .Include(x => x.Owner)
                                                  .Where(x => x.IsRent == 0 &&
                                                         cities.Contains(x.City) &&
                                                         postalCodes.Contains(x.PostalCode) &&
                                                         !x.BuildingUsers.Any(xx => xx.UserId == userId));
                var buildings = query.ToList();

                if (!buildings.Any())
                    return null;

                var orderedBuilding = user.BuildingHistories.OrderBy(x => x.HasSeen == 0).ToList();

                var order = new List<int>(orderedBuilding.Select(x => x.Building.BuildingId));

                var listBuildingApiDtos = new List<BuildingApiDto>();

                foreach (var building in buildings.OrderBy(x => order.IndexOf(x.BuildingId)))
                {
                    listBuildingApiDtos.Add(_mapperHelper.MapBuildingToBuildingApiDto(building));
                }

                return listBuildingApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Buildings.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the building async.
        /// </summary>
        /// <returns>The building async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        public async Task<bool> DeleteBuildingAsync(int buildingId)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .ThenInclude(xx => xx.User)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingId);
                if (building == null)
                    return false;

                _rentTogetherDbContext.Buildings.Remove(building);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the building for owner identifier async.
        /// </summary>
        /// <returns>The building for owner identifier async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="ownerId">Owner identifier.</param>
        public async Task<bool> DeleteBuildingForOwnerIdAsync(int buildingId, int ownerId)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                            .ThenInclude(xx => xx.User)
                                                            .Include(x => x.BuildingMessages)
                                                            .ThenInclude(xx => xx.Writer)
                                                            .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingId && x.Owner.UserId == ownerId);
                if (building == null)
                    return false;

                _rentTogetherDbContext.Buildings.Remove(building);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates the building async.
        /// </summary>
        /// <returns>The building async.</returns>
        /// <param name="buildingUpdateDto">Building update dto.</param>
        public async Task<BuildingApiDto> UpdateBuildingAsync(BuildingUpdateDto buildingUpdateDto)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                            .ThenInclude(xx => xx.User)
                                                            .Include(x => x.BuildingMessages)
                                                            .ThenInclude(xx => xx.Writer)
                                                            .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingUpdateDto.BuildingId);
                if (building == null)
                    return null;

                var updateBuilding = _mapperHelper.MapBuildingUpdateDtoToBuilding(buildingUpdateDto, building);

                _rentTogetherDbContext.Buildings.Update(updateBuilding);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapBuildingToBuildingApiDto(updateBuilding);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while updating Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region BuildingMessage

        /// <summary>
        /// Posts the async building message.
        /// </summary>
        /// <returns>The async building message.</returns>
        /// <param name="buildingMessageDto">Building message dto.</param>
        public async Task<BuildingMessageApiDto> PostAsyncBuildingMessage(BuildingMessageDto buildingMessageDto)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                             .SingleOrDefaultAsync(x => x.BuildingId == buildingMessageDto.BuildingId);

                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == buildingMessageDto.UserId);
                if (building == null || user == null)
                    return null;

                var buildingMessage = new BuildingMessage()
                {
                    Building = building,
                    CreatedDate = DateTime.UtcNow,
                    IsReport = 0,
                    MessageText = buildingMessageDto.MessageText,
                    Writer = user
                };

                await _rentTogetherDbContext.BuildingMessages
                                            .AddAsync(buildingMessage);

                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapBuildingMessageToBuildingMessageApiDto(buildingMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Building Message.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the building messages async.
        /// </summary>
        /// <returns>The building messages async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        public async Task<List<BuildingMessageApiDto>> GetBuildingMessagesAsync(int buildingId)
        {
            try
            {

                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingId);
                if (building == null)
                    return null;

                var listMessageApiDto = new List<BuildingMessageApiDto>();

                foreach (var buildingMessage in building.BuildingMessages)
                {
                    listMessageApiDto.Add(_mapperHelper.MapBuildingMessageToBuildingMessageApiDto(buildingMessage));
                }

                return listMessageApiDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Building Message.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the building message async.
        /// </summary>
        /// <returns>The building message async.</returns>
        /// <param name="buildingMessageId">Building message identifier.</param>
        public async Task<bool> DeleteBuildingMessageAsync(int buildingMessageId)
        {
            try
            {
                var buildingMessage = await _rentTogetherDbContext.BuildingMessages
                                                                  .Include(x => x.Writer)
                                                                  .Include(x => x.Building)
                                                                  .SingleOrDefaultAsync(x => x.BuildingMessageId == buildingMessageId);
                if (buildingMessage == null)
                    return false;

                _rentTogetherDbContext.BuildingMessages.Remove(buildingMessage);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Building Message.");
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region BuildingUsers

        /// <summary>
        /// Posts the building user async.
        /// </summary>
        /// <returns>The building user async.</returns>
        /// <param name="buildingUserDto">Building user dto.</param>
        public async Task<BuildingUserApiDto> PostBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            try
            {
                var hasBuildingUser = await _rentTogetherDbContext.BuildingUsers
                                                               .Include(x => x.Building)
                                                               .Include(x => x.User)
                                                               .AnyAsync(x => x.UserId == buildingUserDto.UserId);
                //If user has already a rent
                if (hasBuildingUser == true)
                    return null;

                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingUserDto.BuildingId);

                if (building == null || (building.BuildingUsers.Count != 0 && building.NbMaxRenters == building.BuildingUsers.Count))
                    return null;

                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == buildingUserDto.UserId);
                if (user == null)
                    return null;

                var buildingUser = new BuildingUser()
                {
                    Building = building,
                    BuildingId = building.BuildingId,
                    User = user,
                    UserId = user.UserId
                };

                building.NbRenters += 1;
                building.BuildingUsers.Add(buildingUser);

                if (building.NbRenters == building.NbMaxRenters)
                    building.IsRent = 1;

                _rentTogetherDbContext.Buildings
                                      .Update(building);

                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapBuildingUserToBuildingUserApiDto(buildingUser);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Building User.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the building user async.
        /// </summary>
        /// <returns>The building user async.</returns>
        /// <param name="buildingUserDto">Building user dto.</param>
        public async Task<bool> DeleteBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingUserDto.BuildingId);

                if (building == null)
                    return false;

                var buildingUser = await _rentTogetherDbContext.BuildingUsers
                                                                .Include(x => x.Building)
                                                                .Include(x => x.User)
                                                                .SingleOrDefaultAsync(x => x.BuildingId == buildingUserDto.BuildingId &&
                                                                                     x.UserId == buildingUserDto.UserId);
                if (buildingUser == null)
                    return false;

                building.NbRenters -= 1;
                building.BuildingUsers.Remove(buildingUser);

                _rentTogetherDbContext.Buildings.Update(building);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Building User.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region BuildingPictures
        /// <summary>
        /// Posts the building picture async.
        /// </summary>
        /// <returns>The building picture async.</returns>
        /// <param name="buildingPictureDto">Building picture dto.</param>
        public async Task<BuildingPictureApiDto> PostBuildingPictureAsync(BuildingPictureDto buildingPictureDto)
        {
            try
            {
                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingPictureDto.BuildingId);

                if (building == null || building.BuildingPictures.Count == 6)
                    return null;

                var buildingPicture = new BuildingPicture()
                {
                    Building = building,
                    FileToBase64 = buildingPictureDto.FileToBase64
                };

                await _rentTogetherDbContext.BuildingPictures.AddAsync(buildingPicture);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapBuildingPictureToBuildingPictureApiDto(buildingPicture);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Building Picture.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the building picture informations async.
        /// </summary>
        /// <returns>The building picture informations async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        public async Task<List<BuildingPictureInformationApiDto>> GetBuildingPictureInformationsAsync(int buildingId)
        {
            try
            {
                var buildingPictures = await _rentTogetherDbContext.BuildingPictures
                                                                   .Include(x => x.Building)
                                                                   .Where(x => x.Building.BuildingId == buildingId)
                                                                   .ToListAsync();
                if (!buildingPictures.Any())
                    return null;

                var buildingPictureInformations = new List<BuildingPictureInformationApiDto>();
                foreach (var buildingPicture in buildingPictures)
                {
                    buildingPictureInformations.Add(_mapperHelper.MapBuildingPictureToBuildingPictureInformationApiDto(buildingPicture));
                }

                return buildingPictureInformations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Building Picture Informations.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the building pictures async.
        /// </summary>
        /// <returns>The building pictures async.</returns>
        /// <param name="buildingPictureId">Building picture identifier.</param>
        public async Task<BuildingPictureApiDto> GetBuildingPicturesAsync(int buildingPictureId)
        {
            try
            {
                var buildingPicture = await _rentTogetherDbContext.BuildingPictures
                                                                  .Include(x => x.Building)
                                                                  .SingleOrDefaultAsync(x => x.BuildingPictureId == buildingPictureId);
                if (buildingPicture == null)
                    return null;

                return _mapperHelper.MapBuildingPictureToBuildingPictureApiDto(buildingPicture);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Building Picture.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the building picture async.
        /// </summary>
        /// <returns>The building picture async.</returns>
        /// <param name="buildingPictureId">Building picture identifier.</param>
        public async Task<bool> DeleteBuildingPictureAsync(int buildingPictureId)
        {
            try
            {
                var buildingPicture = await _rentTogetherDbContext.BuildingPictures
                                                                  .Include(x => x.Building)
                                                                  .SingleOrDefaultAsync(x => x.BuildingPictureId == buildingPictureId);

                if (buildingPicture == null)
                    return false;

                _rentTogetherDbContext.BuildingPictures.Remove(buildingPicture);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Building Picture.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region SearchLocation

        /// <summary>
        /// Gets the search locations async.
        /// </summary>
        /// <returns>The search locations async.</returns>
        /// <param name="searchLocationDto">Search location dto.</param>
        public async Task<List<SearchLocationApiDto>> GetSearchLocationsAsync(SearchLocationDto searchLocationDto)
        {
            try
            {
                var searchLocationApiDto = new List<SearchLocationApiDto>();
                var postalCodes = new List<PostalCode>();

                searchLocationDto.Libelle = searchLocationDto.Libelle.ToUpper();

                if (!string.IsNullOrEmpty(searchLocationDto.Libelle) && !string.IsNullOrEmpty(searchLocationDto.PostalCodeId))
                    postalCodes = await _rentTogetherDbContext.PostalCodes
                                                              .Where(x => x.Libelle.Contains(searchLocationDto.Libelle) &&
                                                                     x.PostalCodeId.Contains(searchLocationDto.PostalCodeId))
                                                              .Take(25)
                                                              .ToListAsync();

                if (!string.IsNullOrEmpty(searchLocationDto.Libelle) && string.IsNullOrEmpty(searchLocationDto.PostalCodeId))
                    postalCodes = await _rentTogetherDbContext.PostalCodes
                                                              .Where(x => x.Libelle.Contains(searchLocationDto.Libelle))
                                                              .Take(25)
                                                              .ToListAsync();

                if (!string.IsNullOrEmpty(searchLocationDto.PostalCodeId) && string.IsNullOrEmpty(searchLocationDto.Libelle))
                    postalCodes = await _rentTogetherDbContext.PostalCodes
                                                              .Where(x => x.PostalCodeId.Contains(searchLocationDto.PostalCodeId))
                                                              .Take(25)
                                                              .ToListAsync();

                return _mapperHelper.MapPostalCodeToSearchLocation(postalCodes);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Search Locations.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region BuildingHistory

        /// <summary>
        /// Posts the building history async.
        /// </summary>
        /// <returns>The building history async.</returns>
        /// <param name="buildingHistoryDto">Building history dto.</param>
        public async Task<BuildingHistoryApiDto> PostBuildingHistoryAsync(BuildingHistoryDto buildingHistoryDto)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == buildingHistoryDto.UserId);
                var building = await _rentTogetherDbContext.Buildings
                                                           .SingleOrDefaultAsync(x => x.BuildingId == buildingHistoryDto.BuildingId);
                if (user == null || building == null)
                    return null;

                var buildingHistory = new BuildingHistory()
                {
                    Building = building,
                    HasSeen = 1,
                    User = user
                };

                await _rentTogetherDbContext.BuildingHistories
                                            .AddAsync(buildingHistory);

                await _rentTogetherDbContext.SaveChangesAsync();

                return new BuildingHistoryApiDto()
                {
                    BuildingHistoryId = buildingHistory.BuildingHistoryId,
                    BuildingId = buildingHistory.Building.BuildingId,
                    HasSeen = buildingHistory.HasSeen,
                    UserId = buildingHistory.User.UserId
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Building History.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the building histories by user identifier async.
        /// </summary>
        /// <returns>The building histories by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingHistoryApiDto>> GetBuildingHistoriesByUserIdAsync(int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.BuildingHistories)
                                                       .ThenInclude(xx => xx.Building)
                                                       .Include(x => x.BuildingHistories)
                                                       .ThenInclude(xx => xx.User)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);
                if (user == null)
                    return null;

                var buildingHistories = new List<BuildingHistoryApiDto>();

                foreach (var buildingHistory in user.BuildingHistories)
                {
                    buildingHistories.Add(_mapperHelper.MapBuildingHistoryToBuildingHistoryApiDto(buildingHistory));
                }

                return buildingHistories;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Building Histories.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region FavoriteBuilding

        /// <summary>
        /// Posts the favorite building async.
        /// </summary>
        /// <returns>The favorite building async.</returns>
        /// <param name="favoriteBuildingDto">Favorite building dto.</param>
        public async Task<FavoriteBuildingApiDto> PostFavoriteBuildingAsync(FavoriteBuildingDto favoriteBuildingDto)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == favoriteBuildingDto.UserId);

                var building = await _rentTogetherDbContext.Buildings
                                                           .Include(x => x.BuildingPictures)
                                                           .ThenInclude(xx => xx.Building)
                                                           .Include(x => x.BuildingUsers)
                                                           .Include(x => x.BuildingMessages)
                                                           .ThenInclude(xx => xx.Writer)
                                                           .Include(x => x.Owner)
                                                           .SingleOrDefaultAsync(x => x.BuildingId == favoriteBuildingDto.BuildingId);
                if (user == null || building == null)
                    return null;

                var existingFavoriteBuildings = await _rentTogetherDbContext.FavoriteBuildings
                                                                            .Include(x => x.User)
                                                                            .Include(x => x.TargetBuildings)
                                                                            .Where(x => x.User.UserId == favoriteBuildingDto.UserId)
                                                                            .ToListAsync();
                //Insert
                if (!existingFavoriteBuildings.Any())
                {
                    var favoriteBuilding = new FavoriteBuilding
                    {
                        TargetBuildings = new List<Building>()
                    };

                    favoriteBuilding.TargetBuildings.Add(building);
                    favoriteBuilding.User = user;

                    await _rentTogetherDbContext.FavoriteBuildings.AddAsync(favoriteBuilding);
                    await _rentTogetherDbContext.SaveChangesAsync();

                    return new FavoriteBuildingApiDto()
                    {
                        BuildingId = building.BuildingId,
                        FavoriteBuildingId = favoriteBuilding.FavoriteBuildingId,
                        UserId = user.UserId
                    };
                }
                //Update
                else
                {
                    var exist = existingFavoriteBuildings.SingleOrDefault(x => x.TargetBuildings.Any(xx => xx.BuildingId == favoriteBuildingDto.BuildingId) &&
                                                                               x.User.UserId == favoriteBuildingDto.UserId);
                    if (exist != null)
                        return null;

                    var favoriteBuilding = new FavoriteBuilding
                    {
                        TargetBuildings = new List<Building>()
                    };

                    favoriteBuilding.TargetBuildings.Add(building);
                    favoriteBuilding.User = user;

                    existingFavoriteBuildings.Add(favoriteBuilding);

                    _rentTogetherDbContext.FavoriteBuildings.UpdateRange(existingFavoriteBuildings);
                    await _rentTogetherDbContext.SaveChangesAsync();

                    return new FavoriteBuildingApiDto()
                    {
                        BuildingId = building.BuildingId,
                        FavoriteBuildingId = existingFavoriteBuildings.FirstOrDefault(x => x.User.UserId == user.UserId &&
                                                                                      x.TargetBuildings.Any(xs => xs.BuildingId == building.BuildingId)).FavoriteBuildingId,
                        UserId = user.UserId
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while inserting Favorite Building.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the favorite buildings by user identifier async.
        /// </summary>
        /// <returns>The favorite buildings by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingApiDto>> GetFavoriteBuildingsByUserIdAsync(int userId)
        {
            try
            {
                var favoriteBuildings = await _rentTogetherDbContext.FavoriteBuildings
                                                                    .Include(x => x.TargetBuildings)
                                                                    .Include(x => x.User)
                                                                    .Where(x => x.User.UserId == userId)
                                                                    .SelectMany(x => x.TargetBuildings)
                                                                    .ToListAsync();

                if (!favoriteBuildings.Any())
                    return null;

                var hashSetBuildingId = new HashSet<int>(favoriteBuildings.Select(x => x.BuildingId));

                var query = _rentTogetherDbContext.Buildings
                                                  .Include(x => x.BuildingPictures)
                                                  .ThenInclude(xx => xx.Building)
                                                  .Include(x => x.BuildingUsers)
                                                  .ThenInclude(xx => xx.User)
                                                  .Include(x => x.Owner)
                                                  .Where(x => hashSetBuildingId.Contains(x.BuildingId));
                var buildings = query.ToList();

                if (!buildings.Any())
                    return null;

                var buildingApiDtos = new List<BuildingApiDto>();
                foreach (var building in buildings)
                {
                    buildingApiDtos.Add(_mapperHelper.MapBuildingToBuildingApiDto(building));
                }

                return buildingApiDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while getting Favorite Buildings.");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the favorite building by building identifier async.
        /// </summary>
        /// <returns>The favorite building by building identifier async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<bool> DeleteFavoriteBuildingByBuildingIdAsync(int buildingId, int userId)
        {
            try
            {
                var favoriteBuilding = await _rentTogetherDbContext.FavoriteBuildings
                                                                   .Include(x => x.TargetBuildings)
                                                                   .Include(x => x.User)
                                                                   .SingleOrDefaultAsync(x => x.TargetBuildings.Select(xx => xx.BuildingId).Contains(buildingId) &&
                                                                                         x.User.UserId == userId);
                if (favoriteBuilding == null)
                    return false;

                _rentTogetherDbContext.FavoriteBuildings.Remove(favoriteBuilding);
                await _rentTogetherDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while deleting Favorite Building.");
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }

}
