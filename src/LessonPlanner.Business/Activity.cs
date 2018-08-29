using System;

namespace M7.LessonPlanner.Business
{
    public class Activity
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public Activity( int ID, string Name, string Description, 
            TimeSpan StartTime, TimeSpan EndTime ) {

            this.ID = ID;
            ChangeActivity( Name, Description, StartTime, EndTime );            
        }

        public void ChangeActivity( string Name, string Description, TimeSpan StartTime, TimeSpan EndTime ) {

            SetTimeSpan( StartTime, EndTime );
            this.Name = Name ?? "Untitled";
            this.Description = Description ?? "No Description";
        }

        private void SetTimeSpan( TimeSpan Start, TimeSpan End ) {

            if( Start >= End ) throw new Exception( "In correct time span" );
            this.StartTime = Start;
            this.EndTime = End;
        }
    }
}

