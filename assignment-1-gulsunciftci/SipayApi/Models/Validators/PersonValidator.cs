using FluentValidation;
using FluentValidation.Validators;
using System.ComponentModel.DataAnnotations;
namespace SipayApi.Models.Validators
{
    public class PersonValidator:AbstractValidator<Person> //Hangi sınıfın validator'ı olduğunu göstermek için o sınıfın AbstractValidator'ının inherit aldım 
    {
        //Fluent validation eklemeleri
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(5).MaximumLength(100).WithMessage("Name length can be at least 5 and up to 100")
                .WithName("staff person name");


            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be empty")
                .MinimumLength(5).MaximumLength(100).WithMessage("Lastname length can be at least 5 and up to 100")
                .WithName("staff person lastname");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone cannot be empty")
                .Matches(@"(0(\d{3}) (\d{3}) (\d{2}) (\d{2}))$").WithMessage("PhoneNumber not valid")
                .WithName("staff person phone number");


            RuleFor(x => x.AccessLevel).NotEmpty().WithMessage("AccessLevel cannot be empty")
                .InclusiveBetween(1, 5).WithMessage("AccessLevel should be between 1 and 5")
                .WithName("staff person access level to system");


            RuleFor(x => x.Salary).NotEmpty().WithMessage("Salary cannot be empty")
                 .Must((x,salary) => SalaryAccessLevel (x.AccessLevel, salary))
                 .InclusiveBetween(5000, 50000).WithMessage("Salary should be between 5000 and 50000")
                 .WithName("staff person salary");

         

        }

        public bool SalaryAccessLevel(int accesslevel, decimal salary)
        {
            bool message;
            switch (accesslevel)
            {
                case 1:
                    message = salary > 10000 ? false : true; //eğer salary accesslevel'ı 1 olan birisi için 10000 den büyük girilmişse hata verece
                    return message;
                case 2:
                    message = salary > 20000 ? false : true;
                    return message;
                case 3:
                    message = salary > 30000 ? false : true;
                    return message;
                case 4:
                    message = salary > 40000 ? false : true;
                    return message;
                default:
                    message = false;
                    break;

            }
            return message;
        }
    }
}
