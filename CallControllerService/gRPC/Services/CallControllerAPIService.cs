using System;
using System.Threading.Tasks;
using CallController;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CallControllerService.gRPC.Services
{
    public class CallControllerAPIService : CallControllerAPI.CallControllerAPIBase
    {
        private readonly ILogger<CallControllerAPIService> _logger;

        public CallControllerAPIService(ILogger<CallControllerAPIService> logger)
        {
            _logger = logger;
        }

        public override Task<CallReply> GetNextCall(Empty request, ServerCallContext context)
        {
            var nextCall = InstancesContainer._CallsManager.NextCall;

            return Task.FromResult(new CallReply
            {
                Name = nextCall.Name,
                DateTime = nextCall.GetShortTime()
            });
        }

        public override async Task GetListCalls(Empty request,
            IServerStreamWriter<CallReply> responseStream, ServerCallContext context)
        {
            var listCalls = InstancesContainer._CallsManager.ListCalls;

            foreach (var call in listCalls)
            {
                await responseStream.WriteAsync(new CallReply
                {
                    Name = call.Name,
                    DateTime = call.GetShortTime()
                });
            }
        }

        public override async Task GetListNextCalls(Empty requestStream,
            IServerStreamWriter<CallReply> responseStream, ServerCallContext context)
        {
            var listNextCalls = InstancesContainer._CallsManager.ListNextCalls;

            foreach (var call in listNextCalls)
            {
                await responseStream.WriteAsync(new CallReply
                {
                    Name = call.Name,
                    DateTime = call.GetShortTime()
                });
            }
        }
    }
}