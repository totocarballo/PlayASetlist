using System.Collections.Generic;

namespace PlayASetlist.Library.Http
{
    public class Data
    {
        public List<Option> Options { get; set; }
        public List<Selected> Selected { get; set; }
        public string TimeLeft { get; set; }
        public string Title { get; set; }
    }

    public class Option
    {
        public string Name { get; set; }
        public int Votes { get; set; }
    }

    public class Selected
    {
        public string Name { get; set; }
        public int Votes { get; set; }
    }
}