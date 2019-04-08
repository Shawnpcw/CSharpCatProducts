using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace productsCats.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        public string Name {get;set;}
        [Required]
        public string Description {get;set;}
        [Required]
        public decimal Price {get; set;}

        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;

        public List<Association> Associations {get;set;}
    }
}