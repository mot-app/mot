using System;
using System.Collections.Generic;
using System.Text;

namespace mot.Models
{
    public class Meetup
    {
        public int Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
        public DateTime Time { get; set; }
        public bool Active { get; set; }
    }
}
