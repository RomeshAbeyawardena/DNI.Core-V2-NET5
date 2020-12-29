using DNI.Core.Shared.Contracts.Handlers;
using DNI.Core.Shared.Handlers;
using NUnit.Framework;
using System;

namespace DNI.Core.Tests
{
    public class AttemptHandlerTests
    {
        [SetUp]
        public void SetUp()
        {
            testHandler = Handler.Default;
        }

        [Test]
        public void Attempt_Failed()
        {
            var test1 = false;
            var test2 = false;
            var test3 = false;

            var handler = testHandler.Try(() => {
                test1 = true;
                throw new NotSupportedException();
            }, 
                @catch => @catch
                    .When<NotSupportedException>(ex => test2 = true)
                    .Default((ex) => test3 = true), @finally => Cleanup());

            var attempt = handler.AsAttempt();

            Assert.IsTrue(test1);
            Assert.IsTrue(test2);
            Assert.IsFalse(test3);
            Assert.IsTrue(isCleanedUp);
            Assert.IsFalse(attempt.Successful);
        }

        [Test]
        public void Attempt_Passed()
        {
            var test1 = false;
            var test2 = false;
            var test3 = false;

            var handler = testHandler.Try(() => {
                test1 = true;
            }, 
                @catch => @catch
                    .When<NotSupportedException>(ex => test2 = true)
                    .Default((ex) => test3 = true), @finally => Cleanup());

            var attempt = handler.AsAttempt();

            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
            Assert.IsFalse(test3);
            Assert.IsTrue(isCleanedUp);
            Assert.IsTrue(attempt.Successful);
        }

        public void Cleanup()
        {
            isCleanedUp = true;
        }

        bool isCleanedUp = false;
        IHandler testHandler;
    }
}
