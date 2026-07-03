using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using LogicaAplicacion.Mappers;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class CUListadoUsuariosCompleto : IListadoUsuariosCompleto
    {
        private readonly IRepositorioUsuarios _repo;

        public CUListadoUsuariosCompleto(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<UsuarioDTO> ObtenerListado()
        {
            return _repo
                .FindAll()
                .Select(MapperUsuarios.ToDto);
        }
    }
}
