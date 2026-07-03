namespace PresentacionWebMVC.Models.DTOs
{
    public class ModificarOcularDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }
        public double DiametroMm { get; set; }
        public double CampoVisualGrados { get; set; }
    }
}
