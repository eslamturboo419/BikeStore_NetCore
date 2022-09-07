using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Bike
    {

        public int Id { get; set; }

        public Make Make  { get; set; }
        [ForeignKey("Make")]
        public int MakeID { get; set; }

        public Model Model { get; set; }
        [ForeignKey("Model")]
        public int ModelID { get; set; }

        public int Year { get; set; }
        public int Mileage { get; set; }


        public string Features { get; set; }
        public string SeallerName { get; set; }
        public string SeallerEmail { get; set; }
        public string SeallerPhone { get; set; }

        public int Price { get; set; }
        public string Currency { get; set; }

        public string ImgURL { get; set; }

    }
}
