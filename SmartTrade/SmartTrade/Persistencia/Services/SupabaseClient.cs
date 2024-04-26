using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Entities;

public class SupabaseClient
{

    private readonly string _baseUrl = "https://apjeqdhvkthosokvpvma.supabase.co/rest/v1/";
    private readonly string _apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFwamVxZGh2a3Rob3Nva3Zwdm1hIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDkyODg4NjIsImV4cCI6MjAyNDg2NDg2Mn0.f7CoRtVVO60qER7HzCtcZyELhSEf_GbdMsSYiJq_7iE";
    private HttpResponseMessage responseSubclase;

    public async Task<List<Producto>> ObtenerProductosAsync()
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("apikey", _apiKey);
                var response = await httpClient.GetAsync(_baseUrl + "Producto");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var productos = JsonConvert.DeserializeObject<List<Respuesta_Producto>>(json);
                    List<Producto> listaProductosCompleta = new List<Producto>();
                    foreach (var respuestaProducto in productos)
                    {
                        Debug.WriteLine("ID: " + respuestaProducto.Id + " | Categoria: " + respuestaProducto.Categoria);
                        // Aquí asumimos que tienes un endpoint separado para cada subclase
                        // Por ejemplo: _baseUrl + "Ropa/" + respuestaProducto.Id para obtener detalles de Ropa
                        responseSubclase = await httpClient.GetAsync(_baseUrl + respuestaProducto.Categoria + "?id=eq." + respuestaProducto.Id);
                        if (responseSubclase.IsSuccessStatusCode)
                        {
                            var jsonSubclase = await responseSubclase.Content.ReadAsStringAsync();
                            Debug.WriteLine("JSON_SUBCLASE: " + jsonSubclase);
                            var productoSubclase = JsonConvert.DeserializeObject<Producto>(jsonSubclase);
                            // Aquí debes combinar la información de respuestaProducto con productoSubclase
                            // y agregar el producto completo a listaProductosCompleta
                        }
                    }
                    return listaProductosCompleta;
                }
                else
                {
                    Debug.WriteLine("Error al obtener productos: " + response.StatusCode);
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Producto> ObtenerProductoAsync(int id)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_baseUrl + "Producto?id=eq." + id + "&apikey=" + _apiKey);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //obtener id del json
                    var producto = JsonConvert.DeserializeObject<Producto>(json);
                    Debug.WriteLine("ID: " + producto.Id + " | Categoria: " + producto.Categoria);
                    return producto;
                    /*
                    responseSubclase = await httpClient.GetAsync(_baseUrl + producto.Categoria + "?id=eq." + producto.Id);

                    if (responseSubclase.IsSuccessStatusCode)
                    {
                        var jsonSubclase = await responseSubclase.Content.ReadAsStringAsync();
                        Debug.WriteLine("JSON_SUBCLASE: " + jsonSubclase);
                        var productoSubclase = JsonConvert.DeserializeObject<Producto>(jsonSubclase);
                        // Aquí debes combinar la información de producto con productoSubclase
                        return productoSubclase;
                    }
                    else
                    {
                        Debug.WriteLine("Error al obtener producto: " + responseSubclase.StatusCode);
                    }

                    return null;*/
                }
                else
                {
                    Debug.WriteLine("Error al obtener producto: " + response.StatusCode);
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            return null;
        }

    }

    class Respuesta_Producto : Producto
    {
    }


}
