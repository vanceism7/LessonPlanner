using Microsoft.VisualStudio.TestTools.UnitTesting;
using M7.LessonPlanner.Business;
using System;
using System.Linq;
using System.Collections.Generic;

namespace M7.LessonPlanner.Tests {

    [TestClass]
    public class ActivityTests {

        [TestMethod]
        public void ActivityCtorTest() {

            string Name = "Test Name";
            string Desc = "Test Description";
            var Start = DateTime.Now.TimeOfDay;
            var End = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) );

            var a = new Activity( 1, Name, Desc, Start, End );
            
            Assert.AreEqual( 1, a.ID );
            Assert.AreEqual( Name, a.Name );
            Assert.AreEqual( Desc, a.Description );
            Assert.AreEqual( Start, a.StartTime );
            Assert.AreEqual( End, a.EndTime );
        }

        [TestMethod]
        public void ActivityInvalidTimeRangeTest() {

            var End = DateTime.Now.TimeOfDay;
            var Start = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) );

            Assert.ThrowsException<Exception>( () => new Activity( 1, "", "", Start, End ) );
        }

        [TestMethod]
        public void ChangeActivityTest() {
            var Start = DateTime.Now.TimeOfDay;
            var End = DateTime.Now.TimeOfDay.Add( TimeSpan.FromMinutes( 3 ) );

            var a = new Activity( 1, "Test", "Test Desc", Start, End );

            string Name = "Updated Name!";
            string Desc = "Updated Description!";

            a.ChangeActivity( Name, Desc, Start, End );

            Assert.AreEqual( 1, a.ID );
            Assert.AreEqual( Name, a.Name );
            Assert.AreEqual( Desc, a.Description );
        }
    }
}