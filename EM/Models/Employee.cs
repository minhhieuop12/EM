using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public string? id {  get; set; }
        public string? name { get; set; }
        public int? age { get; set; }
        public string? subject { get; set; }
        public string? address { get; set; }
        public int? salary { get; set; }
    }
}
