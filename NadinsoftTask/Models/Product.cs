﻿namespace NadinsoftTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set;}
        public bool IsAvailable { get; set; }
    }
}