using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest.User
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirtsName).NotEmpty().WithMessage("First Name không được để trống !")
                .MaximumLength(200).WithMessage("Tên quá dài > 200 ký tự !");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name không được để trống !")
                .MaximumLength(200).WithMessage("Tên quá dài > 200 ký tự !");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("Tuổi của bạn quá cao >120 tuổi !");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống !")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email sai định dạng !");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Điện thoại không được để trống");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username không được để trống !");
            
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password không được để trống !")
                .MinimumLength(6).WithMessage("Mật khẩu quá ngắn !");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Password không trùng !");
                }
            });

        }
    }
}
