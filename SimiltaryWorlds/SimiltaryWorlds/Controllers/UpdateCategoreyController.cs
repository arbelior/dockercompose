using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimiltaryWorlds.Models;
using SimiltaryWorlds.Services;
using System.Collections.Generic;

namespace SimiltaryWorlds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateCategoreyController : ControllerBase
    {
        private readonly Ifacadelogic _ifacadelogic;

        public UpdateCategoreyController(Ifacadelogic ifacadelogic)
        {
            _ifacadelogic = ifacadelogic;
        }

        [HttpGet("UpdateDataFile")]
        public async Task<ActionResult> UpdateDataFile()
        {

            try
            {
                var d1 = _ifacadelogic.UpdateHobsCategorey();

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpGet("SaveJobsFile")]
        public async Task<ActionResult> SaveJobsFile()
        {

            try
            {

                await _ifacadelogic.SaveJobsToFileAsync();

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("GetJobsDataToUpdate")]
        public async Task<ActionResult> GetJobsDataToUpdate()
        {

            try
            {

                var GetalljobsFormFile =  await _ifacadelogic.LoadJobsFromFileAsync();


                var GetJobsFromAdam = await _ifacadelogic.GetAllJobs();

                if(_ifacadelogic.AreJobListsFullyEqualIgnoreOrder(GetJobsFromAdam, GetalljobsFormFile))
                {
                    return Ok("Same Table");
                }

                else
                {
                    //לעדכן את הקובץ של השינוי במשרות עם ה orderid
                    var getdiffernceJobs = _ifacadelogic.GetNewJobsDifference(GetJobsFromAdam, GetalljobsFormFile);

                    await _ifacadelogic.SaveJobDifferencesToFileAsync(getdiffernceJobs);

                    //check if need 
                    //var check_if_found_job_sentence = "";
                }

                return Ok("difference Table");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("GetFileDiffernceJob")]
        public async Task<ActionResult<List<Job>>> GetFileDiffernceJob()
        {
            try
            {
                var d1 = await _ifacadelogic.GetJobsFromFile();

                if (d1 != null)
                {
                    return (d1);
                }

                return (d1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("UpdateJobFile")]
        public async Task<ActionResult> UpdateJobFile([FromQuery] int OrderId)
        {

            try
            {
                if (OrderId == null || OrderId == 0)
                    return BadRequest($"Not Update File OrderId is :{OrderId}");

                await _ifacadelogic.RemoveJobByOrderIdAsyncFromFoundDiffernceJobsFle(OrderId);

                return Ok("difference Table updated");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
