using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class OurService
    {
        [Key]
        public int Id { get; set; }
        public string ServiceNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ImageUrl { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDelate { get; set; }

    }
}
