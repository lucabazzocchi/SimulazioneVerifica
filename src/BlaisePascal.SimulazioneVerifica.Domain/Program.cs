using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BlaisePascal.SimulazioneVerifica.Domain
{
    public class Program
    {
        public void Main(string[] args)
        {
            
            Local local = new Local("cocoricò");
            var stringList = new List<String>();
            List<EventTags> tags = new List<EventTags>();
            tags.Add(EventTags.Music);
            tags.Add(EventTags.Culture);

            local.AddEvent(new Event("music90", new DateOnly(2025, 12, 16), tags, 80.0));
        }
    }
}
