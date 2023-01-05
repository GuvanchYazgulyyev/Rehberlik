using System.ComponentModel.DataAnnotations;

namespace Rehberlik.Models.SqlDat
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string AdminNo { get; set; }
        public string NameSurname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Tel { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
