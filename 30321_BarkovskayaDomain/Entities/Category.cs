﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _30321_BarkovskayaDomain.Entities
{
    public class Category
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string NormalizedName { get; set; }
		//public ICollection<Dish> Dishes { get; set; }
	}
}
