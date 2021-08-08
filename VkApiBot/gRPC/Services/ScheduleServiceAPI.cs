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

        public static async Task<List<ScheduleReply>> GetScheduleByGroupName(string groupName, string date, string weekType)
        {
            try
            {
                var scheduleReply = new List<ScheduleReply>();
                
                using var reply = _client.GetScheduleByGroupName(new ScheduleRequest
                {
                    GroupName = groupName,
                    Date = date,
                    WeekType = weekType,
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

        public static async Task<List<GroupsNameReply>> GetGroupsName(string groupType, string _class)
        {
            try
            {
                var groupsName = new List<GroupsNameReply>();

                using var reply = _client.GetGroupsName(new GroupsNameRequest
                {
                    GroupType = groupType,
                    Class = _class,
                });

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

        public static async Task<List<DateScheduleReply>> GetDatesSchedulesByGroup(string groupName, string groupeType, string _class, string weeksType)
        {
            try
            {
                var dates = new List<DateScheduleReply>();

                using var reply = _client.GetDateScheduleByGroupName(new DateScheduleRequest
                {
                    GroupName = groupName,
                    Class = _class,
                    GroupType = groupeType,
                    WeeksType = weeksType,
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

        public static async Task<List<GroupsTypeReply>> GetGroupType()
        {
            try
            {
                var groupsTypeList = new List<GroupsTypeReply>();

                using var reply = _client.GetGroupType(new Google.Protobuf.WellKnownTypes.Empty());

                await foreach (var response in reply.ResponseStream.ReadAllAsync())
                {
                    groupsTypeList.Add(response);
                }

                return groupsTypeList;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<ClassesReply>> GetClasses(string groupType)
        {
            try
            {
                var classesList = new List<ClassesReply>();

                using var reply = _client.GetClasses(new ClassesRequest()
                {
                    GroupType = groupType,
                });

                await foreach (var response in reply.ResponseStream.ReadAllAsync())
                {
                    classesList.Add(response);
                }

                return classesList;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<WeeksTypeReply>> GetWeeksType(string groupType, string _classes, string groupName)
        {
            try
            {
                var classesList = new List<WeeksTypeReply>();

                using var reply = _client.GetWeeksType(new WeeksTypeRequest()
                {
                    GroupType = groupType,
                    Class = _classes,
                    GroupName = groupName,
                });

                await foreach (var response in reply.ResponseStream.ReadAllAsync())
                {
                    classesList.Add(response);
                }

                return classesList;
            }
            catch
            {
                return null;
            }
        }
    }
}
