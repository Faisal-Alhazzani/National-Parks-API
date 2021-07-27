using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Models
{
    public class NationalParkEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string state { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Eistablished { get; set; }
    }
}
