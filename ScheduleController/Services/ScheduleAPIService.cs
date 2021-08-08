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

        public override async Task GetGroupType(Empty request, IServerStreamWriter<GroupsTypeReply> responseStream, ServerCallContext context)
        {
            var groupsType = ScheduleManager.GetGroupTypes();

            foreach (var groupType in groupsType)
            {
                await responseStream.WriteAsync(new GroupsTypeReply
                {
                    GroupType = groupType,
                });
            }
        }

        public override async Task GetClasses(ClassesRequest request, IServerStreamWriter<ClassesReply> responseStream, ServerCallContext context)
        {
            var classes = ScheduleManager.GetClasses(request.GroupType);

            foreach (var _class in classes)
            {
                await responseStream.WriteAsync(new ClassesReply
                {
                    Class = _class
                });
            }
        }

        public override async Task GetWeeksType(WeeksTypeRequest request, IServerStreamWriter<WeeksTypeReply> responseStream, ServerCallContext context)
        {
            var weeksType = ScheduleManager.GetWeeksType(request.GroupName, request.GroupType);

            foreach (var weekType in weeksType)
            {
                await responseStream.WriteAsync(new WeeksTypeReply
                {
                    WeeksType = weekType,
                });
            }
        }

        public override async Task GetScheduleByGroupName(ScheduleRequest request,
            IServerStreamWriter<ScheduleReply> responseStream, ServerCallContext context)
        {
            var lessons = ScheduleManager.GetSchedule(request.GroupName, DateTime.ParseExact(request.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture), request.WeekType);

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

        public override async Task GetGroupsName(GroupsNameRequest request,
            IServerStreamWriter<GroupsNameReply> responseStream, ServerCallContext context)
        {
            var groupsName = ScheduleManager.GetGroupsName(request.Class, request.GroupType);

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
            var lessonsDates = ScheduleManager.GetDatesLessons(request.GroupName, request.GroupType, request.WeeksType);

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
