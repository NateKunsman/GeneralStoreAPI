using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Product
    {
        //SKU  aka barcode number
        [Key]
        public string SKU { get; set; }
        //Name
        [Required]
        public string Name { get; set; }
        [Required]
        //Price
        public decimal Price { get; set; }
        //Description
        public string Description { get; set; }
        //Number in stock
        [Required]
        public int NumInStock { get; set; }
        //InStock
        public bool IsInStock
        {
            get
            {
                return NumInStock > 0;
            }
        }
    }
}