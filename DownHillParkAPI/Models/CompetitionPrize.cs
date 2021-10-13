namespace DownHillParkAPI.Models
{
    public class CompetitionPrize
    {
        public int Id { get; set; }
        public string firstPlace { get; set; }
        public string secondPlace { get; set; }
        public string thirdPlace { get; set; }
        public int? CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
    }
}
