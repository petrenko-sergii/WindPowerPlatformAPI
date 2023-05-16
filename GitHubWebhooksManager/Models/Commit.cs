using System;

namespace GitHubWebhooksManager.Models
{
    //class is auto-generated from JSON as Classes
    public class Commit
    {
        public string id { get; set; }
        public string tree_id { get; set; }
        public bool distinct { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public object[] added { get; set; }
        public object[] removed { get; set; }
        public string[] modified { get; set; }
    }
}
