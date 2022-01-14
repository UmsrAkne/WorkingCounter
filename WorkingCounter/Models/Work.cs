namespace WorkingCounter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Work
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime AdditionDate { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ノルマを表す 
        /// </summary>
        [Required]
        public int Quota { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
