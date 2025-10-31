// ====================== DTOs ======================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _3ecexamen.DTOs
{
    public class CreateSpeakerDto
    {
        [Required] public string FullName { get; set; } = default!;
        [Required] public string TopicArea { get; set; } = default!;
    }
    
    public class TalkDto
    {
        public int SpeakerId { get; set; }
        public string Speaker { get; set; } = default!;
        public int RoomId { get; set; }
        public string Room { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class CreateTalkDto
    {
        [Required] public int SpeakerId { get; set; }
        [Required] public int RoomId { get; set; }
        [Required] public DateTime StartTime { get; set; }
        [Required] public DateTime EndTime { get; set; }
    }

    public class SpeakerScheduleDto
    {
        public string Speaker { get; set; } = default!;
        public List<TalkDto> Slots { get; set; } = new();
    }
    public class CreateConferenceDto
    {
        [Required] public string Title { get; set; } = default!;
        [Required] public string City { get; set; } = default!;
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }

        // Optional: create rooms along with conference
        public List<CreateRoomDto> Rooms { get; set; } = new();
    }

    public class CreateRoomDto
    {
        [Required] public string Name { get; set; } = default!;
    }

    public class ConferenceAgendaDto
    {
        public string Conference { get; set; } = default!;
        public string City { get; set; } = default!;
        public List<RoomScheduleDto> Rooms { get; set; } = new();
    }

    public class RoomScheduleDto
    {
        public string Room { get; set; } = default!;
        public List<TalkDto> Talks { get; set; } = new();
    }
}
