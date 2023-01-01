using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class CustomerComment
    {
        [Key]
        public int Id { get; set; }
        public string Branch { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDalete { get; set; }



    }
}
