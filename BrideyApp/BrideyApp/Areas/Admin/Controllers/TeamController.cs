using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITeamService _teamService;
        private readonly IPositionService _positionService;

        public TeamController(AppDbContext context,
                              IWebHostEnvironment env,
                              ITeamService teamService,
                              IPositionService positionService)
        {
            _context = context;
            _env = env;
            _teamService = teamService;
            _positionService = positionService;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _teamService.GetAll();
            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Team team = await _teamService.GetTeamById(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Team dbTeam = await _teamService.GetTeamById(id);
                if (dbTeam == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbTeam.Image);
                FileHelper.DeleteFile(path);
                _context.Teams.Remove(dbTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            @ViewBag.positions = await GetPositionsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM team)
        {
            @ViewBag.positions = await GetPositionsAsync();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(team);
                }
                if (!team.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(team);
                }
                if (!team.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(team);

                }
                string fileName = Guid.NewGuid().ToString() + " " + team.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                await FileHelper.SaveFileAsync(newPath, team.Photo);

                Team newTeam = new()
                {
                    Image = fileName,
                    Name = team.Name,
                    PositionId = team.PositionId,
                    Testimonials = team.Testimotionals,

                };
                await _context.Teams.AddAsync(newTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                @ViewBag.error = ex.Message;
                return View();
            }
        }
        private async Task<SelectList> GetPositionsAsync()
        {
            List<Position> positions = await _positionService.GetAll();
            return new SelectList(positions, "Id", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            @ViewBag.positions = await GetPositionsAsync();

            if (id == null) return BadRequest();
            Team dbTeam = await _teamService.GetTeamById(id);
            if (dbTeam == null) return NotFound();

            TeamUpdateVM model = new()
            {
                Image = dbTeam.Image,
                Name = dbTeam.Name,
                Testimotionals = dbTeam.Testimonials,
                PositionId = dbTeam.PositionId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, TeamUpdateVM updatedTeam)
        {
            @ViewBag.positions = await GetPositionsAsync();

            try
            {
                if (id == null) return BadRequest();
                Team dbTeam = await _teamService.GetTeamById(id);
                if (dbTeam == null) return NotFound();

                TeamUpdateVM model = new()
                {
                    Image = dbTeam.Image,
                    Name = dbTeam.Name,
                    Testimotionals = dbTeam.Testimonials,
                    PositionId = dbTeam.PositionId
                };

                if (updatedTeam.Photo != null)
                {
                    if (!updatedTeam.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!updatedTeam.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbTeam.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + updatedTeam.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, updatedTeam.Photo);
                    dbTeam.Image = fileName;
                }
                else
                {
                    Team newTeam = new()
                    {
                        Image = dbTeam.Image
                    };
                }

                dbTeam.Name = updatedTeam.Name;
                dbTeam.PositionId = updatedTeam.PositionId;
                dbTeam.Testimonials = updatedTeam.Testimotionals;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




    }
}
