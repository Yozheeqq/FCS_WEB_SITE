using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Data.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCS_WebSite_v2.Controllers
{
    [Route("form/")]
    public class FormController : Controller
    {

        [Route("{id}")]
        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Edit(IFormCollection fc)
        {
            Form form = new Form()
            {
                // id генерируется автоматически
                EventId = "tstevent", // тоже брать ивент из контекста
                CreatorId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value
            };
            DBObjects.InitialForm(form);
            return Redirect($"/form/edit/{form.Id}");
        }

        private DateTime ParseDate(string input)
        {
            var parsed = input.Split('T');
            var date = parsed[0];
            var time = parsed[1];
            var dateParse = date.Split('-').Select(x => int.Parse(x)).ToList();
            var timeParse = time.Split(':').Select(x => int.Parse(x)).ToList();
            return new DateTime(dateParse[0], dateParse[1],
                dateParse[2], timeParse[0],
                timeParse[1], 0);
        }

        [HttpGet]
        [Route("edit/{id}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Edit([FromRoute] string id)
        {
            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            var formElements = DBObjects.GetFormQuestions().Where(x => x.FormId == id).ToList();
            SortQuestions(formElements);
            var model = new Tuple<Form, List<FormQuestion>>(form, formElements);
            return View(model);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult SaveForm(IFormCollection fc, string id)
        {
            SetFormAttributes(fc, id);
            DeletePreviousQuestions(id);
            CreateQuestions(fc, id);

            return Redirect("/profile/myforms");
        }

        [HttpPost("fill")]
        public IActionResult Fill(IFormCollection fc)
        {
            var formId = fc["formId"];

            return Redirect($"fill/{formId}");
        }

        [HttpGet]
        [Route("fill/{id}")]
        public IActionResult Fill([FromRoute] string id)
        {
            var formElements = DBObjects.GetFormQuestions().Where(x =>
                x.FormId == id).ToList();
            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            SortQuestions(formElements);
            var model = new Tuple<Form, List<FormQuestion>>(form, formElements);
            return View(model);
        }

        [HttpPost("send/{id}")]
        public IActionResult SendForm(IFormCollection fc, string id)
        {
            return Redirect("/profile");
        }

        private void DeletePreviousQuestions(string id)
        {
            var list = DBObjects.GetFormQuestions().Where(x =>
                x.FormId == id).ToList();
            for(int i = 0; i < list.Count; i++)
            {
                DBObjects.Content.FormQuestion.Remove(list[i]);
            }
            DBObjects.Content.SaveChanges();
        }

        private void SortQuestions(List<FormQuestion> formQuestions)
        {
            formQuestions.OrderBy(x => int.Parse(x.TypeId.Split('_')[1]));
        }

        private void CreateQuestions(IFormCollection fc, string id)
        {
            var formKeys = fc.Keys.Where(x => x.StartsWith("select_")).ToList();
            for(int i = 0; i < formKeys.Count; i++)
            {
                FormQuestion formQuestion = new FormQuestion();
                formQuestion.FormId = id;
                formQuestion.Id = formKeys[i] + ";;" + id; // Ну тут вообще плохо
                switch (fc[formKeys[i]])
                {
                    case "0":
                        formQuestion.TypeId = "qshort";
                        break;
                    case "1":
                        formQuestion.TypeId = "qlong";
                        break;
                    case "2":
                        formQuestion.TypeId = "qcheckbox";
                        break;
                }
                var idForAll = formKeys[i].Split('_')[1];
                var inputKey = "questionInput_" + idForAll;
                formQuestion.Content = fc[inputKey];
                if(fc[formKeys[i]] == "2")
                {
                    var keyForCheckboxAns = "inputPlace_" + idForAll;
                    var checkBoxAnswers = fc.Keys.
                        Where(x => x.StartsWith(keyForCheckboxAns)).ToList();
                    for(int j = 0; j < checkBoxAnswers.Count; j++)
                    {
                        var answer = fc[checkBoxAnswers[j]];
                        formQuestion.Content += ";" + answer;
                    }
                }
                DBObjects.InitialFormQuestion(formQuestion);
            }
        }

        private void SetFormAttributes(IFormCollection fc, string formId)
        {
            var form = DBObjects.GetForm().Where(x => x.Id == formId).First();
            form.Name = fc["formname"];
            var ifDate = fc["ifdate"];
            if(!String.IsNullOrEmpty(ifDate))
            {
                var startDates = fc["startDate"].ToString().Split('-').
                    Select(x => int.Parse(x)).ToList();
                DateTime startDate = new DateTime(
                    startDates[0], startDates[1], startDates[2]);
                var endDates = fc["endDate"].ToString().Split('-').
                    Select(x => int.Parse(x)).ToList();
                DateTime endDate = new DateTime(
                    endDates[0], endDates[1], endDates[2]);
                form.StartDate = startDate;
                form.EndDate = endDate;
            } else
            {
                form.StartDate = null;
                form.EndDate = null;
            }
            DBObjects.Content.SaveChanges();
        }
    }
}
