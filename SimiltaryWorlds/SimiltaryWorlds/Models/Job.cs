namespace SimiltaryWorlds.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int OrderId { get; set; }
        public float[] Embedding { get; set; }
        public double similitaryjob { get; set; }

        public string notes { get; set; }

        public Job()
        {
             notes = string.Empty;
        }
    }
}
