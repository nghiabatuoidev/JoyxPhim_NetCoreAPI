﻿namespace Backend.ViewModels
{
    public class ResponseViewModel
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public int? totalItems  { get; set; }
        public int ? totalPages { get; set; }
      
    }
}
