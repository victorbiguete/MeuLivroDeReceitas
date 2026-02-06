using CommomTestsUtilities.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCases.Test.Recipe.InlineData
{
    public class ImageTypeInlineData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            var images = FormFileBuilder.ImageCollection();
            foreach (var image in images)
                yield return new object[] { image };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
