namespace WorkingCounter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Work
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime AdditionDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime LimitDate { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ノルマを表す 
        /// </summary>
        [Required]
        public int Quota { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [NotMapped]
        public List<WorkingUnit> Units { get; set; }

        [NotMapped]
        public bool IsComplete => Units.Count >= Quota;
    }
}
