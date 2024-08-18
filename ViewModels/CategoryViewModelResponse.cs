using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class CategoryViewModelResponse
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? Slug { get; set; }
    }
}
