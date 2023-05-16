namespace GitHubWebhooksManager.Models
{
    public class Rootobject
    {
        //class is auto-generated from JSON as Classes
        public string _ref { get; set; }
        public string before { get; set; }
        public string after { get; set; }
        public Repository repository { get; set; }
        public Pusher pusher { get; set; }
        public Sender sender { get; set; }
        public bool created { get; set; }
        public bool deleted { get; set; }
        public bool forced { get; set; }
        public object base_ref { get; set; }
        public string compare { get; set; }
        public Commit[] commits { get; set; }
        public Head_Commit head_commit { get; set; }
    }
}
