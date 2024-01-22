using RainfallAPI.Models.ViewModels;

namespace RainfallAPI.Utils
{
    public static class ErrorHelper
    {
        public static List<ErrorDetail> ExtractErrorDetails(System.Collections.IDictionary data)
        {
            var errorDetails = new List<ErrorDetail>();

            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    errorDetails.Add(new ErrorDetail
                    {
                        PropertyName = key?.ToString(),
                        Message = data[key]?.ToString()
                    });
                }
            }
            return errorDetails;
        }
    }
}
