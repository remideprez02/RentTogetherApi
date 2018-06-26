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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                                Password = participant.User.Password,
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
                throw new Exception(ex.Message);
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
                                Password = participant.User.Password,
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Post the async participant to existing conversation.
        /// </summary>
        /// <returns>The async participant to existing conversation.</returns>
        /// <param name="participantDto">Participant dto.</param>
        public async Task<ParticipantApiDto> PostAsyncParticipantToExistingConversation(ParticipantDto participantDto)
        {
            try
            {
                var conversation = await _rentTogetherDbContext.Conversations
                                                               .Include(x => x.Participants)
                                                               .ThenInclude(xx => xx.User)
                                                               .Include(x => x.Messages)
                                                               .SingleOrDefaultAsync(x => x.ConversationId == participantDto.ConversationId);

                var user = await _rentTogetherDbContext.Users
                                                       .SingleOrDefaultAsync(x => x.UserId == participantDto.UserId);

                if (conversation == null || user == null)
                    return null;

                //Si l'utilisateur est déjà présent dans la conversation
                if (conversation.Participants.Any(x => x.User.UserId == participantDto.UserId))
                    return null;

                var participant = new Participant()
                {
                    Conversation = conversation,
                    StartDate = DateTime.Now,
                    EndDate = null,
                    User = user
                };

                conversation.Participants.Add(participant);

                var updatingConversation = _rentTogetherDbContext.Conversations
                                                                 .Update(conversation);

                var isSuccess = await _rentTogetherDbContext.SaveChangesAsync();
                if (isSuccess <= 0)
                    return null;

                var participantApiDto = _mapperHelper.MapParticipantToParticipantApiDto(await _rentTogetherDbContext.Participants
                                                                                        .Include(x => x.User)
                                                                                        .Include(x => x.Conversation)
                                                                                        .FirstOrDefaultAsync(x => x.Conversation.ConversationId == participantDto.ConversationId
                                                                                                             && x.User.UserId == participantDto.UserId));
                if (participantApiDto == null)
                    return null;

                return participantApiDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Media
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
                throw new Exception(ex.Message);
            }
        }

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
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Personality

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
                throw new Exception(ex.Message);
            }
        }

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
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                       .Include(x => x.Personality)
                                       .ThenInclude(xx => xx.PersonalityValues)
                                       .Include(x => x.Personality)
                                       .ThenInclude(xx => xx.User)
                                       .SingleOrDefaultAsync(x => x.UserId == userId);

                var personalityValues = new List<PersonalityValue>();

                foreach (var personalityValue in personalityValueDtos)
                {
                    var personalityReferencial = await _rentTogetherDbContext.PersonalityReferencials
                                                                       .SingleOrDefaultAsync(x => x.PersonalityReferencialId == personalityValue.PersonalityReferencialId);

                    if (personalityReferencial != null && !user.Personality.PersonalityValues.Any(x => x.PersonalityReferencial == personalityReferencial))
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
                throw new Exception(ex.Message);
            }
        }

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
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Match
        public async Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto)
        {
            try
            {
                var match = await _rentTogetherDbContext.Matches
                                                        .Include(x => x.User)
                                                        .Include(x => x.TargetUser)
                                                        .SingleOrDefaultAsync(x => x.MatchId == matchDto.MatchId);


                if (match.DateStatusTargetUser == null && matchDto.StatusTargetUser == 1)
                {
                    match.DateStatusTargetUser = new DateTime();
                    match.DateStatusTargetUser = DateTime.UtcNow;

                    match.StatusTargetUser = 1;
                }
                else
                {
                    match.DateStatusTargetUser = new DateTime();
                    match.DateStatusTargetUser = DateTime.UtcNow;

                    match.StatusTargetUser = 0;
                }

                if (match.DateStatusUser == null && matchDto.StatusUser == 1)
                {
                    match.DateStatusUser = new DateTime();
                    match.DateStatusUser = DateTime.UtcNow;

                    match.StatusUser = 1;
                }
                else
                {
                    match.DateStatusTargetUser = new DateTime();
                    match.DateStatusTargetUser = DateTime.UtcNow;

                    match.StatusUser = 0;
                }

                _rentTogetherDbContext.Update(match);
                await _rentTogetherDbContext.SaveChangesAsync();

                return _mapperHelper.MapMatchToMatchApiDto(match);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MatchApiDto>> GetAsyncListMatches(int userId)
        {
            try
            {
                var user = await _rentTogetherDbContext.Users
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.User)
                                                       .Include(x => x.Matches)
                                                       .ThenInclude(xx => xx.TargetUser)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .Include(x => x.Personality)
                                                       .ThenInclude(xx => xx.PersonalityValues)
                                                       .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                       .SingleOrDefaultAsync(x => x.UserId == userId);

                var users = await _rentTogetherDbContext.Users
                                                        .Include(x => x.Matches)
                                                        .ThenInclude(xx => xx.TargetUser)
                                                        .Include(x => x.Matches)
                                                        .ThenInclude(xx => xx.User)
                                                        .Include(x => x.Personality)
                                                        .ThenInclude(xx => xx.PersonalityValues)
                                                        .Include(x => x.Personality)
                                                        .ThenInclude(xx => xx.PersonalityValues)
                                                        .ThenInclude(xxx => xxx.PersonalityReferencial)
                                                        .Where(x => x.UserId != userId && x.Matches == null || x.Matches.Select(xx => xx.User) != user)
                                                        .ToListAsync();

                var matchApiDtos = new List<MatchApiDto>();
                var matchProcessDto = new List<MatchProcessDto>();

                if (user.Matches == null)
                {
                    user.Matches = new List<Match>();
                }

                var result = 0;
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

                            matchProcessDto.Add(new MatchProcessDto()
                            {
                                MatchPercent = result,
                                UserTargetId = userTarget.UserId
                            });
                            result = 0;
                        }
                    }
                }

                var count = 0;
                foreach (var match in matchProcessDto.OrderByDescending(x => x.MatchPercent))
                {
                    user.Matches.Add(new Match()
                    {
                        User = user,
                        TargetUser = users.SingleOrDefault(x => x.UserId == match.UserTargetId)
                    });

                    count += 1;
                    if (count == 10)
                        break;
                }

                _rentTogetherDbContext.Users.Update(user);
            await _rentTogetherDbContext.SaveChangesAsync();

            foreach (var match in user.Matches.OrderByDescending(x => x.MatchId))
            {
                matchApiDtos.Add(_mapperHelper.MapMatchToMatchApiDto(match));
            }

            return matchApiDtos;
        }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
    }
}
        #endregion

    }

}
