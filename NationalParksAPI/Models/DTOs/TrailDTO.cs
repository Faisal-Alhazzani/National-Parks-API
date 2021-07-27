using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static NationalParksAPI.Models.TrailEntity;

namespace NationalParksAPI.Models.DTOs
{
    public class TrailDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double distance { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        public NationalParkDTO NationalPark { get; set; }
    }
}
