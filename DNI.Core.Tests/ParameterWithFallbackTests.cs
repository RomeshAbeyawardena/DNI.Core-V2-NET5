using DNI.Core.Abstractions;
using DNI.Core.Shared.Contracts;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class ParameterWithFallbackTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void Invoke_should_trigger_fallback_callback_when_parameter_is_null()
        {
            int? testId = null;
            sut = ParameterWithFallback.Create(testId, "Cat");

            sut.Invoke(i => Assert.Fail("Actual value was {0}", i), m => Assert.Pass("Actual value was {0}", m));

        }

        [Test]
        public void Invoke_should_trigger_first_callback_when_parameter_is_not_null()
        {
            int? testId = 1;
            sut = ParameterWithFallback.Create(testId, "Cat");

            sut.Invoke(i => Assert.Pass("Actual value was {0}", i), m => Assert.Fail("Actual value was {0}", m));
         }

        [Test]
        public void Invoke_should_trigger_fallback_callback_when_parameter_is_null_and_return_fallback_value()
        {
            int? testId = null;
            sut = ParameterWithFallback.Create(testId, "Cat");

            var result = sut.Invoke(i => new ReturnValueSet<int, string>(i), 
                m => new ReturnValueSet<int, string>(m));

            Assert.IsFalse(result.Parameter.HasValue);
            Assert.AreEqual("Cat", result.FallbackParameter);
        }

        [Test]
        public void Invoke_should_trigger_first_callback_when_parameter_is_not_null_and_return_paramater_value()
        {
            int? testId = 1;
            sut = ParameterWithFallback.Create(testId, "Cat");

            var result = sut.Invoke(i => new ReturnValueSet<int, string>(i), 
                m => new ReturnValueSet<int, string>(m));

            Assert.True(result.Parameter.HasValue);
            Assert.AreEqual(1, result.Parameter);
         }

        [Test]
        public async Task InvokeAsync_should_trigger_fallback_callback_when_parameter_is_null_and_return_fallback_value()
        {
            int? testId = null;
            sut = ParameterWithFallback.Create(testId, "Cat");
            
            var result = await sut.Invoke(async(i) => await Task.FromResult(new ReturnValueSet<int, string>(i)), 
                async(m) => await Task.FromResult(new ReturnValueSet<int, string>(m)));

            Assert.IsFalse(result.Parameter.HasValue);
            Assert.AreEqual("Cat", result.FallbackParameter);
        }

        [Test]
        public async Task InvokeAsync_should_trigger_first_callback_when_parameter_is_not_null_and_return_paramater_value()
        {
            int? testId = 1;
            sut = ParameterWithFallback.Create(testId, "Cat");

            var result = await sut.Invoke(async(i) => await Task.FromResult(new ReturnValueSet<int, string>(i)), 
                async(m) => await Task.FromResult(new ReturnValueSet<int, string>(m)));

            Assert.True(result.Parameter.HasValue);
            Assert.AreEqual(1, result.Parameter);
         }

        private IParameterWithFallback<int, string> sut;

        private class ReturnValueSet<TParameter, TFallbackParameter>
            where TParameter : struct
        {
            public ReturnValueSet(TParameter parameter)
            {
                Parameter = parameter;
            }

            public ReturnValueSet(TFallbackParameter fallbackParameter)
            {
                FallbackParameter = fallbackParameter;
            }

            public TParameter? Parameter { get; }
            public TFallbackParameter FallbackParameter { get; }
        }
    }
}