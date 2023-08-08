using System.ComponentModel.DataAnnotations;

namespace Toursimtestimonyservice.Models
{
    public class FeedBack
    {
        [Key]
        public int FeddBackId { get; set; } 

        public string ?FeedbackDescription{ get; set; }  

        public string ? TourName { get; set; }

        public int? Rating { get;set; }

        public int? UserId{ get; set; }
        


    }
}
