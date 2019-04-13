using System.ComponentModel.DataAnnotations;
namespace Production.Models {
    public class Product {
        public int id {set; get;}
        public string p_name {get; set;}
        public string  description {get; set;}
        [Range(1,int.MaxValue)]
        public int amount {get; set;}
        [Range(0,float.MaxValue)]
        public float price {get; set;}
        public int material_id {get; set;}
        [Range(1,int.MaxValue)]
        public int material {get; set;}
    }
}