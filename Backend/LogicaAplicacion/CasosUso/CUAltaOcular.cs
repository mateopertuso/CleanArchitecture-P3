using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUAltaOcular : IAltaOcular
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUAltaOcular(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarAlta(AltaOcularDTO dto)
        {
            Ocular ocular = MapperEquipos.ToOcular(dto);

            RepoEquipos.Add(ocular);
        }
    }
}