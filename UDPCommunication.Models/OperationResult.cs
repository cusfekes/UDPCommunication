namespace UDPCommunication.Models
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }

        public void SetSuccessMode() 
        { 
            Success = true;
            Message = string.Empty;
        }

        public void SetSuccessMode(T result)
        {
            SetSuccessMode();
            Result = result;
        }

        public void SetFailureMode(Exception ex)
        {
            Success = false;
            Message = ex.Message;
        }
    }
}