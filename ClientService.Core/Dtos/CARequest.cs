namespace ClientService.Core.Dtos
{
    public class CARequest
    {
        public long IdClient { get; set; }
        public int IdStructure { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int All { get; set; } = 1;
    }
}
