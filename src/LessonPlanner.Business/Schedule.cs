using System;
using System.Linq;
using System.Collections.Generic;

namespace M7.LessonPlanner.Business
{
    public class Schedule
    {
        public string Name { get; private set; }
        public List<Activity> Activities { get; private set; }

        public Schedule( string Name ) {
            SetName( Name );
            Activities = new List<Activity>();
        }

        public void LoadSchedule( string Name, List<Activity> Activities ) {
            SetName( Name );
            this.Activities = Activities ?? throw new Exception( "Activities are invalid (null)" );
        }

        public void SetName( string Name ) {
            if( string.IsNullOrWhiteSpace( Name ) ) Name = "Untitled";
            this.Name = Name;
        }

        public void Clear() {
            Activities.Clear();
        }

        private int GetActivityID() {

            if( Activities.Count > 0 )
                return Activities.Max( x => x.ID ) + 1;
            else
                return 1;
        }

        public void AddActivity( string Name, string Description, 
            TimeSpan StartTime, TimeSpan EndTime ) {

            Activities.Add( new Activity( GetActivityID(), Name, Description, StartTime, EndTime ) );
        }

        public void UpdateAcitivity( int ID, string Name, string Description, 
            TimeSpan StartTime, TimeSpan EndTime ) {

            var a = Activities.SingleOrDefault( x => x.ID == ID ) ?? throw new Exception( "Activity not found" );
            int i = Activities.IndexOf( a );

            Activities[i].ChangeActivity( Name, Description, StartTime, EndTime );
        } 

        public void RemoveActivity( int ID ) {
            var a = Activities.SingleOrDefault( x => x.ID == ID ) ?? throw new Exception( "Activity not found" );
            Activities.Remove( a );
        }
    }
}
