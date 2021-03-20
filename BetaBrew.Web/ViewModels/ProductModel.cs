﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BetaBrew.Web.ViewModels
{
    /// <summary>
    /// Product Entity DTO (Data Transfer Object)
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        [MaxLength(100)]       
        public string Name { get; set; }
        
        [MaxLength(255)] 
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public bool IsTaxable { get; set; }
        public bool IsArchived { get; set; }
    }
}