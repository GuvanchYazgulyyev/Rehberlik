using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriprion1 { get; set; }
        public string Descriprion2 { get; set; }
        public string Image { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDelate { get; set; }
    }
}
