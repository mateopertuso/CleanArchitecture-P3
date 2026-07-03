using LogicaNegocio.ClasesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPrestamos : IRepositorio<Prestamo>
    {
        IEnumerable<Prestamo> FindActivosByUsuario(int usuarioId); //los que estan en prestamo
        bool EquipoEstaPrestado(int equipoId);
        List<Prestamo> ObtenerPrestamosSocio(int usuarioId, int mes, int anio);
        IEnumerable<Usuario> ObtenerSociosPorTelescopio(int telescopioId);
    }
}
