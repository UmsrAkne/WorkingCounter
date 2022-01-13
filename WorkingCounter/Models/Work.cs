namespace WorkingCounter.Models
{
    using System;

    public class Work
    {
        public DateTime AdditionDate { get; set; }

        /// <summary>
        /// ノルマを表す 
        /// </summary>
        public int Quota { get; set; }

        public string Unit { get; set; }
    }
}
