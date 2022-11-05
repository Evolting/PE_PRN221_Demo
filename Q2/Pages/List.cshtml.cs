using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Q2.Pages
{
    public class ListModel : PageModel
    {
        PRN221_Spr22Context context = new PRN221_Spr22Context();

        public void OnGet()
        {
            List<Service> services = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.Month == DateTime.Now.Month).ToList();

            ViewData["services"] = services;
        }

        public void OnPostSearch(String title)
        {
            List<Service> services = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.RoomTitle.Contains(title)).ToList();

            ViewData["services"] = services;
        }

        public void OnPostImport(IFormFile importJSON)
        {
            var content = string.Empty;

            using (var reader = new StreamReader(importJSON.OpenReadStream()))
            {
                content = reader.ReadToEnd();
            }

            List<Service> services = null;

            services = JsonSerializer.Deserialize<List<Service>>(content);

            foreach(var s in services)
            {
                s.Id = 0;

                context.Services.Add(s);
                context.SaveChanges();
            }

            List<Service> allServices = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.Month == DateTime.Now.Month).ToList();

            ViewData["services"] = allServices;
        }
    }
}
