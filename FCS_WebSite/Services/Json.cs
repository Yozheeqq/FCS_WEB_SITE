using Microsoft.AspNetCore.Hosting;
using FCS_WebSite.Models;
using System.Text.Json;

namespace FCS_WebSite.Services
{
    public class Json
    {
        public Json(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName =>
            Path.Combine(WebHostEnvironment.WebRootPath, "data", "user.json");

        public void WritePerson(Pupil pupil)
        {
            using (StreamWriter sw = new StreamWriter(JsonFileName))
            {
                sw.Write(JsonSerializer.Serialize(pupil));
            }
        }
    }
}
