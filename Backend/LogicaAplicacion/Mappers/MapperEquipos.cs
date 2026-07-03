using CasosUso.DTOs;
using CasosUso.DTOs.Editar_equipos;
using LogicaNegocio.ClasesDominio;

namespace LogicaAplicacion.Mappers
{
    public class MapperEquipos
    {
        public static Telescopio ToTelescopio(AltaTelescopioDTO dto)
        {
            return new Telescopio
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                AperturaMm = dto.AperturaMm,
                RelacionFocal = dto.RelacionFocal,
                DistanciaFocalMm = dto.DistanciaFocalMm,
                PesoKg = dto.PesoKg
            };
        }

        public static Telescopio ToTelescopio(ModificarTelescopioDTO dto)
        {
            return new Telescopio
            {
                Id = dto.Id,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                AperturaMm = dto.AperturaMm,
                RelacionFocal = dto.RelacionFocal,
                DistanciaFocalMm = dto.DistanciaFocalMm,
                PesoKg = dto.PesoKg
            };
        }

        public static TelescopioDTO ToTelescopioDTO(Telescopio telescopio)
        {
            return new TelescopioDTO
            {
                Id = telescopio.Id,
                Marca = telescopio.Marca,
                Modelo = telescopio.Modelo,
                CantidadDisponible = telescopio.CantidadDisponible,
                AperturaMm = telescopio.AperturaMm,
                RelacionFocal = telescopio.RelacionFocal,
                DistanciaFocalMm = telescopio.DistanciaFocalMm,
                PesoKg = telescopio.PesoKg
            };
        }

        public static Montura ToMontura(AltaMonturaDTO dto)
        {
            return new Montura
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                TipoMontura = dto.TipoMontura,
                CargaMaximaKg = dto.CargaMaximaKg,
                EsGoTo = dto.EsGoTo
            };
        }

        public static Montura ToMontura(ModificarMonturaDTO dto)
        {
            return new Montura
            {
                Id = dto.Id,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                TipoMontura = dto.TipoMontura,
                CargaMaximaKg = dto.CargaMaximaKg,
                EsGoTo = dto.EsGoTo
            };
        }

        public static MonturaDTO ToMonturaDTO(Montura montura)
        {
            return new MonturaDTO
            {
                Id = montura.Id,
                Marca = montura.Marca,
                Modelo = montura.Modelo,
                CantidadDisponible = montura.CantidadDisponible,
                TipoMontura = montura.TipoMontura,
                CargaMaximaKg = montura.CargaMaximaKg,
                EsGoTo = montura.EsGoTo
            };
        }

        public static Camara ToCamara(AltaCamaraDTO dto)
        {
            return new Camara
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                TipoSensor = dto.TipoSensor,
                Resolucion = dto.Resolucion,
                TamanoPixelUm = dto.TamanoPixelUm
            };
        }

        public static Camara ToCamara(ModificarCamaraDTO dto)
        {
            return new Camara
            {
                Id = dto.Id,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                TipoSensor = dto.TipoSensor,
                Resolucion = dto.Resolucion,
                TamanoPixelUm = dto.TamanoPixelUm
            };
        }

        public static CamaraDTO ToCamaraDTO(Camara camara)
        {
            return new CamaraDTO
            {
                Id = camara.Id,
                Marca = camara.Marca,
                Modelo = camara.Modelo,
                CantidadDisponible = camara.CantidadDisponible,
                TipoSensor = camara.TipoSensor,
                Resolucion = camara.Resolucion,
                TamanoPixelUm = camara.TamanoPixelUm
            };
        }

        public static Ocular ToOcular(AltaOcularDTO dto)
        {
            return new Ocular
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                DiametroMm = dto.DiametroMm,
                CampoVisualGrados = dto.CampoVisualGrados
            };
        }

        public static Ocular ToOcular(ModificarOcularDTO dto)
        {
            return new Ocular
            {
                Id = dto.Id,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                CantidadDisponible = dto.CantidadDisponible,
                DiametroMm = dto.DiametroMm,
                CampoVisualGrados = dto.CampoVisualGrados
            };
        }

        public static OcularDTO ToOcularDTO(Ocular ocular)
        {
            return new OcularDTO
            {
                Id = ocular.Id,
                Marca = ocular.Marca,
                Modelo = ocular.Modelo,
                CantidadDisponible = ocular.CantidadDisponible,
                DiametroMm = ocular.DiametroMm,
                CampoVisualGrados = ocular.CampoVisualGrados
            };
        }

        public static EquipoDTO ToEquipoDTO(Equipo equipo)
        {
            return new EquipoDTO
            {
                Id = equipo.Id,
                Marca = equipo.Marca,
                Modelo = equipo.Modelo,
                CantidadDisponible = equipo.CantidadDisponible,
                TipoEquipo = equipo.GetType().Name
            };
        }
    }
}