//using Excepciones;
//using LogicaNegocio.ClasesDominio;
//using LogicaNegocio.Enums;
//using LogicaNegocio.InterfacesRepositorio;

//namespace LogicaAccesoDatos.Repositorios
//{
//    public class RepositorioPrestamos : IRepositorioPrestamos
//    {
//        private static List<Prestamo> _prestamos = new List<Prestamo>();
//        private static int _ultId = 0;

//        public void Add(Prestamo nuevo)
//        {
//            nuevo.Id = ++_ultId;
//            _prestamos.Add(nuevo);
//        }

//        public IEnumerable<Prestamo> FindAll()
//        {
//            return _prestamos;
//        }

//        public Prestamo? FindById(int id)
//        {
//            return _prestamos.FirstOrDefault(p => p.Id == id);
//        }

//        public void Remove(int id)
//        {
//            var prestamo = FindById(id);
//            if (prestamo == null)
//                throw new OperacionInvalidaException("Préstamo no encontrado");

//            _prestamos.Remove(prestamo);
//        }

//        public void Update(Prestamo nuevo)
//        {
//            var existente = FindById(nuevo.Id);
//            if (existente == null)
//                throw new OperacionInvalidaException("Préstamo no encontrado");

//            existente.FechaInicio = nuevo.FechaInicio;
//            existente.FechaFin = nuevo.FechaFin;
//            existente.Estado = nuevo.Estado;
//        }

//        public IEnumerable<Prestamo> FindActivosByUsuario(int usuarioId)
//        {
//            return _prestamos
//                .Where(p => p.Usuario.Id == usuarioId &&
//                            p.Estado == EstadoPrestamo.EN_PRESTAMO)
//                .ToList();
//        }
//    }
//}

