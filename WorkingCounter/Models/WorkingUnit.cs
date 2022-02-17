namespace WorkingCounter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkingUnit
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime AdditionDate { get; set; }

        [Required]
        public int ParentWorkId { get; set; }

        [Required]
        public string Memo { get; set; } = string.Empty;

        public bool MemoIsEmpty => Memo == string.Empty;
    }
}
