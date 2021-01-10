using DNI.Core.Abstractions;
using NUnit.Framework;
using System;

namespace DNI.Core.Tests
{
    public class DefaultChangeTrackerTests
    {
        [SetUp]
        public void SetUp()
        {
            userChangeTracker = new DefaultChangeTracker<User>();
        }

        [Theory]
        public void HasChanges()
        {
            var originalUser = new User
            {
                Id = 1,
                EmailAddress = "john.doe@website.net",
                CustomerId = 12,
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = "Harrison",
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            var destinationUser = new User
            {
                Id = -1,
                EmailAddress = "john.doe@website.net",
                CustomerId = null,
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = "Harrison",
                DateOfBirth = new DateTime(1980, 12, 1)
            };

            Assert.IsTrue(userChangeTracker.HasChanges(originalUser, destinationUser, out var propertyChanges));

            userChangeTracker.MergeChanges(originalUser, destinationUser, propertyChanges);
            
            Assert.IsFalse(userChangeTracker.HasChanges(originalUser, destinationUser, out propertyChanges));

            originalUser = new User
            {
                Id = 1,
                EmailAddress = "john.doe@website.net",
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = "Harrison",
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            destinationUser = new User
            {
                Id = default,
                EmailAddress = "john.doe@website.net",
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = "Harrison",
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            Assert.IsFalse(userChangeTracker.HasChanges(originalUser, destinationUser, out propertyChanges));
        }

        private DefaultChangeTracker<User> userChangeTracker;

        internal class User
        {
            public int Id { get; set; }
            public int? CustomerId { get; set; }
            public string EmailAddress { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}
