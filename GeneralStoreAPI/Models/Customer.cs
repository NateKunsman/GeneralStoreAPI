using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Customer
    {
        //Unique Identifier
        [Key]
        public int Id { get; set; }
        //First
        [Required]
        public string FirstName { get; set; }
        //Last
        [Required]
        public string LastName { get; set; }
        //FullName
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}