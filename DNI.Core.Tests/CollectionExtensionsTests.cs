using DNI.Core.Shared.Extensions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void TryAddOrCreate()
        {
            Assert.IsNull(namesList);

            Shared.Extensions.CollectionExtensions.TryAddOrCreate(ref namesList, "Tom");

            Assert.IsNotNull(namesList);

            Assert.Contains("Tom", namesList as ICollection);
        }

        private ICollection<string> namesList;
    }
}
