namespace ModelService_ArgusBackend.Models
{
    public class Model
    {
        public string LogId { get; set; }

        public string BoxId { get; set; }

        public string ItemType { get; set; }

        public string UserId { get; set; }
        
        public DateTime StartTime { get; set; }
    }
}