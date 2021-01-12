using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaModel.Data;
using CinemaModel.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Controllers
{
    public class StudiosController : Controller
    {
        private readonly CinemaContext _context;
        private string _baseUrl = "http://localhost:60178/api/Studios";

        public StudiosController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Studios
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var studios = JsonConvert.DeserializeObject<List<Studio>>(await response.Content.ReadAsStringAsync());
                return View(studios);
            }
            return NotFound();

        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Studios.ToListAsync());
        //}

        // GET: Studios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var studios = JsonConvert.DeserializeObject<Studio>(await response.Content.ReadAsStringAsync());
                return View(studios);
            }
            return NotFound();
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var studio = await _context.Studios
        //        .FirstOrDefaultAsync(m => m.StudioID == id);
        //    if (studio == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(studio);
        //}

        // GET: Studios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("StudioID,StudioName,Founded,ParentOrganization")] Studio studio)
        {
            if (!ModelState.IsValid) return View(studio);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(studio);
                var response = await client.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ ex.Message} ");
            }
            return View(studio);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("StudioID,StudioName,Founded,ParentOrganization")] Studio studio)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(studio);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(studio);
        //}

        // GET: Studios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var studio = JsonConvert.DeserializeObject<Studio>(await response.Content.ReadAsStringAsync());
                return View(studio);
            }
            return new NotFoundResult();
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var studio = await _context.Studios.FindAsync(id);
        //    if (studio == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(studio);
        //}

        // POST: Studios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("StudioID,StudioName,Founded,ParentOrganization")] Studio studio)
        {
            if (!ModelState.IsValid) return View(studio);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(studio);
            var response = await client.PutAsync($"{_baseUrl}/{studio.StudioID}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(studio);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("StudioID,StudioName,Founded,ParentOrganization")] Studio studio)
        //{
        //    if (id != studio.StudioID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(studio);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StudioExists(studio.StudioID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(studio);
        //}

        // GET: Studios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var studio = JsonConvert.DeserializeObject<Studio>(await response.Content.ReadAsStringAsync());
                return View(studio);
            }
            return new NotFoundResult();
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var studio = await _context.Studios
        //        .FirstOrDefaultAsync(m => m.StudioID == id);
        //    if (studio == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(studio);
        //}

        // POST: Studios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("StudioID")] Studio studio)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{studio.StudioID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(studio),Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ ex.Message} ");
            }
            return View(studio);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var studio = await _context.Studios.FindAsync(id);
        //    _context.Studios.Remove(studio);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool StudioExists(int id)
        //{
        //    return _context.Studios.Any(e => e.StudioID == id);
        //}
    }
}
