﻿namespace BSsystem0.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Shop> Shops { get; set; }

       
    }
}
