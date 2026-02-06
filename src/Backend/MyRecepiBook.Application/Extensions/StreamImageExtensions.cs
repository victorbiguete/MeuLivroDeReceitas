using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.Extensions
{
    public static class StreamImageExtensions
    {
        public static (bool isValidImage, string extensions) ValidateAndGetImageExtension(this Stream stream)
        {
            var result = (false,string.Empty);

            if(stream.Is<PortableNetworkGraphic>())
                result = (true,NormalizeExtension(PortableNetworkGraphic.TypeExtension));
            else if(stream.Is<JointPhotographicExpertsGroup>())
                result = (true, NormalizeExtension(JointPhotographicExpertsGroup.TypeExtension));

            stream.Position = 0;

            return result;
        }

        private static string NormalizeExtension(string extension)
        {
            return extension.StartsWith('.') ? extension : $".{extension}";
        }
    }
}
