﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BetaBrew.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        [MaxLength(100)]       
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public CustomerAddress PrimaryAddress { get; set; }
    }
}