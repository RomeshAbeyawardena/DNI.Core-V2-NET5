﻿using DNI.Core.Abstractions.Factories;
using DNI.Core.Abstractions.Managers;
using DNI.Core.Tests.Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class ExceptionResourceFactoryTests
    {
        
        [Test] public void GetException()
        {
            var expected = "Null reference exception";
            resourceManager = new DefaultResourceManager();

            resourceManager.AddExceptionErrorMessage<NullReferenceException>(expected);
            exceptionResourceFactory = new ExceptionResourceFactory(resourceManager);
            var exception = exceptionResourceFactory.GetException<NullReferenceException>(false);

            Assert.IsNotNull(exception);
            Assert.AreEqual(expected, exception.Message);
        }

        [Test] public void GetEntityException()
        {
            var expected = "Null Student reference exception";;
            resourceManager = new DefaultResourceManager();
            exceptionResourceFactory = new ExceptionResourceFactory(resourceManager);
            
            resourceManager.AddExceptionErrorMessage<NullReferenceException>("Null [type] reference exception");
            var exception = exceptionResourceFactory.GetException<Student, NullReferenceException>(false);

            Assert.IsNotNull(exception);
            Assert.AreEqual(expected, exception.Message);
        }

        [Test] public void GetEntityException_with_camel_case()
        {
            var expected = "Null Student Type reference exception";;
            resourceManager = new DefaultResourceManager();
            exceptionResourceFactory = new ExceptionResourceFactory(resourceManager);
            
            resourceManager.AddExceptionErrorMessage<NullReferenceException>("Null [type] reference exception");
            var exception = exceptionResourceFactory.GetException<StudentType, NullReferenceException>(false);

            Assert.IsNotNull(exception);
            Assert.AreEqual(expected, exception.Message);
        }
        
        private DefaultResourceManager resourceManager;
        private ExceptionResourceFactory exceptionResourceFactory;
    }
}