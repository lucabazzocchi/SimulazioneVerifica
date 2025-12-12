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
            Events.Add(eventToAdd);
        }

        public Event MostExpensiveEvent()
        {
            if (Events == null)
                throw new ArgumentNullException(nameof(Events));

            Event mostExpensiveEvent = Events[0];
            for (int i = 1; i<Events.Count; i++)
            {
                if (Events[i].TicketCost > mostExpensiveEvent.TicketCost)
                {
                    mostExpensiveEvent = Events[i];
                }
            }
            return mostExpensiveEvent;
        }

        public EventTags MostFrequentEventTag()
        {
            if (Events == null)
                throw new ArgumentNullException(nameof(Events));
            EventTags mostFrequentTag = Events[0].EventTagList[0];
            List<EventTags> tags = new List<EventTags>();
            List<int> Counters = new List<int>();
            int maxCounter = 0;
            int maxIndex = 0;
            for (int i = 0; i < Events.Count; i++)
            {
                for(int j = 0; j < Events[i].EventTagList.Count; j++)
                {
                    EventTags currentTag = Events[i].EventTagList[j];
                    bool tagExists = false;
                    int foundIndex = -1;
                    for (int c = 0; c< tags.Count; c++)
                    {
                        if (currentTag == tags[c])
                        {
                            tagExists = true;
                            foundIndex = c;
                            break;
                        }
                    }
                    if (tagExists)
                    {
                        Counters[foundIndex]++;
                    }
                    else
                    {
                        tags.Add(currentTag);
                        Counters.Add(1);
                    }
                        
                    
                }
            }
            for(int i = 0; i<Counters.Count; i++)
            {
                if (Counters[i] > maxCounter)
                {
                    maxCounter = Counters[i];
                    maxIndex = i;
                }
                if(maxIndex != -1)
                mostFrequentTag = tags[maxIndex];
            }
            return mostFrequentTag;
        }

        public List<Event> CompatibleEvents(List<EventTags> tags)
        {
            if (Events == null)
                throw new ArgumentNullException(nameof(Events));

            List<Event> compatibleEvents = new List<Event>();

            for(int i = 0; i<Events.Count; i++)
            {
                Event currentEvent = Events[i];

                if (currentEvent.ContainTags(tags))
                {
                    compatibleEvents.Add(currentEvent);
                }
            }
            return compatibleEvents;
        }

        public int[,] GenerateEventTagsMatrix()
        {
            List<EventTags> uniqueEventTags = new List<EventTags>();
            for(int i = 0; i<Events.Count; i++)
            {
                for(int j = 0; j < Events[i].EventTagList.Count; j++)
                {
                    EventTags currentTag = Events[i].EventTagList[j];
                    bool tagsFound = false;
                    for(int c = 0; c<uniqueEventTags.Count; c++)
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
            int numRighe = uniqueEventTags.Count;
            int numColonne = Events.Count;
            int[,] EventTagsMatrix = new int[numRighe, numColonne];
            for(int c = 0; c<Events.Count; c++)
            {
                for(int r = 0; r<uniqueEventTags.Count; r++)
                {
                    if (Events[c].ContainsTag(uniqueEventTags[r]))
                    {
                        EventTagsMatrix[r, c] = 1;
                    }
                    else
                        EventTagsMatrix[r, c] = 0;
                }
            }
            return EventTagsMatrix;
        }

        public Event[][] GenerateJaggedEventsTag()
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

            List<List<Event>> temporaryLists = new List<List<Event>>();

            for (int r = 0; r < uniqueEventTags.Count; r++)
            {
                temporaryLists.Add(new List<Event>());
            }
            for (int i = 0; i < Events.Count; i++)
            {
                Event currentEvent = Events[i];

                for (int t = 0; t < uniqueEventTags.Count; t++)
                {
                    if (currentEvent.ContainsTag(uniqueEventTags[t]))
                    {
                        temporaryLists[t].Add(currentEvent);
                    }
                }
            }
           
            Event[][] jaggedArrayEvents = new Event[uniqueEventTags.Count][];
            for (int i = 0; i < temporaryLists.Count; i++)
            {
                // Conversione della List<Event> in Event[] (Array interno)
                jaggedArrayEvents[i] = temporaryLists[i].ToArray();
            }
            return jaggedArrayEvents;
        }
    }
}
