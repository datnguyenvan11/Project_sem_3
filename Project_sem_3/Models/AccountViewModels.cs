using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_sem_3.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string UserName { get;set; }

        public enum OrderStatus { Done = 1, Cancel = 0, Deleted = -1 }
        public ExternalLoginConfirmationViewModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            Status = (int)OrderStatus.Done;
        }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]

        public string UserName { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public string Gender { get; set; }
        [Required]

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public RegisterViewModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
         
            Status = 1;
        }
    }
    public class Users_in_Role_ViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; }
        public string Role { get; set; }
    }
    public class CreateAdminModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public string RoleName { get; set; }
        public enum OrderStatus { Done = 1, Cancel = 0, Deleted = -1 }
        public CreateAdminModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            Status = (int)OrderStatus.Done;
        }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }     
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
        public class RoleViewModel
        {
            public RoleViewModel()
            {

            }
            public RoleViewModel(ApplicationRole role)
            {
            Id = role.Id;
            Name = role.Name;
           

            }
        public string Id { get; set; }
        public string Name { get; set; }
   

    }
    public class ViewOrderMotor
    {
        [Required]
        public int insurancePackageId { get; set; }
        [Required]

        public int quantity { get; set; }
        [Required]

        public string CarOwner { get; set; }
        [Required]

        public string RegisteredAddress { get; set; }
        [Required]

        public string LicensePlate { get; set; }
        [Required]

        public string ChassisNumber { get; set; }
        [Required]
        public DateTime Duration { get; set; }
    }
    public class ContractMedical{
        public int Totalprice { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string InsurancePackageId { get; set; }
        public int unitprice { get; set; }
        public string Programmeid { get; set; }
        public string DateOfbirth { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
