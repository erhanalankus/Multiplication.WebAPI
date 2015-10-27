using CarpYeni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarpYeni.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {        
        public ActionResult Index()
        {
            List<Question> allQuestions;
            using (var db = new CarpmaContext())
            {
                allQuestions = db.Questions.Where(q => q.SolveDate != null).OrderByDescending(p => p.SolveDate).ToList();
            }
            return View(allQuestions);
        }

        public ActionResult SetScoreIntervals()
        {
            ScoreSetting scoreSetting;
            using (var db = new CarpmaContext())
            {
                scoreSetting = db.ScoreSettings.First();
            }            
            return View(scoreSetting);
        }

        [HttpPost]
        public ActionResult SetScoreIntervals(ScoreSetting scoreSetting)
        {
            if (InputOrder(scoreSetting))
            {
                try
                {
                    using (var db = new CarpmaContext())
                    {
                        var currentSetting = db.ScoreSettings.First();
                        currentSetting.Wood = scoreSetting.Wood;
                        currentSetting.Copper = scoreSetting.Copper;
                        currentSetting.Silver = scoreSetting.Silver;
                        currentSetting.Gold = scoreSetting.Gold;
                        db.SaveChanges();
                        ViewBag.ValidationStatus = "success";
                        return View("SetScoreIntervals");
                    }
                }
                catch (Exception)
                {
                    ViewBag.ValidationStatus = "error";
                    return View("SetScoreIntervals");
                }                
            }
            else
            {
                ViewBag.ValidationStatus = "failure";
                return View("SetScoreIntervals");
            }            
        }

        private bool InputOrder(ScoreSetting scoreSetting)
        {
            if (scoreSetting.Wood < scoreSetting.Copper && scoreSetting.Wood < scoreSetting.Silver && scoreSetting.Wood < scoreSetting.Gold && scoreSetting.Copper < scoreSetting.Silver && scoreSetting.Copper < scoreSetting.Gold && scoreSetting.Silver < scoreSetting.Gold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult ResetScores()
        {
            Score score;
            using (var db = new CarpmaContext())
            {
                score = db.Scores.First();
            }
            return View(score);
        }

        [HttpPost]
        public ActionResult ResetScore()
        {
            try
            {
                using (var db = new CarpmaContext())
                {
                    var score = db.Scores.First();
                    score.Wood = 0;
                    score.Copper = 0;
                    score.Silver = 0;
                    score.Gold = 0;
                    db.SaveChanges();
                    ViewBag.ResetStatus = "success";
                    return View("ResetScores");
                }
            }
            catch (Exception)
            {
                ViewBag.ResetStatus = "error";
                return View("ResetScores");
            }
        }
    }
}
