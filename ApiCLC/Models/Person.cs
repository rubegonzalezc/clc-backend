using ApiCLC.Models;

public class Person
{
    public int Id { get; set; }
    public required string Rut { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required int Phone { get; set; }
    public int? TeamId { get; set; } // Permitir nulo
    // Clave foránea para Team
    public Team? Team { get; set; } // Permitir nulo
    public int? PositionId { get; set; } // Permitir nulo
    // Clave foránea para Position
    public Position? Position { get; set; } // Permitir nulo
}