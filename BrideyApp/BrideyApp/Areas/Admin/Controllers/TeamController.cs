using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITeamService _teamService;

        public TeamController(AppDbContext context, 
                              IWebHostEnvironment env, 
                              ITeamService teamService)
        {
            _context = context;
            _env = env;
            _teamService = teamService;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
