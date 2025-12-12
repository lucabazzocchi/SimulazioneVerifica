using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SimulazioneVerifica.Domain
{
    public class Local
    {
        public string Name { get; set; }
        public List<Event> Events { get; set; }
        public Local(string name, List<Event> events)
        {
            Name = name;
            Events = events;
        }
        public void AddEvent(string nome, DateOnly giorno, List<EventTags> tags, double cost)
        {
            Event eventToAdd = new Event(nome, giorno, tags, cost);
        }
    }
}
