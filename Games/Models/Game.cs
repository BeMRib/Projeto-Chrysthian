namespace Games.Models
{
    public record Game{
        public Game(Guid Id, string name, string plataforma, string genero)
        {
            this.Id = Id;
            this.name = name;
            this.plataforma = plataforma;
            this.genero = genero;
        }

        public Guid Id { get; set; }
        public string name { get; set; }
        public string plataforma { get; set; }
        public string genero { get; set; }
        
    }
   
}
