namespace Production.Models {
    public class Product {
        public int id {set; get;}
        public string p_name {get; set;}
        public string  description {get; set;}
        public int amount {get; set;}
        public float price {get; set;}
        public int m_id {get; set;}
        public int material_id {get; set;}
        public int material {get; set;}
    }
}