namespace AIO.Web.ViewModels.Home
{
    /// <summary>
    /// View model for the error page.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request id of the error view model.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request id is shown.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}