using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimiltaryWorlds.Models;
using SimiltaryWorlds.Services;

namespace SimiltaryWorlds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("similar-jobs")]
        public async Task<ActionResult<List<Job>>> GetSimilarJobs([FromBody] SentenceRequest request)
        {
            try
            {
                var results = await _jobService.GetSimilarJobsAsync(request.Sentence);

                var result_jobs = results.Where(x => !string.IsNullOrEmpty(x.JobName)).Select(x => x.JobName).ToList();
               
                return Ok(result_jobs);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }
    }

 }
