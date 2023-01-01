using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class ContactInformation
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Map { get; set; }
    }
}
