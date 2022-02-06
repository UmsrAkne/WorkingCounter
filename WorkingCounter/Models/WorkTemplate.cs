namespace WorkingCounter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class WorkTemplate
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int DayCountToStart { get; set; }

        [Required]
        public int DayCountToLimit { get; set; }

        [Required]
        public int Quota { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string GroupName { get; set; } = string.Empty;
    }
}
