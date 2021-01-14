using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void StartsWith<T>(IEnumerable<T> items, T expectedItem)
        {
            var firstItem = items.FirstOrDefault();

            if(firstItem == null)
            {
                Assert.Fail();
                return;
            }

            Assert.AreEqual(expectedItem, firstItem);
        }

        public static void EndsWith<T>(IEnumerable<T> items, T expectedItem)
        {
            var firstItem = items.LastOrDefault();

            if(firstItem == null)
            {
                Assert.Fail();
                return;
            }

            Assert.AreEqual(expectedItem, firstItem);
        }
    }
}
