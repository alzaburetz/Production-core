using System.ComponentModel.DataAnnotations;
namespace Production.Models {
    public class Materials {
        public int id {get; set;}
        [Required]
        public string materialname {get; set;}
        [Range(1,int.MaxValue)]
        public int amount {get; set;}
        [Required]
        public int m_id {get; set;}


    }
}