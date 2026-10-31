using System.ComponentModel.DataAnnotations;

namespace PlacementDashboard.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Department { get; set; } = "";

        [Required]
        public string Company { get; set; } = "";

        public double Package { get; set; }

        public string Status { get; set; } = "Not Placed";
    }
}