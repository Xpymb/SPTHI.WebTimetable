using Grpc.Core;
using Grpc.Net.Client;
using ScheduleController;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VkApiBot.gRPC.Services
{
    public static class ScheduleServiceAPI
    {
        private static ScheduleAPI.ScheduleAPIClient _client;

        public static void ConnectToService(string url)
        {
            var channel = GrpcChannel.ForAddress(url);

            _client = new ScheduleAPI.ScheduleAPIClient(channel);
        }

        public static async Task<List<ScheduleReply>> GetScheduleByGroupName(string groupName, string date)
        {
            try
            {
                var scheduleReply = new List<ScheduleReply>();
                
                var reply = _client.GetScheduleByGroupName(new ScheduleRequest
                {
                    GroupName = groupName,
                    Date = date,
                });

                await foreach (var response in reply.ResponseStream.ReadAllAsync())
                {
                    scheduleReply.Add(response);
                }

                return scheduleReply;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<GroupsNameReply>> GetGroupsName()
        {
            try
            {
                var groupsName = new List<GroupsNameReply>();

                var reply = _client.GetGroupsName(new Google.Protobuf.WellKnownTypes.Empty());

                await foreach(var response in reply.ResponseStream.ReadAllAsync())
                {
                    groupsName.Add(response);
                }

                return groupsName;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<DateScheduleReply>> GetDatesSchedulesByGroup(string groupName)
        {
            try
            {
                var dates = new List<DateScheduleReply>();

                var reply = _client.GetDateScheduleByGroupName(new DateScheduleRequest
                {
                    GroupName = groupName,
                });

                await foreach(var response in reply.ResponseStream.ReadAllAsync())
                {
                    dates.Add(response);
                }

                return dates;
            }
            catch
            {
                return null;
            }
        }
    }
}
