using System.Collections.Generic;

namespace _3ecexamen.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int ConferenceId { get; set; }
        public Conference Conferences { get; set; } = default!;

        // N:M Speakers via Talk
        public ICollection<Talk> Talks { get; set; } = new List<Talk>();
    }
}
