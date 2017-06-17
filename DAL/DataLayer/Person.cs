using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataLayer
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public string TaxCode { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
