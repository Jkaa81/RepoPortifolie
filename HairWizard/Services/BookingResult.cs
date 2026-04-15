namespace HairWizard.Services
{
    public class BookingResult
    {
        public bool IsSuccessful { get; }
        public string? Key { get; }
        public string ErrorMessage { get; } = string.Empty;


        private BookingResult(bool isSuccessful, string? key = null, string? errorMessage = null)
        {
            IsSuccessful = isSuccessful;
            Key = key;
            ErrorMessage = errorMessage ?? string.Empty;
        }

        public static BookingResult Ok() => new BookingResult(true);

        public static BookingResult Fail(string key, string message)
            => new BookingResult(false, key, message);


    }
}

