using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ScheduleController.Schedule;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ScheduleController
{
    public class ScheduleAPIService : ScheduleAPI.ScheduleAPIBase
    {
        private readonly ILogger<ScheduleAPIService> _logger;
        public ScheduleAPIService(ILogger<ScheduleAPIService> logger)
        {
            _logger = logger;
        }

        public override async Task GetScheduleByGroupName(ScheduleRequest request,
            IServerStreamWriter<ScheduleReply> responseStream, ServerCallContext context)
        {
            var lessons = ScheduleManager.GetScheduleByGroupName(request.GroupName, DateTime.ParseExact(request.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture));

            foreach (var lesson in lessons)
            {
                await responseStream.WriteAsync(new ScheduleReply
                {
                    Time = lesson.DateTime.ToString("HH:mm"),
                    Type = lesson.Type,
                    Name = lesson.Name,
                    Classroom = lesson.Classroom,
                    TeacherName = lesson.TeacherName,
                    GroupName = lesson.GroupName,
                    Class = lesson.Class,
                });
            }
        }

        public override async Task GetGroupsName(Empty request,
            IServerStreamWriter<GroupsNameReply> responseStream, ServerCallContext context)
        {
            var groupsName = ScheduleManager.GetGroupsName();

            foreach (var groupName in groupsName)
            {
                await responseStream.WriteAsync(new GroupsNameReply
                {
                    GroupName = groupName,
                });
            }
        }

        public override async Task GetDateScheduleByGroupName(DateScheduleRequest request,
            IServerStreamWriter<DateScheduleReply> responseStream, ServerCallContext context)
        {
            var lessonsDates = ScheduleManager.GetDatesLessonsByGroup(request.GroupName);

            foreach (var lessonDate in lessonsDates)
            {
                await responseStream.WriteAsync(new DateScheduleReply
                {
                    GroupName = request.GroupName,
                    Date = lessonDate.ToString("dd.MM.yyyy"),
                });
            }
        }
    }
}
