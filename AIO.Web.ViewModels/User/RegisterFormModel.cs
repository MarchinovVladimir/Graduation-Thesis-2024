using System.ComponentModel.DataAnnotations;
using static AIOCommon.EntityValidationConstants.User;
using static AIOCommon.ErrorMessageConstants.RegisterFormModel;

namespace AIO.Web.ViewModels.User
{
	/// <summary>
	/// Register form model for user registration.
	/// </summary>
	public class RegisterFormModel
    {
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [StringLength(PasswordMaxLength,
                      MinimumLength = PasswordMinLength, 
                      ErrorMessage = PasswordLengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = PasswordConfirmationErrorMessage)]
        public string ConfirmPassword { get; set; } = null!;

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required]
        [StringLength(FirstNameMaxLength, 
                      MinimumLength = FirstNameMinLength,
                      ErrorMessage = FirstNameLengthErrorMessage)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Required]
        [StringLength(LastNameMaxLength, 
                      MinimumLength = LastNameMinLength,
                      ErrorMessage = LastNameLengthErrorMessage)]
        public string LastName { get; set; } = null!;
    }
}
