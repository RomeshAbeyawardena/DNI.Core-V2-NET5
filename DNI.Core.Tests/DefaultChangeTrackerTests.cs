using DNI.Core.Abstractions;
using DNI.Core.Abstractions.Defaults;
using DNI.Core.Tests.Assets;
using NUnit.Framework;
using System;
using System.Linq;

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

        [Test]
        public void HasChanges_with_null_value()
        {
            var originalUser = new User
            {
                Id = 1,
                Sid = 238239382,
                EmailAddress = "john.doe@website.net",
                FirstName = null,
                MiddleName = "Middleton",
                LastName = "Harrison",
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            var destinationUser = new User
            {
                Id = default,
                Sid = default,
                EmailAddress = "john.doe@website.net",
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = null,
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            Assert.IsTrue(userChangeTracker.HasChanges(originalUser, destinationUser, out var propertyChanges));

            Assert.IsTrue(propertyChanges.Any(pc => pc.HasChanges));
        }

        private DefaultChangeTracker<User> userChangeTracker;
    }
}
