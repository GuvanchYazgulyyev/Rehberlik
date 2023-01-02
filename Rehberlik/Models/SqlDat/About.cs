using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string LoverTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Entrydate { get; set; }
        public bool IsDelete { get; set; }

    }
}
