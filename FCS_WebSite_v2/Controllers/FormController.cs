using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Data.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult Edit(IFormCollection fc, string eventId)
        {
            if (eventId == null)
            {
                return Redirect("/profile");
            }
            Form form = new Form()
            {
                // id генерируется автоматически
                EventId = eventId, // тоже брать ивент из контекста
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
            if (form.CreatorId != ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Forbid();
            }
            var formElements = DBObjects.GetFormQuestions().Where(x => x.FormId == id).ToList();
            SortQuestions(formElements);
            var registerFormEndDate = DBObjects.GetForm().Where(x => x.EventId == form.EventId &&
                x.IsRegistration == 1).First().EndDate;
            var model = (form, formElements, registerFormEndDate);
            return View(model);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult SaveForm(IFormCollection fc, string id)
        {
            SetFormAttributes(fc, id);
            DeletePreviousQuestions(id);
            CreateQuestions(fc, id);

            return Redirect("/profile");
        }

        [HttpPost("fill")]
        [Authorize]
        public IActionResult Fill(IFormCollection fc, string formId)
        {
            return Redirect($"fill/{formId}");
        }

        [HttpGet]
        [Route("fill/{id}")]
        [Authorize]
        public IActionResult Fill([FromRoute] string id)
        {
            var formElements = DBObjects.GetFormQuestions().Where(x =>
                x.FormId == id).ToList();
            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            // Если прошел дедлайн формы
            if (form.EndDate != null && form.EndDate < DateTime.Now)
            {
                return Forbid();
            }
            SortQuestions(formElements);
            var model = new Tuple<Form, List<FormQuestion>>(form, formElements);
            return View(model);
        }

        [HttpPost("send/{id}")]

        public IActionResult SendForm(IFormCollection fc, string id)
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var formKeys = fc.Keys.Where(x => x.StartsWith("answer:")).ToList();
            var groupedKeys = formKeys.GroupBy(x => x.Split(':')[1]).ToList();

            for (int i = 0; i < groupedKeys.Count(); i++)
            {
                var answersKeys = groupedKeys[i];
                FormQuestionAnswer questionAnswer = new FormQuestionAnswer();
                questionAnswer.UserId = userId;
                questionAnswer.QuestionId = answersKeys.Key;
                questionAnswer.FormTime = DateTime.Now;
                for (int j = 0; j < answersKeys.Count(); j++)
                {
                    if (j != 0)
                    {
                        questionAnswer.Answers += ";";
                    }
                    questionAnswer.Answers += fc[answersKeys.ElementAt(j)];
                }

                DBObjects.InitialFormQuestionAnswer(questionAnswer);

            }

            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            if (form.IsRegistration == 1)
            {
                UserEvent userEvent = new UserEvent()
                {
                    UserId = userId,
                    EventId = form.EventId
                };
                DBObjects.InitialUserEvent(userEvent);
            }

            return Redirect("/profile");
        }

        private void DeletePreviousQuestions(string id)
        {
            var list = DBObjects.GetFormQuestions().Where(x =>
                x.FormId == id).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                DBObjects.Content.FormQuestion.Remove(list[i]);
            }
            DBObjects.Content.SaveChanges();
        }

        private void SortQuestions(List<FormQuestion> formQuestions)
        {
            formQuestions.OrderBy(x => int.Parse(x.TypeId.Split('_')[1]));
        }

        [HttpPost]
        [Route("copyform")]
        public IActionResult CopyForm(IFormCollection fc)
        {
            string formId = fc["availableForms"];
            string eventName = fc["availableEvents"];

            Form copyForm = DBObjects.GetForm().Where(x => x.Id == formId).Single();
            Event @event = DBObjects.GetEvents().Where(x => x.Name == eventName).Single();

            if (copyForm.EventId == @event.Id)
            {
                return Redirect("/profile");
            }

            Form newForm = new Form()
            {
                Name = copyForm.Name,
                StartDate = copyForm.StartDate,
                EndDate = copyForm.EndDate,
                CreatorId = copyForm.CreatorId,
                EventId = @event.Id,
                IsRegistration = copyForm.IsRegistration
            };

            DBObjects.InitialForm(newForm);

            List<FormQuestion> copyFormQuestions = DBObjects.GetFormQuestions().
                Where(x => x.FormId == copyForm.Id).ToList();

            foreach (var question in copyFormQuestions)
            {
                string id = question.Id.Split(";;")[0];
                FormQuestion newFormQuestion = new FormQuestion()
                {
                    Id = id + ";;" + newForm.Id,
                    Content = question.Content,
                    TypeId = question.TypeId,
                    Answers = question.Answers,
                    FormId = newForm.Id
                };
                DBObjects.InitialFormQuestion(newFormQuestion);
            }

            

            return Redirect("/profile");
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult DeleteForm([FromRoute] string id)
        {
            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            DBObjects.GetForm().Remove(form);
            DBObjects.Content.SaveChanges();
            return Redirect("/profile");
        }

        private void CreateQuestions(IFormCollection fc, string id)
        {
            var formKeys = fc.Keys.Where(x => x.StartsWith("select_")).ToList();
            for (int i = 0; i < formKeys.Count; i++)
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
                if (fc[formKeys[i]] == "2")
                {
                    var keyForCheckboxAns = "inputPlace_" + idForAll;
                    var checkBoxAnswers = fc.Keys.
                        Where(x => x.StartsWith(keyForCheckboxAns)).ToList();
                    for (int j = 0; j < checkBoxAnswers.Count; j++)
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
            var isRegistration = fc["isRegistration"];
            var eventId = form.EventId;
            // Проверяем, что это единственная форма для регистрации
            var isRegisterFormExist = DBObjects.GetForm().Where(x => x.EventId == eventId &&
                x.IsRegistration == 1).Any();
            if (!isRegisterFormExist)
            {
                form.IsRegistration = !String.IsNullOrEmpty(isRegistration) ? 1 : 0;
            }


            if (!String.IsNullOrEmpty(ifDate))
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
            }
            else
            {
                form.StartDate = null;
                form.EndDate = null;
            }
            DBObjects.Content.SaveChanges();
        }
    }
}
