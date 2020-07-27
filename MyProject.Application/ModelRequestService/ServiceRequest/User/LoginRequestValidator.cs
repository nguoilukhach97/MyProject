using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tên đăng nhập không được để trống !");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống !")
                .MinimumLength(6).WithMessage("Mật khẩu phải lớn hơn 6 ký tự !");
        }
    }
}
