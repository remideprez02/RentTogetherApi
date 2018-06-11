using System;
namespace RentTogether.Entities.Dto.Participant
{
    public class ParticipantApiDto
    {
		public int ParticipantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}
