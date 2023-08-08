using System.ComponentModel.DataAnnotations;

namespace ToursimLoginAndRegistrationService.Models.DTOs
{
    public class StatusDTO
    {
        [Required]
        public int TravelAgentId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
