namespace UDPCommunication.Models
{
    /// <summary>
    /// Describes any operation result in all application. Generic type T defines the return type
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        public void SetFailureMode(string message)
        {
            Success = false;
            Message = message;
        }
    }
}