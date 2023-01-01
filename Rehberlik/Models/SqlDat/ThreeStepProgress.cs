using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class ThreeStepProgress
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDelate { get; set; }
    }
}
