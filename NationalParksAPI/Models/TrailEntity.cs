using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Models
{
    public class TrailEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double distance { get; set; }
        public enum DifficultyLevel { Easy, Moderate, Difficult, Expert}
        public DifficultyLevel Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalParkEntity NationalPark { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
