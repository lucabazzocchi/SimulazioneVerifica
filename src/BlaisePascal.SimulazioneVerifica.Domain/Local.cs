using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SimulazioneVerifica.Domain
{
    public class Local
    {
        public string Name { get; set; }
        public List<Event> Events { get; set; }
        public Local(string name)
        {
            Name = name;
            Events = new List<Event>();
        }

        public void AddEvent(Event eventToAdd)
        {
            Events.Add(eventToAdd);
        }

        //public Event MostExpensiveEvent()
        //{
        //    if (Events == null)
        //        throw new ArgumentNullException(nameof(Events));

        //    Event mostExpensiveEvent = Events[0];
        //    for (int i = 1; i<Events.Count; i++)
        //    {
        //        if (Events[i].TicketCost > mostExpensiveEvent.TicketCost)
        //        {
        //            mostExpensiveEvent = Events[i];
        //        }
        //    }
        //    return mostExpensiveEvent;
        //}



        //public Event MostExpensiveEvent()
        //{
        //    if (Events.Count == 0)
        //        throw new InvalidOperationException(nameof(Events));
        //    Event maxEventCost = Events[0];
        //    foreach (var evento in Events)
        //    {
        //        if (evento.TicketCost > maxEventCost.TicketCost)
        //            maxEventCost = evento;
        //    }
        //    return maxEventCost;
        //}

        public List<Event> MostExpensiveEvent()
        {
            if (Events.Count == 0)
                throw new InvalidOperationException(nameof(Events));
            double maxCost = 0.0;
            foreach (var evento in Events)
            {
                if (evento.TicketCost > maxCost)
                    maxCost = evento.TicketCost;
            }
            List<Event> events = new List<Event>();
            foreach(Event e  in Events)
            {
                if(e.TicketCost == maxCost)
                {
                    events.Add(e);
                }
            }
            return events;
        }

        //public EventTags MostFrequentEventTag()
        //{
        //    if (Events == null)
        //        throw new ArgumentNullException(nameof(Events));
        //    EventTags mostFrequentTag = Events[0].EventTagList[0];
        //    List<EventTags> tags = new List<EventTags>();
        //    List<int> Counters = new List<int>();
        //    int maxCounter = 0;
        //    int maxIndex = 0;
        //    for (int i = 0; i < Events.Count; i++)
        //    {
        //        for(int j = 0; j < Events[i].EventTagList.Count; j++)
        //        {
        //            EventTags currentTag = Events[i].EventTagList[j];
        //            bool tagExists = false;
        //            int foundIndex = -1;
        //            for (int c = 0; c< tags.Count; c++)
        //            {
        //                if (currentTag == tags[c])
        //                {
        //                    tagExists = true;
        //                    foundIndex = c;
        //                    break;
        //                }
        //            }
        //            if (tagExists)
        //            {
        //                Counters[foundIndex]++;
        //            }
        //            else
        //            {
        //                tags.Add(currentTag);
        //                Counters.Add(1);
        //            }
        //        }
        //    }
        //    for(int i = 0; i<Counters.Count; i++)
        //    {
        //        if (Counters[i] > maxCounter)
        //        {
        //            maxCounter = Counters[i];
        //            maxIndex = i;
        //        }
        //        if(maxIndex != -1)
        //        mostFrequentTag = tags[maxIndex];
        //    }
        //    return mostFrequentTag;
        //}
        public List<EventTags> CreateUniqueTags()
        {
            List<EventTags> uniqueEventTags = new List<EventTags>();
            for (int i = 0; i < Events.Count; i++)
            {
                for (int j = 0; j < Events[i].EventTagList.Count; j++)
                {
                    EventTags currentTag = Events[i].EventTagList[j];
                    bool tagsFound = false;
                    for (int c = 0; c < uniqueEventTags.Count; c++)
                    {
                        if (currentTag == uniqueEventTags[c])
                        {
                            tagsFound = true;
                            break;
                        }
                    }
                    if (!tagsFound)
                        uniqueEventTags.Add(currentTag);
                }
            }
            return uniqueEventTags;
        }

        public EventTags MostFrequentEventTag()
        {
            if (Events.Count == 0)
                throw new InvalidOperationException(nameof(Events));
            EventTags mostFrequentTag = Events[0].EventTagList[0];
            HashSet<EventTags> tags = new HashSet<EventTags>();
            List<int> Counters = new List<int>();
            //foreach(var e in Events)
            //{
            //    foreach(var t in e.EventTagList)
            //    {
            //        bool tagExist = false;
            //        int foundIndex = -1;
            //        for(int i = -1; i<tags.Count; i++)
            //        {
            //            if (t == tags[i])
            //            {
            //                tagExist = true;
            //                foundIndex = i;
            //                break;
            //            }
            //        }
            //        if (tagExist)
            //        {
            //            Counters[foundIndex]++;
            //        }
            //        else
            //        {
            //            tags.Add(t);
            //            Counters.Add(1);
            //        }
            //    }
            foreach (var e in Events)
            {
                foreach (var tag in e.EventTagList)
                {
                    tags.Add(tag);
                }
            }
            int maxCounter = 0;
            int maxIndex = 0;
            
            for (int i = 0; i<Counters.Count; i++)
            {
               if(Counters[i] > maxCounter)
               {
                    maxCounter = Counters[i];
                    maxIndex = i;
               }
                    mostFrequentTag = tags[maxIndex];
               }
            }
            return mostFrequentTag;
        }

        public List<Event> CompatibleEvents(List<EventTags> tags)
        {
            if (Events.Count == 0)
                throw new InvalidOperationException(nameof(Events));

            List<Event> compatibleEvents = new List<Event>();

            foreach(var e in Events)
            {
                if (e.ContainTags(tags))
                    compatibleEvents.Add(e);
            }
            
            return compatibleEvents;
        }

        //public int[,] GenerateEventTagsMatrix()
        //{
        //    List<EventTags> uniqueEventTags = new List<EventTags>();
        //    for(int i = 0; i<Events.Count; i++)
        //    {
        //        for(int j = 0; j < Events[i].EventTagList.Count; j++)
        //        {
        //            EventTags currentTag = Events[i].EventTagList[j];
        //            bool tagsFound = false;
        //            for(int c = 0; c<uniqueEventTags.Count; c++)
        //            {
        //                if (currentTag == uniqueEventTags[c])
        //                {
        //                    tagsFound = true;
        //                    break;
        //                }
        //            }
        //            if (!tagsFound)
        //                uniqueEventTags.Add(currentTag);
        //        }
        //    }
        //    int numRighe = uniqueEventTags.Count;
        //    int numColonne = Events.Count;
        //    int[,] EventTagsMatrix = new int[numRighe, numColonne];
        //    for(int c = 0; c<Events.Count; c++)
        //    {
        //        for(int r = 0; r<uniqueEventTags.Count; r++)
        //        {
        //            if (Events[c].ContainsTag(uniqueEventTags[r]))
        //            {
        //                EventTagsMatrix[r, c] = 1;
        //            }
        //            else
        //                EventTagsMatrix[r, c] = 0;
        //        }
        //    }
        //    return EventTagsMatrix;
        //}


        


        public int[,] GenerateEventTagsMatrix()
        {
            HashSet<EventTags> uniqueTags = new HashSet<EventTags>();
            foreach(var e in Events)
            {
                foreach(var tag in e.EventTagList)
                {
                    uniqueTags.Add(tag);
                }
            }
            List<EventTags> tags = uniqueTags.ToList();
            int numRows = tags.Count;
            int numCol = Events.Count;
            int[,] matrix = new int[numRows, numCol];
            for(int c = 0; c<Events.Count; c++)
            {
                for(int r = 0; r<tags.Count; r++)
                {
                    if (Events[c].ContainsTag(tags[r]))
                    {
                        matrix[r, c] = 1;
                    }
                }
            }
            return matrix;
        }


        public Event[][] GenerateJaggedEventsTag()
        {
            //List<EventTags> uniqueEventTags = new List<EventTags>();
            //for (int i = 0; i < Events.Count; i++)
            //{
            //    for (int j = 0; j < Events[i].EventTagList.Count; j++)
            //    {
            //        EventTags currentTag = Events[i].EventTagList[j];
            //        bool tagsFound = false;
            //        for (int c = 0; c < uniqueEventTags.Count; c++)
            //        {
            //            if (currentTag == uniqueEventTags[c])
            //            {
            //                tagsFound = true;
            //                break;
            //            }
            //        }
            //        if (!tagsFound)
            //            uniqueEventTags.Add(currentTag);
            //    }
            //}
            HashSet<EventTags> uniqueTagss = new HashSet<EventTags>();
            foreach (var e in Events)
            {
                foreach (var tag in e.EventTagList)
                {
                    uniqueTagss.Add(tag);
                }
            }
            List<EventTags> tags = uniqueTagss.ToList();
            List<List<Event>> temporaryLists = new List<List<Event>>();
            foreach(var t in tags)
            {
                temporaryLists.Add(new List<Event>());
            }
            for (int i = 0; i < Events.Count; i++)
            {
                Event currentEvent = Events[i];

                for (int t = 0; t < tags.Count; t++)
                {
                    if (currentEvent.ContainsTag(tags[t]))
                    {
                        temporaryLists[t].Add(currentEvent);
                    }
                }
            }
            Event[][] jaggedArrayEvents = new Event[tags.Count][];
            for (int i = 0; i < temporaryLists.Count; i++)
            {
                // Conversione della List<Event> in Event[] (Array interno)
                jaggedArrayEvents[i] = temporaryLists[i].ToArray();
            }
            return jaggedArrayEvents;
        }

        /// metodo riceve un array di interi di dimensioni 9, verificare che al suo interno siano contenuti tutti i numeri da 1 a 9
    }
}
