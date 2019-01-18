namespace Production.Models {
    public class Cart {
        public int id {get; set;}
        public float sum {get; set;}
        public string item_name {get; set;}
        public int items_amount {get;set;}
        public string user_id {get; set;}
    }
}