﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _30321_Barkovskaya_API.Data;
using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;

namespace _30321_Barkovskaya_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DishesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Dishes
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Dish>>>> GetDishes(
            string? category,
            int pageNo = 1,
            int pageSize = 6
        )
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Dish>>();
            // Фильтрация по категории загрузка данных категории
            var data = _context.Dishes
                .Include(p => p.Category)
                .Where(p => String.IsNullOrEmpty(category) || p.Category.NormalizedName.Equals(category));
            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);

            if (pageNo > totalPages) pageNo = totalPages;
            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ListModel<Dish>()
            {
                Items = await data
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }

        // GET: api/Dishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.Id)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
        }

        // DELETE: api/Dishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(
           int id,
           IFormFile image
       )
        {
            // Найти объект по Id
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            dish.Image = url;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
