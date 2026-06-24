using System.ComponentModel.DataAnnotations;

namespace p1.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Date { get; set; } = string.Empty;

        [Required]
        public string Time { get; set; } = string.Empty;

        [Required]
        public int Duration { get; set; } = 30;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}