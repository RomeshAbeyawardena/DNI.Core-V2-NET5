using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Core.Tests
{
    public class SearchCriteriaExtensionsTests
    {
        [Test]
        public void GetSearchParameters()
        {
            var expression = new StudentViewModel { Parameters = new Student { Name = "John" } }.GetSearchParameters();

            Assert.AreEqual(1, expression.Count());

            expression = new StudentViewModel { Parameters = new Student { Id = 1, Name = "John" } }.GetSearchParameters();

            Assert.AreEqual(2, expression.Count());
        }

        [Test] 
        public void GetExpression()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "John", Score = 120 },
                new Student { Id = 2, Name = "Harris" },
                new Student { Id = 3, Name = "Jane", Score = 90 }
            };

            var expression = new StudentViewModel { Parameters = new Student { Name = "John" } }.GetExpression();

            var foundStudents = students.Where(expression.Compile());

            Assert.AreEqual(1, foundStudents.Count());
            Assert.Contains(students[0], foundStudents.ToArray());
            expression = new StudentViewModel { Parameters = new Student {  Name = "Harris" } }.GetExpression();

            foundStudents = students.Where(expression.Compile());
            
            Assert.AreEqual(1, foundStudents.Count());
            Assert.Contains(students[1], foundStudents.ToArray());

            expression = new StudentViewModel { Parameters = new Student { Id = 1,  Name = "Harris" } }.GetExpression();

            foundStudents = students.Where(expression.Compile());
            
            Assert.AreEqual(2, foundStudents.Count());
            Assert.Contains(students[0], foundStudents.ToArray());
            Assert.Contains(students[1], foundStudents.ToArray());

            expression = new StudentViewModel { Parameters = new Student { Id = 3,  Name = "Harrison", Score = 90 } }.GetExpression();

            foundStudents = students.Where(expression.Compile());
            
            Assert.AreEqual(2, foundStudents.Count());
        }

        private class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? Score { get; set; }
        }

        private class StudentViewModel : ISearchCriteria<Student>
        {
            public Student Parameters { get; set; }
            public int TotalItemsPerPage { get; set; }
            public int PageIndex { get; set; }
        }
    }
}
