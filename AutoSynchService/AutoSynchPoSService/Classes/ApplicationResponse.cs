namespace AutoSynchPosService.Classes
{
    public class ApplicationResponse
    {
        public const string SUCCESS_CODE = "100";
        public const string SUCCESS_MESSAGE = "successful";

        public const string EMPTY_TRACKING_ID_CODE = "101";
        public const string EMPTY_TRACKING_ID_MESSAGE = "id not provided";

        public const string NOT_EXISTS_CODE = "104";
        public const string NOT_EXISTS_MESSAGE = "id not exist";

        public const string MAX_REACHED_CODE = "105";
        public const string MAX_REACHED_MESSAGE = "latest record already downloaded";

        public const string GENERIC_ERROR_CODE = "201";
        public const string GENERIC_ERROR_MESSAGE = "error occurred on server please try again later ";
    }
}
