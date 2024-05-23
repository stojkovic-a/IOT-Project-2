using EventInfo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventInfoController : ControllerBase
    {
        private readonly EventService eventService;

        public EventInfoController(EventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("GetLatestInfo")]
        public ActionResult GetLatestInfo()
        {
            try
            {
                var data=this.eventService.GetLatestInfo();
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetLastEvents")]
        public ActionResult GetLastEvents()
        {
            try
            {
                var data = this.eventService.GetLastEvents();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
