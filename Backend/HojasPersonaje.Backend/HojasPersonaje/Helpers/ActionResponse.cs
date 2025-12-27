namespace HojasPersonaje.Helpers
{
    public class ActionResponse<T>
    {
        public bool Exitoso { get; set; }
        public string? Mensaje { get; set; }
        public T? Resultado { get; set; }
    }
}
