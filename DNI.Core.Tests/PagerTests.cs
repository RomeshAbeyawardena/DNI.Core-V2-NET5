using DNI.Core.Shared;
using DNI.Core.Tests.Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Tests.Extensions;

namespace DNI.Core.Tests
{
    public class PagerTests
    {
        [Test] public void ToArrayAsync()
        {
            var students = new List<Student>(new Student[] {
                new Student { Id = 1 },
                new Student { Id = 2 },
                new Student { Id = 3 },
                new Student { Id = 4 },
                new Student { Id = 5 },
                new Student { Id = 6 },
                new Student { Id = 7 },
                new Student { Id = 8 },
                new Student { Id = 9 },
                new Student { Id = 10 },
                new Student { Id = 11 },
                new Student { Id = 12 },
                new Student { Id = 13 },
                new Student { Id = 14 },
                new Student { Id = 15 },
                new Student { Id = 16 },
                new Student { Id = 17 },
                new Student { Id = 18 },
                new Student { Id = 19 },
                new Student { Id = 20 },
                new Student { Id = 21 },
                new Student { Id = 22 },
                new Student { Id = 23 },
                new Student { Id = 24 },
                new Student { Id = 25 },
                new Student { Id = 26 },
                new Student { Id = 27 },
                new Student { Id = 28 },
                new Student { Id = 29 },
                new Student { Id = 30 },
                new Student { Id = 31 },
                new Student { Id = 32 },
                new Student { Id = 33 },
                new Student { Id = 34 },
                new Student { Id = 35 },
                new Student { Id = 36 },
                new Student { Id = 37 },
                new Student { Id = 38 },
                new Student { Id = 39 },
                new Student { Id = 40 },
                new Student { Id = 41 },
                new Student { Id = 42 },
                new Student { Id = 43 },
                new Student { Id = 44 },
                new Student { Id = 45 },
                new Student { Id = 46 },
                new Student { Id = 47 },
                new Student { Id = 48 },
            });

            var studentQuery = new TestEnumerableQuery<Student>(students);
            sut = new Pager<Student>(studentQuery);
            
            var items = sut.GetPagedItems(1, 10);

            Assert.AreEqual(10, items.Count());
            AssertExtensions.StartsWith(items, new Student { Id = 1 });
            AssertExtensions.EndsWith(items, new Student { Id = 10 });
            
            items = sut.GetPagedItems(2, 10);
            Assert.AreEqual(10, items.Count());
            AssertExtensions.StartsWith(items, new Student { Id = 11 });
            AssertExtensions.EndsWith(items, new Student { Id = 20 });

            items = sut.GetPagedItems(3, 10);
            Assert.AreEqual(10, items.Count());

            AssertExtensions.StartsWith(items, new Student { Id = 21 });
            AssertExtensions.EndsWith(items, new Student { Id = 30 });

            items = sut.GetPagedItems(4, 10);
            Assert.AreEqual(10, items.Count());
            
            AssertExtensions.StartsWith(items, new Student { Id = 31 });
            AssertExtensions.EndsWith(items, new Student { Id = 40 });

            items = sut.GetPagedItems(5, 10);
            Assert.AreEqual(8, items.Count());
            
            AssertExtensions.StartsWith(items, new Student { Id = 41 });
            AssertExtensions.EndsWith(items, new Student { Id = 48 });

        }

        private Pager<Student> sut;

        class TestEnumerableQuery<T> : EnumerableQuery<T>
        {
            public TestEnumerableQuery(IEnumerable<T> items)
                : base(items)
            {

            }
        }
    }
}
