namespace PresentacionWebMVC.Models.DTOs
{
    public class AltaOcularDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadDisponible { get; set; }

        public double DiametroMm { get; set; }
        public double CampoVisualGrados { get; set; }
    }
}
