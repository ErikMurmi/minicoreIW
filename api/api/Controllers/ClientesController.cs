using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Text.Json.Serialization;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ClientesController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IConfiguration _configuration;

    public ClientesController(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    [HttpGet(Name = "GetClientes")]
    public string Get()
    {
      string query = "select * from cliente";
      DataTable table = new DataTable();
      string? sqlDataSource = _configuration.GetConnectionString("MinicoreDB");
      NpgsqlDataReader myReader;

      using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
        {
          myReader = myCommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          myCon.Close();
        }
      }

      return JsonConvert.SerializeObject(table);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetTotales([FromQuery]DateTime inicio,DateTime fin)
    {
      if (inicio == DateTime.MinValue || fin == DateTime.MinValue)
      {
        return BadRequest("Es necesario especificar un rango de fechas.");
      }

      List<Contrato> contratos = new List<Contrato>();
      List<Cliente> clientes = new List<Cliente>();
      string query = "select * from cliente";
      string query_contratos = string.Format("SELECT * FROM contratos WHERE fecha >= '{0}' AND fecha <= '{1}'", inicio, fin);
      DataTable table = new DataTable();
      DataTable table_contratos = new DataTable();
      NpgsqlDataReader myReader;
      string? sqlDataSource = _configuration.GetConnectionString("MinicoreDB");

      using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
      {
        myCon.Open();
        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
        {
          myReader = myCommand.ExecuteReader();
          table.Load(myReader);
          clientes = JsonConvert.DeserializeObject<List<Cliente>>(JsonConvert.SerializeObject(table));
        }
        using (NpgsqlCommand myCommand = new NpgsqlCommand(query_contratos, myCon))
        {
          myReader = myCommand.ExecuteReader();
          table_contratos.Load(myReader);
          //contratos = JsonConvert.DeserializeObject<List<Contrato>>(JsonConvert.SerializeObject(table));
        }
        myReader.Close();
        myCon.Close();
      }
      JArray clientData = new JArray();
      foreach (Cliente cliente in clientes)
      {
        double total = 0;
        JObject client = new JObject();
        client.Add("Nombre", cliente.Nombre);
        foreach (DataRow row in table_contratos.Rows)
        {
          //client.Add(contrato.Nombre, contrato.monto);
          if ((int)row["clienteid"] == cliente.Id)
          {
            total += (double)row["monto"];
          }
        }
        client.Add("Total", total);
        clientData.Add(client);
      }

      string result = JsonConvert.SerializeObject(clientData);

      return Ok(result);
    }

  }
  
}