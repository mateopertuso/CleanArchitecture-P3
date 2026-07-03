using CasosUso.DTOs;


namespace CasosUso.InterfacesCU
{
    public interface ICrearPrestamo
    {
        PrestamoDTO Ejecutar(AltaPrestamoDTO dto); //devuelvo prestamoDTO porque el controller no debe recibir entidad del dominio
    }
}
