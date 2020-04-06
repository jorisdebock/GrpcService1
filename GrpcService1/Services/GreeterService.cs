using Grpc.Core;
using MappingGenerator.OnBuildGenerator;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var hello = new Hello { Message = "test" };

            var response = new GreeterServiceMapper().MapHello(hello);

            return Task.FromResult(response);
        }
    }

    public class Hello
    {
        public string Message { get; set; }
    }

    [MappingInterface]
    public interface IGreeterServiceMapper
    {
        public HelloReply MapHello(Hello hello);
    }
}

namespace MappingGenerator.OnBuildGenerator
{
    [AttributeUsage(AttributeTargets.Interface)]
    [Conditional("CodeGeneration")]
    public class MappingInterface : Attribute
    {
    }
}