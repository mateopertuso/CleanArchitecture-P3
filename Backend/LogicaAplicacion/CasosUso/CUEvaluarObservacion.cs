using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using LogicaNegocio.ClasesDominio;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso
{
    public class CUEvaluarObservacion : IEvaluarObservacion
    {
        private readonly IRepositorioPrestamos _repoPrestamos;

        private readonly IRepositorioObjetosCelestes _repoObjetos;

        private readonly IEvaluadorObservacionIA _evaluadorIA;

        public CUEvaluarObservacion(IRepositorioPrestamos repoPrestamos,
            IRepositorioObjetosCelestes repoObjetos, IEvaluadorObservacionIA evaluadorIA)
        {
            _repoPrestamos = repoPrestamos;
            _repoObjetos = repoObjetos;
            _evaluadorIA = evaluadorIA;
        }

        public EvaluacionObservacionDTO Evaluar(int prestamoId, int objetoId)
        {
            try
            {
                // obtener el prestamo
                var prestamo = _repoPrestamos.FindById(prestamoId);

                if (prestamo == null)
                {
                    throw new DatosInvalidosException("Préstamo no encontrado");
                }

                // validar que esté EN PRÉSTAMO
                if (prestamo.Estado != EstadoPrestamo.EN_PRESTAMO)
                {
                    throw new DatosInvalidosException(
                        $"El préstamo debe estar en estado EN PRÉSTAMO. Estado actual: {prestamo.Estado}");
                }

                // validar que esté vigente hoy 
                if (!prestamo.EstaVigente())
                {
                    throw new DatosInvalidosException(
                        "El préstamo no está vigente en la fecha actual");
                }

                // obtener el objeto celeste
                var objeto = _repoObjetos.FindById(objetoId);

                if (objeto == null)
                {
                    throw new DatosInvalidosException("Objeto celeste no encontrado");
                }

                // EVALUAR CON GEMINI 
                var evaluacion = _evaluadorIA.Evaluar(prestamo, objeto);

                return evaluacion;
            }
            catch (DatosInvalidosException)
            {
                throw; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar la observación: " + ex.Message);
            }
        }
    }
}