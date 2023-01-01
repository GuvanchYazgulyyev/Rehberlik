using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string ContactNo { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Message { get; set; }
    }
}
