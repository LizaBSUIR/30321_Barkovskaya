using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30321_BarkovskayaDomain.Entities
{
    public class Dish
    {
         public int Id { get; set; } //ID
		 public string Name { get; set; } //название
		 public string Description { get; set; } //Описание
	 	 public int Price { get; set; } // цена
		public string? Image { get; set; } // картинка
        public int CategoryId { get; set; } //категория
        public Category? Category { get; set; } //Навигационное свойство к категории
												 
	}
}
