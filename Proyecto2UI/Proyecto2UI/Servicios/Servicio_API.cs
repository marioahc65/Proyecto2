using Newtonsoft.Json;
using Proyecto2UI.Models;
using System;
using System.Text;

namespace Proyecto2UI.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _baseUrl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<Cliente> CrearCliente(Cliente objeto)
        {
            Cliente cliente = new Cliente();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Cliente/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Cliente>(json_respuesta);
                cliente = resultado;
            }

            return cliente;
        }

        public async Task<Libro> CrearLibro(Libro objeto)
        {
            Libro libro = new Libro();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Libro/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Libro>(json_respuesta);
                libro = resultado;
            }

            return libro;
        }

        public async Task<LibroRetirado> CrearLibroRetirado(LibroRetirado objeto)
        {
            LibroRetirado libroRetirado = new LibroRetirado();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);


            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("LibroRetirado/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroRetirado>(json_respuesta);
                libroRetirado = resultado;
            }

            return libroRetirado;
        }

        public async Task<LibroStock> CrearLibroStock(LibroStock objeto)
        {
            LibroStock libroStock = new LibroStock();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("LibroStock/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroStock>(json_respuesta);
                libroStock = resultado;
            }

            return libroStock;
        }

        public async Task<Cliente> ModificarCliente(long ClienteId, Cliente objeto)
        {
            Cliente cliente = new Cliente();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"Cliente/{ClienteId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Cliente>(json_respuesta);
                cliente = resultado;
            }

            return cliente;
        }

        public async Task<Libro> ModificarLibro(long LibroId, Libro objeto)
        {
            Libro libro = new Libro();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"Libro/{LibroId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Libro>(json_respuesta);
                libro = resultado;
            }

            return libro;
        }

        public async Task<LibroRetirado> ModificarLibroRetirado(long LibroRetiradoId, LibroStock objeto)
        {
            LibroRetirado libroRetirado = new LibroRetirado();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"LibroRetirado/{LibroRetiradoId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroRetirado>(json_respuesta);
                libroRetirado = resultado;
            }

            return libroRetirado;
        }

        public async Task<LibroStock> ModificarLibroStock(long LibroStockId, LibroStock objeto)
        {
            LibroStock libroStock= new LibroStock();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"LibroStock/{LibroStockId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroStock>(json_respuesta);
                libroStock = resultado;
            }

            return libroStock;
        }

        public async Task<Cliente> ObtenerCliente(long ClienteId)
        {
            Cliente cliente = new Cliente();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"Cliente/{ClienteId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Cliente>(json_respuesta);
                cliente = resultado;
            }

            return cliente;
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync("Cliente/");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Cliente>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;

        }

        public async Task<Libro> ObtenerLibro(long LibroId)
        {
            Libro libro = new Libro();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"Libro/{LibroId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Libro>(json_respuesta);
                libro = resultado;
            }

            return libro;
        }

        public async Task<List<LibroRetirado>> ObtenerLibroRetiradoPorFecha(DateTime FechaInicio, DateTime FechaFinal)
        {
            List<LibroRetirado> lista = new List<LibroRetirado>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"LibroRetirado/{FechaInicio}/{FechaFinal}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<LibroRetirado>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<List<Libro>> ObtenerLibros()
        {
            try { 
            List<Libro> lista = new List<Libro>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync("Libro/");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Libro>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LibroStock>> ObtenerLibroStock()
        {
            List<LibroStock> lista = new List<LibroStock>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync("LibroStock/");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<LibroStock>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<List<LibroStock>> ObtenerLibroStockCliente(long ClienteId)
        {
            List<LibroStock> lista = new List<LibroStock>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"LibroStock/Cliente/{ClienteId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<LibroStock>>(json_respuesta);
                lista = resultado.ToList();
            }

            return lista;
        }

        public async Task<LibroStock> ObtenerLibroStockId(long LibroStockId)
        {
            try { 
            LibroStock libroStock = new LibroStock();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"LibroStock/{LibroStockId}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroStock>(json_respuesta);
                libroStock = resultado;
            }

            return libroStock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LibroStock>> ObtenerLibroStockSinReservar()
        {
            try
            {
                List<LibroStock> lista = new List<LibroStock>();

                var client = new HttpClient();
                client.BaseAddress = new Uri(_baseUrl);
                var response = await client.GetAsync("LibroStockSinReservar/");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<List<LibroStock>>(json_respuesta);
                    lista = resultado.ToList();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
