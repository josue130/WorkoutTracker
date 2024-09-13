namespace WorkoutApi.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; } = true;
        public object? Result { get; set; }
        public string Message { get; set; } = "";
    }
}
