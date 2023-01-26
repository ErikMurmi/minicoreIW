using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Text.Json.Serialization;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ContratosController : ControllerBase
  {

    private readonly IConfiguration _configuration;

    public ContratosController(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    [HttpGet(Name = "GetContratos")]
    public string Get()
    {
      string query = "select * from contratos";
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
  }
}