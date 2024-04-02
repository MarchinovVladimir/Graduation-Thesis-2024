using System.ComponentModel.DataAnnotations;

namespace AIO.Web.ViewModels.User
{
    /// <summary>
    /// Login form model for user login.
    /// </summary>
    public class LoginFormModel
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to be remembered.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } 

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        public string? ReturnUrl { get; set; } 
    }
}
