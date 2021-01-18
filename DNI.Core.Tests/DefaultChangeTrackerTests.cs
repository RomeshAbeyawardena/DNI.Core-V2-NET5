﻿using DNI.Core.Abstractions;
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

            var propertyChangesDictionary = propertyChanges.ToDictionary(a => a.Property.Name, a => a);

            Assert.False(propertyChangesDictionary["Id"].HasChanges);
            Assert.False(propertyChangesDictionary["EmailAddress"].HasChanges);
            Assert.True(propertyChangesDictionary["CustomerId"].HasChanges);
            Assert.False(propertyChangesDictionary["FirstName"].HasChanges);
            Assert.False(propertyChangesDictionary["MiddleName"].HasChanges);
            Assert.False(propertyChangesDictionary["LastName"].HasChanges);
            Assert.True(propertyChangesDictionary["DateOfBirth"].HasChanges);

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
            Assert.True(propertyChanges.All(a => !a.HasChanges));
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
                CustomerId = 392392,
                EmailAddress = "john.doe@website.net",
                FirstName = "John",
                MiddleName = "Middleton",
                LastName = null,
                DateOfBirth = new DateTime(1980, 11, 5)
            };

            Assert.IsTrue(userChangeTracker.HasChanges(originalUser, destinationUser, out var propertyChanges));

            var propertyChangesDictionary = propertyChanges.ToDictionary(a => a.Property.Name, a => a);

            Assert.False(propertyChangesDictionary["Id"].HasChanges);
            Assert.True(propertyChangesDictionary["Sid"].HasChanges);
            Assert.False(propertyChangesDictionary["EmailAddress"].HasChanges);
            Assert.True(propertyChangesDictionary["CustomerId"].HasChanges);
            Assert.True(propertyChangesDictionary["FirstName"].HasChanges);
            Assert.False(propertyChangesDictionary["MiddleName"].HasChanges);
            Assert.True(propertyChangesDictionary["LastName"].HasChanges);
            Assert.False(propertyChangesDictionary["DateOfBirth"].HasChanges);


            Assert.IsTrue(propertyChanges.Any(pc => pc.HasChanges));
        }

        private DefaultChangeTracker<User> userChangeTracker;
    }
}
