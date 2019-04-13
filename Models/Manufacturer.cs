using System.ComponentModel.DataAnnotations;
namespace Production.Models {
    public class Manufacturer{
        public int id {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Address {get; set;}
    }
}