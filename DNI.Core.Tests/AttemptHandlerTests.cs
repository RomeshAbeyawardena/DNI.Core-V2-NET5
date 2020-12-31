using DNI.Core.Shared.Contracts.Handlers;
using DNI.Core.Shared.Handlers;
using Moq;
using NUnit.Framework;
using System;

namespace DNI.Core.Tests
{
    public class AttemptHandlerTests
    {
        [SetUp]
        public void SetUp()
        {
            isCleanedUp = false;
            catchHandlerMock = new Mock<ICatchHandler>();

            catchHandlerMock
                .Setup(i => i.GetCatchAction())
                    .Returns(e => DoNothing())
                    .Verifiable();
            
            catchHandlerMock
                .Setup(i => i.Default(It.IsAny<Action<Exception>>()))
                .Returns(catchHandlerMock.Object);

            finallyHandlerMock = new Mock<IFinallyHandler>();

            testHandler = Handler.GetHandler(catchHandlerMock.Object, finallyHandlerMock.Object);
        }

        [Test]
        public void Attempt_fails_when_handled_exception_thrown()
        {
            var tryBlockCalled = false;

            catchHandlerMock
                .Setup(i => i.When<NotSupportedException>(It.IsAny<Action<Exception>>()))
                .Returns(catchHandlerMock.Object)
                .Verifiable();

            var handler = testHandler.Try(() => {
                tryBlockCalled = true;
                throw new NotSupportedException();
            }, @catch => @catch
                    .When<NotSupportedException>(ex => DoNothing())
                    .Default((ex) => DoNothing()), 
               @finally => Cleanup());

            var attempt = handler.AsAttempt();

            Assert.IsTrue(tryBlockCalled);
            catchHandlerMock
                .Verify(i => i.GetCatchAction());

            catchHandlerMock
                .Verify(i => i.When<NotSupportedException>(
                    It.IsAny<Action<Exception>>()));

            Assert.IsFalse(attempt.Successful);
        }

        [Test]
        public void Attempt_fails_when_unhandled_exception_thrown()
        {
            var tryBlockCalled = false;
            
            catchHandlerMock
                .Setup(i => i.When<NotSupportedException>(
                    It.IsAny<Action<Exception>>()))
                .Returns(catchHandlerMock.Object)
                .Verifiable();


            var handler = testHandler.Try(() => {
                tryBlockCalled = true;
                throw new InvalidOperationException();
            }, 
                @catch => @catch
                    .When<NotSupportedException>(ex => DoNothing())
                    .Default((ex) => DoNothing()), @finally => Cleanup());

            var attempt = handler.AsAttempt();

            Assert.IsTrue(tryBlockCalled);
            
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

        public void DoNothing()
        {

        }

        public void Cleanup()
        {
            isCleanedUp = true;
        }

        private Mock<ICatchHandler> catchHandlerMock;
        private Mock<IFinallyHandler> finallyHandlerMock;
        private bool isCleanedUp = false;
        private IHandler testHandler;
    }
}
