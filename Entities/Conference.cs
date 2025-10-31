using System;
using System.Collections.Generic;

namespace _3ecexamen.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //TODO
        // 1:N Conference -> Rooms

        public ICollection<Room> Rooms { get; set; } = new List<Room>();

    }
}
