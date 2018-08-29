using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using M7.LessonPlanner.Business;
using M7.LessonPlanner.Api.Models;

namespace LessonPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : Controller
    {
        Schedule schedule;

        public SchedulesController( Schedule schedule ) {
            this.schedule = schedule;
        }

        private ActivityModel NewActivityModel( Activity x ) => 
            new ActivityModel { 
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            };

        #region Schedule Actions

        // GET api/values
        [HttpGet]
        public ActionResult<ScheduleModel> Get()
        {
            var scheduleInfo = new ScheduleModel {
                Name = schedule.Name,
                Activities = schedule.Activities.Select( NewActivityModel ).ToArray()
            };

            return scheduleInfo;
        }

        [HttpPost("name")]
        public IActionResult SetScheduleName( [FromBody]string Name ) {
            schedule.SetName( Name );
            return Ok();
        }

        //Used for uploading a saved schedule -- For modification
        // [HttpPost]
        // public ActionResult<string> Post([FromBody] string value)
        // {
        //     return Ok( value );
        // }

        [HttpDelete]
        public ActionResult<string> Delete()
        {
            schedule.Clear();
            return Ok( "Schedule cleared" );
        }

        #endregion
    
        #region Activity Actions

        [HttpGet("activities")]
        public ActionResult<Activity[]> GetActivities() {
            return schedule.Activities.ToArray();
        }

        [HttpGet("activities/{id}")]
        public ActionResult<Activity> GetActivityByID( int id ) {
            var result = schedule.Activities.SingleOrDefault( x => x.ID == id );
            if( result == null ) return NotFound();

            return Ok( result );
        }

        [HttpPost("activities")]
        public ActionResult AddActivity( [FromBody] ActivityModel activity ) {

            try {
                schedule.AddActivity( activity.Name, activity.Description, activity.StartTime, activity.EndTime );
                var created = schedule.Activities.Last();
                return Created( $"activities/{created.ID.ToString()}", created ); 
            } 
            catch( Exception ex ) {
                return BadRequest( ex.Message );
            }
        }

        [HttpPut("activities/{id}")]
        public ActionResult UpdateActivity( int id, [FromBody] ActivityModel activity ) {

            try {
                schedule.UpdateAcitivity( id, activity.Name, activity.Description, 
                    activity.StartTime, activity.EndTime ); 

                return Ok( schedule.Activities.SingleOrDefault( x => x.ID == id ) );
            }
            catch( Exception ex ) {
                return BadRequest( ex.Message );
            }
        }

        [HttpDelete("activities/{id}")]
        public ActionResult RemoveActivity( int id ) {
            
            try {
                schedule.RemoveActivity( id );
                return Ok( "Activity Deleted" );
            }
            catch( Exception ex ) {
                return BadRequest( ex.Message );
            }
        }

        #endregion
    }
}
