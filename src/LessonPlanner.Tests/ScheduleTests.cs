using Microsoft.VisualStudio.TestTools.UnitTesting;
using M7.LessonPlanner.Business;
using System;
using System.Linq;
using System.Collections.Generic;

namespace M7.LessonPlanner.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        [TestMethod]
        public void ScheduleCtorTest()
        {
            string Name = "My Spec Schedule!";
            Schedule s = new Schedule( Name );

            Assert.AreEqual( Name, s.Name );
            Assert.IsNotNull( s.Activities );
            Assert.AreEqual( 0, s.Activities.Count );
        }

        [TestMethod]
        public void SetNameTest() {
            
            string Name = "Schedule";
            Schedule s = new Schedule( null );
            s.SetName( Name );

            Assert.AreEqual( Name, s.Name );
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void SetNameBlankTest( string Name ) {
            
            Schedule s = new Schedule( "Test 1" );
            s.SetName( Name );

            Assert.AreEqual( "Untitled", s.Name );
        }

        [TestMethod]
        public void AddActivityTest() {

            Schedule s = new Schedule( "Test" );
            var Act = new {
                Name = "School Time", Description = "Test", 
                Start = DateTime.Now.TimeOfDay, End = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) )
            };
            s.AddActivity( Act.Name, Act.Description, Act.Start, Act.End );

            Assert.AreEqual( 1, s.Activities.Count );
            Assert.AreEqual( 1, s.Activities.First().ID );
        }

        [TestMethod]
        public void AddActivityTest2() {

            Schedule s = new Schedule( "Test" );
            var Start = DateTime.Now.TimeOfDay;
            var End = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) );
            
            s.AddActivity( "A1", "", Start, End );
            s.AddActivity( "A2", "", Start, End );

            Assert.AreEqual( 2, s.Activities.Count );
            Assert.AreEqual( 1, s.Activities[0].ID );
            Assert.AreEqual( 2, s.Activities[1].ID );
        }

        [TestMethod]
        public void AddActivityTestException() {

            Schedule s = new Schedule( "Test" );
            var Act = new {
                Name = "School Time", Description = "Test", 
                End = DateTime.Now.TimeOfDay, Start = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) )
            };
            Assert.ThrowsException<Exception>( () => s.AddActivity( Act.Name, Act.Description, Act.Start, Act.End ) );
        }
 
        [TestMethod]
        public void RemoveActivitiesTest() {

            Schedule s = new Schedule( "Test" );
            List<Activity> Activities = new List<Activity> {
                new Activity( 1, "A1", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) ),
                new Activity( 2, "A2", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) ),
                new Activity( 3, "A3", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) )
            };

            s.LoadSchedule( "Test", Activities );

            s.RemoveActivity( 2 );
            Assert.AreEqual( 2, s.Activities.Count );
            Assert.AreEqual( "A1", s.Activities.SingleOrDefault( x => x.ID == 1 ).Name );
            Assert.AreEqual( "A3", s.Activities.SingleOrDefault( x => x.ID == 3 ).Name );
        }

        [TestMethod]
        public void RemoveActivitiesTest2() {

            Schedule s = new Schedule( "Test" );
            List<Activity> Activities = new List<Activity> {
                new Activity( 1, "A1", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) ),
                new Activity( 2, "A2", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) ),
                new Activity( 3, "A3", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) )
            };

            s.LoadSchedule( "Test", Activities );

            s.RemoveActivity( 1 );
            s.RemoveActivity( 3 );
            Assert.AreEqual( 1, s.Activities.Count );
            Assert.AreEqual( "A2", s.Activities.SingleOrDefault( x => x.ID == 2 ).Name );
        }

        [TestMethod]
        public void RemoveActivitiesTest3() {

            Schedule s = new Schedule( "Test" );
            List<Activity> Activities = new List<Activity> {
                new Activity( 1, "A1", "Description", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) ) ),
            };

            s.LoadSchedule( "Test", Activities );

            Assert.ThrowsException<Exception>( () => s.RemoveActivity( 3 ) );
        }
    }
}
