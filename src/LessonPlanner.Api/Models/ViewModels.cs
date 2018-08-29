using System;
using System.Linq;
using System.Collections.Generic;

namespace M7.LessonPlanner.Api.Models {

    public class ActivityModel {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get;set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class ScheduleModel {

        public string Name { get; set; }
        public ActivityModel[] Activities { get; set; }
    }
}