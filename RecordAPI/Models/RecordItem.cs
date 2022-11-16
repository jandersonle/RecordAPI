namespace RecordAPI.Models
{
    public class RecordItem
    {
        public long id { get; set; }
        public DateTime recordDate { get; set; }
        public bool recordPublished { get; set; }
        public bool recordReviewed { get; set; }

    }
}
