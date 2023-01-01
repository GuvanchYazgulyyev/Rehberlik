using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string ItemNo { get; set; }
        public string WhoWriten { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDelate { get; set; }
    }
}
