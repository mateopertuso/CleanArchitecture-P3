using CasosUso.DTOs;
using CasosUso.DTOs.Editar_equipos;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUModificarOcular : IModificarOcular
    {
        public IRepositorioEquipos RepoEquipos { get; set; }

        public CUModificarOcular(IRepositorioEquipos repoEquipos)
        {
            RepoEquipos = repoEquipos;
        }

        public void EjecutarModificacion(ModificarOcularDTO dto)
        {
            Ocular ocular = MapperEquipos.ToOcular(dto);

            RepoEquipos.Update(ocular);
        }
    }
}