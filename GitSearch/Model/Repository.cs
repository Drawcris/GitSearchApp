using System;


namespace GitSearch.Model
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string html_url { get; set; }
        public string avatar_url { get; set; }
        public DateTime created_at { get; set; }
        public string login { get; set; }
        public int id { get; set; }
    }

}
