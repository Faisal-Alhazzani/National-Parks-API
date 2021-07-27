using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParksAPI.Models.DTOs
{
    public class NationalParkCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string state { get; set; }
        public byte[] picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Eistablished { get; set; }
    }
}
