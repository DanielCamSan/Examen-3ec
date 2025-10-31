using System;

namespace _3ecexamen.Entities
{
    // Join entity for N:M between Speaker and Room, with payload (date/time).
    public class Talk
    {
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; } = default!;

        public int RoomId { get; set; }
        public Room Room { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; 

    }
}
