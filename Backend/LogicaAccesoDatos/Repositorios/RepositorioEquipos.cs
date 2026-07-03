//using Excepciones;
//using LogicaNegocio.ClasesDominio;
//using LogicaNegocio.Enums;
//using LogicaNegocio.InterfacesRepositorio;

//namespace LogicaAccesoDatos.Repositorios
//{
//    public class RepositorioEquipos : IRepositorioEquipos
//    {
//        private static List<Equipo> _equipos = new List<Equipo>();
//        private static int _ultId = 0;

//        public RepositorioEquipos()
//        {
//            // Seed simple (solo una vez)
//            if (!_equipos.Any())
//            {
//                _equipos.Add(new Telescopio
//                {
//                    Id = ++_ultId,
//                    Marca = "Celestron",
//                    Modelo = "NexStar 130",
//                    CantidadDisponible = 3,
//                    AperturaMm = 130,
//                    DistanciaFocalMm = 650,
//                    RelacionFocal = 5,
//                    PesoKg = 5
//                });

//                _equipos.Add(new Montura
//                {
//                    Id = ++_ultId,
//                    Marca = "SkyWatcher",
//                    Modelo = "EQ5",
//                    CantidadDisponible = 2,
//                    TipoMontura = TipoMontura.ECUATORIAL,
//                    CargaMaximaKg = 10,
//                    EsGoTo = true
//                });

//                _equipos.Add(new Camara
//                {
//                    Id = ++_ultId,
//                    Marca = "ZWO",
//                    Modelo = "ASI120",
//                    CantidadDisponible = 2,
//                    TipoSensor = TipoSensor.CMOS,
//                    Resolucion = "1280x960",
//                    TamanoPixelUm = 3.75
//                });

//                _equipos.Add(new Ocular
//                {
//                    Id = ++_ultId,
//                    Marca = "Meade",
//                    Modelo = "Plossl 25mm",
//                    CantidadDisponible = 5,
//                    DiametroMm = 25,
//                    CampoVisualGrados = 52
//                });
//            }
//        }

//        public void Add(Equipo nuevo)
//        {
//            nuevo.Id = ++_ultId;
//            _equipos.Add(nuevo);
//        }

//        public IEnumerable<Equipo> FindAll()
//        {
//            return _equipos;
//        }

//        public Equipo? FindById(int id)
//        {
//            return _equipos.FirstOrDefault(e => e.Id == id);
//        }

//        public void Remove(int id)
//        {
//            var eq = FindById(id);
//            if (eq == null)
//                throw new OperacionInvalidaException("Equipo no encontrado");

//            _equipos.Remove(eq);
//        }

//        public void Update(Equipo nuevo)
//        {
//            var existente = FindById(nuevo.Id);
//            if (existente == null)
//                throw new OperacionInvalidaException("Equipo no encontrado");

//            existente.Marca = nuevo.Marca;
//            existente.Modelo = nuevo.Modelo;
//            existente.CantidadDisponible = nuevo.CantidadDisponible;
//        }

//        public IEnumerable<Equipo> ObtenerDisponibles()
//        {
//            return _equipos
//                .Where(e => e.CantidadDisponible > 0)
//                .ToList();
//        }

//        public IEnumerable<Telescopio> ObtenerTelescopios()
//        {
//            return _equipos
//                .OfType<Telescopio>() //filtra solo por tipo telescopio en este caso
//                .ToList();
//        }

//        public IEnumerable<Montura> ObtenerMonturas()
//        {
//            return _equipos
//                .OfType<Montura>()
//                .ToList();
//        }

//        public IEnumerable<Camara> ObtenerCamaras()
//        {
//            return _equipos
//                .OfType<Camara>()
//                .ToList();
//        }

//        public IEnumerable<Ocular> ObtenerOculares()
//        {
//            return _equipos
//                .OfType<Ocular>()
//                .ToList();
//        }
//    }
//}