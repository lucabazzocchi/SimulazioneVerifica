using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            foreach (Event e in Events)
            {
                if (e.TicketCost == maxCost)
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
            foreach (var e in Events)
            {
                foreach (var t in e.EventTagList)
                {
                    bool tagsFound = false;
                    for (int c = 0; c < uniqueEventTags.Count; c++)
                    {
                        if (t == uniqueEventTags[c])
                        {
                            tagsFound = true;
                            break;
                        }
                    }
                    if (!tagsFound)
                        uniqueEventTags.Add(t);
                }
            }
            return uniqueEventTags;
        }

        public EventTags MostFrequentEventTag()
        {
            if (Events.Count == 0)
                throw new InvalidOperationException(nameof(Events));

            List<EventTags> uniqueTags = CreateUniqueTags();
            int[] Counters = new int[uniqueTags.Count];

            int maxCounter = 0;
            int maxIndex = 0;

            foreach (var e in Events)
            {
                foreach (var t in e.EventTagList)
                {
                    int index = uniqueTags.IndexOf(t);
                    Counters[index]++;
                }

            }
            foreach (var e in uniqueTags)
            {
                int indx = uniqueTags.IndexOf(e);
                if (Counters[indx] > maxCounter)
                {
                    maxCounter = Counters[indx];
                    maxIndex = indx;
                }
            }
            return uniqueTags[maxIndex];
        }



        public List<Event> CompatibleEvents(List<EventTags> tags)
        {
            if (Events.Count == 0)
                throw new InvalidOperationException(nameof(Events));
            List<Event> compatibleEvents = new List<Event>();
            foreach (var e in Events)
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
            List<EventTags> uniqueTags = CreateUniqueTags();
            int numRows = uniqueTags.Count;
            int numCol = Events.Count;
            int[,] matrix = new int[numRows, numCol];
            for (int c = 0; c < Events.Count; c++)
            {
                for (int r = 0; r < uniqueTags.Count; r++)
                {
                    if (Events[c].ContainsTag(uniqueTags[r]))
                    {
                        matrix[r, c] = 1;
                    }
                }
            }
            return matrix;
        }


        //public Event[][] GenerateJaggedEventsTag()
        //{
        //List<EventTags> uniqueTags = CreateUniqueTags();
        //List<List<Event>> temporaryLists = new List<List<Event>>();
        //foreach(var t in uniqueTags)
        //{
        //    temporaryLists.Add(new List<Event>());
        //}
        //for (int i = 0; i < Events.Count; i++)
        //{
        //    Event currentEvent = Events[i];

        //    for (int t = 0; t < uniqueTags.Count; t++)
        //    {
        //        if (currentEvent.ContainsTag(uniqueTags[t]))
        //        {
        //            temporaryLists[t].Add(currentEvent);
        //        }
        //    }
        //}
        //Event[][] jaggedArrayEvents = new Event[uniqueTags.Count][];
        //for (int i = 0; i < temporaryLists.Count; i++)
        //{
        //    // Conversione della List<Event> in Event[] (Array interno)
        //    jaggedArrayEvents[i] = temporaryLists[i].ToArray();
        //}
        //return jaggedArrayEvents;
        //}
        public Event[][] GenerateJaggedEventsTag()
        {
            List<EventTags> uniqueTags = CreateUniqueTags();
            Event[][] jaggedArray = new Event[uniqueTags.Count][];
            for(int i = 0; i<uniqueTags.Count; i++)
            {
                List<Event> temporaryList = new List<Event>();
                foreach(var e in Events)
                {
                    if (e.ContainsTag(uniqueTags[i]))
                    {
                        temporaryList.Add(e);
                    }    
                }
                jaggedArray[i] = temporaryList.ToArray();
            }
            return jaggedArray;
        }

        /// metodo riceve un array di interi di dimensioni 9, verificare che al suo interno siano contenuti tutti i numeri da 1 a 9
        //public bool Contains9(int[] numbers)
        //{
        //    for (int i = 0; i < 9; i++)
        //    {
        //        bool found = false;
        //        for (int j = 0; j<numbers.Length; j++)
        //       {
        //            if (i == numbers[j])
        //            {
        //                found = true;
        //            }
                    
        //       }
        //        if (found == false)
        //            return false;
        //    }
        //    return true;
        //}

        public bool contains9(int[] numbers)
        {
            for(int i = 0; i<9; i++)
            {
                if (!numbers.Contains(i)) return false;
            }
            return false;
        }
        
        public bool HasDuplicates(int[] numbers)
        {
            for(int i = 0; i<numbers.Length; i++)
            {
                for(int j =i+1; j<numbers.Length; j++)
                {
                    if (numbers[i] == numbers[j])
                        return true;
                }
            }
            return false;
        }

        public int FindMaxIndex(int[] numbers)
        {
            foreach(var n in numbers)
            {
                if (n < 0) throw new ArgumentException(nameof(numbers));
            }
            int maxNumber = numbers[0];
            int maxIndx = 0;
            for(int i = 1; i<numbers.Length; i++)
            {
                if (numbers[i] > maxNumber)
                {
                    maxNumber = numbers[i];
                    maxIndx = i;
                }
            }
            return maxIndx;        
        }

        public int MaxGap(int[] numbers)
        {
            foreach(var n in numbers)
            {
                if (n < 0) throw new ArgumentException(nameof(numbers));
            }
            int maxGap = 0;
            for(int i = 0; i<numbers.Length-1; i++)
            {
                int currentGap = Math.Abs(numbers[i] - (numbers[i+1]));
                if (currentGap > maxGap)
                {
                    maxGap = currentGap;
                }
            }
            return maxGap;
        }

        public int[][] SplitParity(int[] numbers)
        {
            int[][] jaggedParity = new int[2][];
            List<int> even = new List<int>();
            List<int> odd = new List<int>();
            for(int i = 0; i< numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0) even.Add(numbers[i]);
                else odd.Add(numbers[i]);
            }
            jaggedParity[0] = even.ToArray();
            jaggedParity[1] = odd.ToArray();
            return jaggedParity;
        }

        public double[] RowAverages(double[][] jagged)
        {
            List<double> averages = new List<double>();
            for(int i = 0; i<jagged.Length; i++)
            {
                double sum = 0.0;
                for(int j = 0; j < jagged[i].Length; j++)
                {
                    sum += jagged[i][j];
                }
                double avg = sum / jagged[i].Length;
                averages.Add(avg);
            }
            return averages.ToArray();
        }
       
    }
}
