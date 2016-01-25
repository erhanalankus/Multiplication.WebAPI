using CarpYeni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CarpYeni.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Returns all the records in the database ordered(descending) by solve date
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            List<Question> allRecords;
            try
            {
                using (var db = new CarpmaContext())
                {
                    allRecords = db.Questions.OrderByDescending(p => p.SolveDate).ToList();
                }
                return Ok(allRecords);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// You make a get request to this method with an integer(1:Wood, 2:Copper, 3:Silver, 4:Gold), it creates a question in the database using the id as the max value(inclusive) for the numbers and returns that question. Parameter name is "id" because I am lazy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var db = new CarpmaContext())
                {
                    int maxValue = GetMaxValue(id, db);
                    var rnd = new Random();
                    int number1 = rnd.Next(2, maxValue);
                    int number2 = rnd.Next(2, maxValue);
                    var questionToCreateAndSend = new Question
                    {
                        CreateDate = DateTime.Now,
                        IsChecked = false,
                        Max = maxValue,
                        Number1 = number1,
                        Number2 = number2,
                        Result = number1 * number2
                    };
                    db.Questions.Add(questionToCreateAndSend);
                    db.SaveChanges();
                    return Created<Question>("", questionToCreateAndSend);
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private int GetMaxValue(int id, CarpmaContext db)
        {
            var scoreSetting = db.ScoreSettings.First();
            switch (id)
            {
                case 1:
                    return scoreSetting.Wood;
                case 2:
                    return scoreSetting.Copper;
                case 3:
                    return scoreSetting.Silver;
                case 4:
                    return scoreSetting.Gold;
                default:
                    return 100;
            }
        }



        /// <summary>
        /// Send your answered question in the body. Use question id as url parameter. It will return the updated score. Method is POST instead of PUT because of Unity3d's WWW class limitations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/values/5
        public IHttpActionResult Post(int id, [FromBody]Question value)
        {
            Score score;
            ScoreSetting scoreSetting;
            Question questionToUpdate;
            try
            {
                using (var db = new CarpmaContext())
                {
                    scoreSetting = db.ScoreSettings.First();
                    score = db.Scores.First();
                    questionToUpdate = db.Questions.First(p => p.Id == id);
                    questionToUpdate.SolveDate = value.SolveDate;
                    questionToUpdate.SolveSeconds = value.SolveSeconds;

                    if (value.Max <= scoreSetting.Wood)
                    {
                        score.Wood++;
                    }
                    else if (value.Max <= scoreSetting.Copper)
                    {
                        score.Copper++;
                    }
                    else if (value.Max <= scoreSetting.Silver)
                    {
                        score.Silver++;
                    }
                    else
                    {
                        score.Gold++;
                    }
                    db.SaveChanges();
                }
                return Ok(score);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
