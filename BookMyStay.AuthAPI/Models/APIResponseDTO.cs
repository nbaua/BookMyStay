﻿
namespace BookMyStay.AuthAPI.Models
{
    public class APIResponseDTO
    {
        public object? Result { get; set; }
        public string? Info { get; set; }
        public bool HasError{ get; set; }
    }
}
