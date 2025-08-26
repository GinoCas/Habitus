using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorComida
    {
        private List<Comida> _comidas;
        private List<Alimento> _baseDatosAlimentos;
        private string _rutaArchivoComidas = "comidas.json";
        private string _rutaArchivoAlimentos = "alimentos.json";

        public ControladorComida()
        {
            CargarComidas();
            CargarBaseDatosAlimentos();
        }

        public void RegistrarComida(string nombreAlimento, double cantidad, string tipoComida)
        {
            var alimento = _baseDatosAlimentos.FirstOrDefault(a => a.Nombre.ToLower() == nombreAlimento.ToLower());
            double calorias = 0;

            if (alimento != null)
            {
                calorias = (alimento.CaloriasPor100g * cantidad) / 100;
            }
            else
            {
                // Si no está en la base de datos, permitir ingreso manual
                calorias = cantidad; // Asumiendo que el usuario ingresa las calorías directamente
            }

            var comida = new Comida
            {
                Nombre = nombreAlimento,
                Calorias = calorias,
                Tipo = tipoComida,
                Fecha = DateTime.Now
            };

            _comidas.Add(comida);
            GuardarComidas();
        }

        public List<Comida> ObtenerComidasPorFecha(DateTime fecha)
        {
            return _comidas.Where(c => c.Fecha.Date == fecha.Date).ToList();
        }

        public List<Comida> ObtenerComidasPorTipo(DateTime fecha, string tipoComida)
        {
            return _comidas.Where(c => c.Fecha.Date == fecha.Date && c.Tipo.ToLower() == tipoComida.ToLower()).ToList();
        }

        public List<Comida> ObtenerComidasPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _comidas.Where(c => c.Fecha.Date >= fechaInicio.Date && c.Fecha.Date <= fechaFin.Date).ToList();
        }

        public double ObtenerTotalCaloriasConsumidas(DateTime fecha)
        {
            return _comidas.Where(c => c.Fecha.Date == fecha.Date)
                          .Sum(c => c.Calorias);
        }

        public List<Alimento> BuscarAlimentos(string termino)
        {
            return _baseDatosAlimentos.Where(a => a.Nombre.ToLower().Contains(termino.ToLower()))
                                     .Take(10)
                                     .ToList();
        }

        public void AgregarAlimento(Alimento alimento)
        {
            _baseDatosAlimentos.Add(alimento);
            GuardarBaseDatosAlimentos();
        }

        private void CargarComidas()
        {
            try
            {
                if (File.Exists(_rutaArchivoComidas))
                {
                    string json = File.ReadAllText(_rutaArchivoComidas);
                    _comidas = System.Text.Json.JsonSerializer.Deserialize<List<Comida>>(json) ?? new List<Comida>();
                }
                else
                {
                    _comidas = new List<Comida>();
                }
            }
            catch (Exception ex)
            {
                _comidas = new List<Comida>();
            }
        }

        private void GuardarComidas()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_comidas, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivoComidas, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private void CargarBaseDatosAlimentos()
        {
            try
            {
                if (File.Exists(_rutaArchivoAlimentos))
                {
                    string json = File.ReadAllText(_rutaArchivoAlimentos);
                    _baseDatosAlimentos = System.Text.Json.JsonSerializer.Deserialize<List<Alimento>>(json) ?? new List<Alimento>();
                }
                else
                {
                    _baseDatosAlimentos = InicializarAlimentosBasicos();
                    GuardarBaseDatosAlimentos();
                }
            }
            catch (Exception ex)
            {
                _baseDatosAlimentos = InicializarAlimentosBasicos();
            }
        }

        private void GuardarBaseDatosAlimentos()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_baseDatosAlimentos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivoAlimentos, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private List<Alimento> InicializarAlimentosBasicos()
        {
            return new List<Alimento>
            {
                new Alimento { Nombre = "Manzana", CaloriasPor100g = 52, Proteinas = 0.3, Carbohidratos = 14, Grasas = 0.2, Fibra = 2.4, Categoria = "Frutas" },
                new Alimento { Nombre = "Plátano", CaloriasPor100g = 89, Proteinas = 1.1, Carbohidratos = 23, Grasas = 0.3, Fibra = 2.6, Categoria = "Frutas" },
                new Alimento { Nombre = "Pollo (pechuga)", CaloriasPor100g = 165, Proteinas = 31, Carbohidratos = 0, Grasas = 3.6, Fibra = 0, Categoria = "Carnes" },
                new Alimento { Nombre = "Arroz blanco", CaloriasPor100g = 130, Proteinas = 2.7, Carbohidratos = 28, Grasas = 0.3, Fibra = 0.4, Categoria = "Cereales" },
                new Alimento { Nombre = "Brócoli", CaloriasPor100g = 34, Proteinas = 2.8, Carbohidratos = 7, Grasas = 0.4, Fibra = 2.6, Categoria = "Verduras" }
            };
        }
    }
}