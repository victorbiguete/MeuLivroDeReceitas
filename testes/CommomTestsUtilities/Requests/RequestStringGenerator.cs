using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestsUtilities.Requests
{
    public class RequestStringGenerator
    {
        public static string Paragraphs(int minCharacters)
        {
            var faker = new Faker();

            var longText = faker.Lorem.Paragraph(min: 7);

            while(longText.Length < minCharacters)
            {
                longText = $"{longText} {faker.Lorem.Paragraph()}";
            }

            return longText;
        }
    }
}
