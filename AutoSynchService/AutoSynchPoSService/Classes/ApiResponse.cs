namespace AutoSynchPosService.Classes
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ApiResponse()
        {
            Code = Message = String.Empty;
        }
    }
}
