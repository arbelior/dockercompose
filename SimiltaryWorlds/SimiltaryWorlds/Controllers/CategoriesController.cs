using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimiltaryWorlds.Helpers;
using SimiltaryWorlds.Models;
using SimiltaryWorlds.Services;
using System.Linq;

namespace SimiltaryWorlds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ISearchCityServer _search_city_Service;

        private readonly Ifacadelogic _ifacadelogic;
        private readonly IJobService _ijobservice;
        private readonly bool IsMotorActive;
        private readonly IfreeSearch _ifreesearch;
        public CategoriesController(ISearchService searchService, ISearchCityServer search_city_server, Ifacadelogic ifacadelogic, IJobService ijobservice,IConfiguration iconfig,IfreeSearch ifreesearch)
        {
            _searchService = searchService;
            _search_city_Service = search_city_server;
            _ifacadelogic = ifacadelogic;
            _ijobservice = ijobservice;
            IsMotorActive = iconfig.GetValue<bool>("AiSearchMotor");
            _ifreesearch = ifreesearch;
        }


        [HttpPost]
        [Route("/analyze")]
        public async Task<ActionResult<List<Job>>> Analyze([FromBody] string description)
        {
            if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException("description from user has not found");

            
            if (IsMotorActive == false) 
            {
                List<Job> getrelevantjobs = new List<Job>();

                List<Job> getalljobs = await _ifreesearch.GetAllJobs();

                #region only if has comparer
                getrelevantjobs = getalljobs.Where(x => x.JobName.Contains(description)).ToList();

                return Ok(getrelevantjobs);
                #endregion
                //getrelevantjobs = await _ifreesearch.GetRelevantJobs(description,getalljobs);

                //return Ok(getrelevantjobs);

            }
            
            List<Job> GetGlobalJobs = await _ifacadelogic.GetAllJobsIfAsk(description);

            if(GetGlobalJobs.Count() > 0)
            {
                return GetGlobalJobs;
            }

            //var check_get_all_jobs = await _ijobservice.GetSimilarJobs(description, 0.84f, GetGlobalJobs.Count, GetGlobalJobs);

            //if(GetGlobalJobs.Count() > 0)
            //{
            //    return Ok(check_get_all_jobs);
            //}


            bool res = _searchService.checkIfCategoreyNotExist(description);

            if(res == true)
            {
                List<Job> data = new List<Job>();

                if (data.Count() == 0)
                    return (data);
            }

            var cities_area_result = _search_city_Service.GetCitiesByArea(description);



            var result = _searchService.AnalyzeSearch(description);

            var  GetRelevanjobsbyCategorey = _searchService.GetRellevatJobsbyCategorey(result);

            //if(GetRelevanjobsbyCategorey != null)
            //{
            //    return Ok(GetRelevanjobsbyCategorey);
            //}



            var check_if_user_write_city = _search_city_Service.DetectCityFromText(description);

            if (!string.IsNullOrEmpty(check_if_user_write_city) && GetRelevanjobsbyCategorey.Count() > 0 && GetRelevanjobsbyCategorey != null)
            {
                //רשם עיר וגם חזרו קטגוריות

                var getsimilarcities_OrderIds = _ifacadelogic.GetequalData(check_if_user_write_city, GetRelevanjobsbyCategorey);

                if (getsimilarcities_OrderIds != null)
                {
                    var GetRelevantJobs = _ifacadelogic.GetJobs(getsimilarcities_OrderIds);

                    if (GetRelevantJobs.Count() == 0)
                    {
                        // להריץ את כל המערך ולבקש גדול מ 0.72 את ה 2 האחרונים

                        //var d1 = await _ijobservice.GetSimilarJobs(description);

                        //return Ok(d1);

                        return Ok(GetRelevantJobs);
                    }
                    else
                    {
                        //להריץ גדול מ 0.6 ולהביא את ה 5 הכי גבוהים

                        var d2 = await _ijobservice.GetSimilarJobs(description,0.79f,5,GetRelevantJobs);

                        return Ok(d2);
                    }


                }

                else
                {
                    //לא בטוח שרלוונטי אולי לגשת עם להשוואה עם דמיון סימנטי של 0.85 ומעלה ולקחת את השניים האחרונים
                }
            }

            else if(cities_area_result.Count() > 0)
            {
                if(GetRelevanjobsbyCategorey.Count() == 0)
                {
                    //תחזיר עם דמיון של 0.84 ומעלה

                    List<Job> GetAllJobs = await _ifacadelogic.GetAllJobs();

                    GetAllJobs.RemoveAll(item =>
                       !cities_area_result.Contains(_search_city_Service.DetectCityFromText(item.JobName))
                  );

                    //

                    if(_ifacadelogic.ContainsGlobalWordsPartially(description))
                    {
                        return Ok(GetAllJobs);
                    }

                    var jobsrelevancitybyarea = await _ijobservice.GetSimilarJobs(description, 0.85f, 4, GetAllJobs);

                    return Ok(jobsrelevancitybyarea);
                }
                else
                {
                    //foreach (var item in GetRelevanjobsbyCategorey.ToList())
                    //{
                    //    var check_if_found_city_in_sentence = _search_city_Service.DetectCityFromText(item.JobName);

                    //    if (!cities_area_result.Contains(check_if_found_city_in_sentence))
                    //    {
                    //        GetRelevanjobsbyCategorey.Remove(item);
                    //    }
                    //}

                    List<Job> AddRelevantJobsCategoreyAndArea = new List<Job>();

                    foreach (var item in GetRelevanjobsbyCategorey)
                    {
                        var check_if_user_write_city_ = _search_city_Service.DetectCityFromText(item.JobName);

                        if (check_if_user_write_city_ != null)
                        {
                            if (cities_area_result.Contains(check_if_user_write_city_))
                            {
                                AddRelevantJobsCategoreyAndArea.Add(item);
                            }

                        }
                        else
                        {
                            AddRelevantJobsCategoreyAndArea.Add(item);
                        }

                    }




                    //GetRelevanjobsbyCategorey.RemoveAll(item =>
                    //    !cities_area_result.Contains(_search_city_Service.DetectCityFromText(item.JobName))
                    //);

                    if (AddRelevantJobsCategoreyAndArea.Count() == 0)
                        return Ok(AddRelevantJobsCategoreyAndArea);

                    var jobsrelevancitybyareacategorey = await _ijobservice.GetSimilarJobs(description, 0.75f, 4, AddRelevantJobsCategoreyAndArea);

                    return Ok(jobsrelevancitybyareacategorey);
                }
            }

            else if (!string.IsNullOrEmpty(check_if_user_write_city) && GetRelevanjobsbyCategorey.Count() == 0)
            {

                var getrelevantjobs = _ifacadelogic.GetJobsWithWriteCityNotFoundJobsCategorey(check_if_user_write_city);

                //צריך לבדוק פה מילים כלליות ולהחזיר

                if (_ifacadelogic.ContainsGlobalWordsPartially(description))
                {
                    return Ok(getrelevantjobs);
                }

                //*/*

                var jobsrelevancity = await _ijobservice.GetSimilarJobs(description, 0.8f, 4, getrelevantjobs);

                return Ok(jobsrelevancity);

                //רשם עיר ולא חזרו קטגוריות להביא

                // להחזיר את המשרות הכלליות לעיר שיש אם יש התאמה בין העיר למשרות הכלליות

                //אם אין התאמה לא בעיר ולא חזרו קטגוריות לא לעשות כלום מערך ריק
            }

            else if (string.IsNullOrEmpty(check_if_user_write_city) && GetRelevanjobsbyCategorey.Count() > 0)
            {
                //אם לא רשם עיר וגם חזרו קטגוריות

                //לשלוח את המשפט עם משרות של הקטגוריות עם דמיון סימנטי של מעל 0.7  ולבקש 7 משרות אחרונות
                var d3 = await _ijobservice.GetSimilarJobs(description, 0.70f, 4, GetRelevanjobsbyCategorey);

                return Ok(d3);

            }

            else if (string.IsNullOrEmpty(check_if_user_write_city) && GetRelevanjobsbyCategorey.Count() == 0)
            {
                var d7 = await _ijobservice.GetSimilarJobs(description, 0.8f, 1, GetRelevanjobsbyCategorey);
                return Ok(d7);

                //אם לא רשם עיר וגם לא חזרו קטגוריות
            }

            if (GetRelevanjobsbyCategorey.Count() == 0)
            {
                // לבדוק אם המשפט מכיל מילים כלליות כמו תביא את כל המשרות הכל כל מה שיש ולהחזיר את כל המשרות
            }

            if(GetRelevanjobsbyCategorey.Count() == 0)
            {
                //לבדוק אם רשמו משפטים כמו  כל המשרות בירושלים  כלומר לבדוק אם כל המשרות בעיר מסויימת

                //נבדוק אם יש עיר

                //נבדוק אם יש גם התאמה למילים כלליות

                //נבדוק אם רשמו בצפון במרכז או בשפלה

                //ונעשה השוואה ונחזיר תשובה
            }


            return Ok(GetRelevanjobsbyCategorey);
        }

        [HttpPost("GetCoreSentece")]
        public async Task<ActionResult<string>> GetProfessionFromSentence(string sentence)
        {
            try
            {
                //var d5 = await _ifacadelogic.GetProfessionFromSentence(sentence);

                var d6 = await _ifacadelogic.GetProfessionFromSentenceAi(sentence);



                return Ok(d6);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }






    }
}
