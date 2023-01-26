namespace api.Models
{
  public class Contrato
  {
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string? Nombre { get; set; }
    public float monto { get; set; }
    public DateTime Fecha { get; set; }
  }
}
