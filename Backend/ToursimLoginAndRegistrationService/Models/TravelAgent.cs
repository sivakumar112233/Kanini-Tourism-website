using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursimLoginAndRegistrationService.Models
{
    public class TravelAgent
    {
        [Key]
        public int TravelAgentId { get; set; }
        [ForeignKey("TravelAgentId")]
        public User? Users { get; set; }
       
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }


       
        public string? AgencyName { get; set; }

       
        
        public string? ContactNumber { get; set; }
        public string ? AgentName { get;set; }
        
        public string? Address { get; set; }

        

        public bool IsActive{ get; set; }

        

    }
}
