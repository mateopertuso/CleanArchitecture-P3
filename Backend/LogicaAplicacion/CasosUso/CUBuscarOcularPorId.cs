using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUBuscarOcularPorId
        : IBuscarOcularPorId
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUBuscarOcularPorId(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public OcularDTO BuscarPorId(int id)
        {
            Equipo? equipo = RepoEquipos.FindById(id);

            if (equipo == null)
            {
                throw new OperacionInvalidaException("No existe el ocular");
            }

            Ocular ocular = (Ocular)equipo;

            return MapperEquipos.ToOcularDTO(ocular);
        }
    }
}