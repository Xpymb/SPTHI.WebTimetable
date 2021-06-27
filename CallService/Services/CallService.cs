using CallService.Calls;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CallService
{
    public class CallService : CallControllerAPI.CallControllerAPIBase
    {
        private readonly ILogger<CallService> _logger;
        public CallService(ILogger<CallService> logger)
        {
            _logger = logger;
        }

        public override Task<CallReply> GetNextCall(Empty request, ServerCallContext context)
        {
            var nextCall = CallsManager.NextCall;

            return Task.FromResult(new CallReply
            {
                Name = nextCall.Name,
                DateTime = nextCall.CallTime.ToShortTimeString()
            });
        }

        public override async Task GetListCalls(Empty request,
            IServerStreamWriter<CallReply> responseStream, ServerCallContext context)
        {
            var listCalls = CallsManager.ListCalls;

            foreach (var call in listCalls)
            {
                await responseStream.WriteAsync(new CallReply
                {
                    Name = call.Name,
                    DateTime = call.CallTime.ToShortTimeString()
                });
            }
        }

        public override async Task GetListNextCalls(Empty requestStream,
            IServerStreamWriter<CallReply> responseStream, ServerCallContext context)
        {
            var listNextCalls = CallsManager.ListNextCalls;

            foreach (var call in listNextCalls)
            {
                await responseStream.WriteAsync(new CallReply
                {
                    Name = call.Name,
                    DateTime = call.CallTime.ToShortTimeString()
                });
            }
        }
    }
}
