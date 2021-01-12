using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaModel.Data;
using CinemaModel.Models;
using Tomoiaga_AndreiBogdan_CinemaTAB.Models.CinemaViewModels;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Controllers
{
    public class ActorsController : Controller
    {
        private readonly CinemaContext _context;

        public ActorsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index(int? id, int? movieID)
        {
            var viewModel = new ActorIndexData();
            viewModel.Actors = await _context.Actors
            .Include(i => i.AppearanceMovies)
            .ThenInclude(i => i.Movie)
            .ThenInclude(i => i.Studio)
            //.ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .ToListAsync();

            if (id != null)
            {
                ViewData["ActorID"] = id.Value;
                Actor actor = viewModel.Actors.Where(
                i => i.ActorID == id.Value).Single();
                viewModel.Movies = actor.AppearanceMovies.Select(s => s.Movie);
            }
            if (movieID != null)
            {
                ViewData["MovieID"] = movieID.Value;
                viewModel.Studios = viewModel.Movies.Where( x => x.MovieID == movieID).Select(s=>s.Studio);
            }
            return View(viewModel);
        }


        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Actors.ToListAsync());
        //}

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorID,Name,BirthDate,BirthPlace")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var actor = await _context.Actors.FindAsync(id);
        //    if (actor == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(actor);
        //}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actor = await _context.Actors
            .Include(i => i.AppearanceMovies).ThenInclude(i => i.Movie)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ActorID == id);
            if (actor == null)
            {
                return NotFound();
            }
            PopulateAppearanceMovieData(actor);
            return View(actor);

        }
        private void PopulateAppearanceMovieData(Actor actor)
        {
            var allMovies = _context.Movies;
            var appearanceMovies = new HashSet<int>(actor.AppearanceMovies.Select(c => c.MovieID));
            var viewModel = new List<AppearanceMovieData>();
            foreach (var movie in allMovies)
            {
                viewModel.Add(new AppearanceMovieData
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    AppearedIn = appearanceMovies.Contains(movie.MovieID)
                });
            }
            ViewData["Movies"] = viewModel;
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ActorID,Name,BirthDate,BirthPlace")] Actor actor)
        //{
        //    if (id != actor.ActorID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(actor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ActorExists(actor.ActorID))
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
        //    return View(actor);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMovies)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorToUpdate = await _context.Actors
            .Include(i => i.AppearanceMovies)
            .ThenInclude(i => i.Movie)
            .FirstOrDefaultAsync(m => m.ActorID == id);
            if (await TryUpdateModelAsync<Actor>( actorToUpdate,"",i => i.Name, i => i.BirthDate, i => i.BirthPlace))
            {
                UpdateAppearanceMovies(selectedMovies, actorToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +"Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateAppearanceMovies(selectedMovies, actorToUpdate);
            PopulateAppearanceMovieData(actorToUpdate);
            return View(actorToUpdate);
        }

        private void UpdateAppearanceMovies(string[] selectedMovies, Actor actorToUpdate)
        {
            if (selectedMovies == null)
            {
                actorToUpdate.AppearanceMovies = new List<AppearanceMovie>();
                return;
            }
            var selectedMoviesHS = new HashSet<string>(selectedMovies);
            var appearanceMovies = new HashSet<int>
            (actorToUpdate.AppearanceMovies.Select(c => c.Movie.MovieID));
            foreach (var movie in _context.Movies)
            {
                if (selectedMoviesHS.Contains(movie.MovieID.ToString()))
                {
                    if (!appearanceMovies.Contains(movie.MovieID))
                    {
                        actorToUpdate.AppearanceMovies.Add(new AppearanceMovie{ActorID =actorToUpdate.ActorID,MovieID = movie.MovieID});
                    }
                }
                else
                {
                    if (appearanceMovies.Contains(movie.MovieID))
                    {
                        AppearanceMovie movieToRemove = actorToUpdate.AppearanceMovies.FirstOrDefault(i=> i.MovieID == movie.MovieID);
                        _context.Remove(movieToRemove);
                    }
                }
            }
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorID == id);
        }
    }
}
