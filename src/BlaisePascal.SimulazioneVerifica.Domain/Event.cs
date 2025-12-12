namespace BlaisePascal.SimulazioneVerifica.Domain
{
    public class Event
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public double TicketCost { get; set; }
        public List<EventTags> EventTagList{get; set;}

        public Event(string name, DateOnly date, List<EventTags> eventTagList, double ticketCost)
        {
            Name = name;
            Date = date;
            EventTagList = eventTagList;
            TicketCost = ticketCost;
        }

        public bool ContainsTag(EventTags tag)
        {
            for(int i = 0; i < EventTagList.Count; i++)
            {
                if (EventTagList[i] == tag)
                    return true;
               
            }
            return false;
            
        }

        public bool ContainTags(EventTags[] tags)
        {
            for (int i = 0; i < tags.Length; i++)
            {
                bool tagIsFound = false;
                for (int j = 0; j < EventTagList.Count; j++)
                {

                    if (tags[i] == EventTagList[j])
                    {
                        tagIsFound = true;
                        break;
                    }
                }
                if (!tagIsFound)
                    return false;
            }
            return true;
        }
    }
}
