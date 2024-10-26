namespace ApiCLC.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string Rut { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required int Phone { get; set; }
        public int TeamId { get; set; } // Clave foránea para Team
        public Team Team { get; set; }
        public int PositionId { get; set; } // Clave foránea para Position
        public Position Position { get; set; }
    }
}
