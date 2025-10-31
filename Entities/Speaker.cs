using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3ecexamen.Entities
{
    public class Speaker
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string TopicArea { get; set; } = default!;

        // N:M via Talk
        public ICollection<Talk> Talks { get; set; } = new List<Talk>();
    }
}