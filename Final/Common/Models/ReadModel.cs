using System.Collections.Generic;

namespace Common.Models
{

    public class CommunityModel
    {
        public List<EventModel> Events { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Link { get; internal set; }
    }
}
