namespace AutoSynchClientEngine.Classes
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int ReturnId { get; set; }
        public List<UpdatedRecords> updatedRecords { get; set; }
        public ApiResponse()
        {
            Code = Message = String.Empty;
            updatedRecords = new List<UpdatedRecords>();
        }
    }
    public class UpdatedRecords
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public UpdatedRecords()
        {
            Id = InvoiceNo = 0;
        }
    }
}
