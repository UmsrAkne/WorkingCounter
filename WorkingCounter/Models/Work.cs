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
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LimitDate { get; set; } = DateTime.Today.AddDays(3);

        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ノルマを表す
        /// </summary>
        /// <value>ノルマを表す数値</value>
        [Required]
        public int Quota { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [NotMapped]
        public List<WorkingUnit> Units { get; set; }

        [NotMapped]
        public bool IsComplete => Units.Count >= Quota;

        [NotMapped]
        public string Status
        {
            get
            {
                if (IsComplete)
                {
                    return "完了";
                }

                return DateTime.Now > LimitDate ? "期限切れ" : "-";
            }
        }
    }
}
