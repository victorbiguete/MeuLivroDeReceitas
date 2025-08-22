using Bogus;
using CommomTestsUtilities.Cryptography;
using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Entities
{
    public class UserBuilder
    {
        public static (User user, string password) Build()
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var password = new Faker().Internet.Password();

            var user = new Faker<User>()
                .RuleFor(user => user.Id,() => 1)
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f,user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password));

            return (user, password);
        }
    }
}
