using Grpc.Core;
using Grpc.Net.Client;
using CallControllerService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace VkApiBot.gRPC.Services
{
    public static class CallControllerServiceAPI
    {
        private static CallControllerAPI.CallControllerAPIClient _client;

        public static void ConnectToService(string url)
        {
            var channel = GrpcChannel.ForAddress(url);

            _client = new CallControllerAPI.CallControllerAPIClient(channel);
        }

        public static CallReply GetNextCall()
        {
            return _client.GetNextCall(new Empty());
        }

        public static async Task<List<CallReply>> GetListCalls()
        {
            var reply = _client.GetListCalls(new Empty());

            var listCalls = new List<CallReply>();

            await foreach(var response in reply.ResponseStream.ReadAllAsync())
            {
                listCalls.Add(response);
            }

            return listCalls;
        }

        public static async Task<List<CallReply>> GetListNextCalls()
        {
            var reply = _client.GetListNextCalls(new Empty());

            var listCalls = new List<CallReply>();

            await foreach (var response in reply.ResponseStream.ReadAllAsync())
            {
                listCalls.Add(response);
            }

            return listCalls;
        }
    }
}
