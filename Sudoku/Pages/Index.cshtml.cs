using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using WebApplication3.Services;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISudokuService sodukoService;

        public IndexModel(ISudokuService sodukoService)
        {
            this.sodukoService = sodukoService;
        }
        
        public bool Solvabel { get; set; }

        public int[,] Board { get; set; }
        
        public int Difficulty { get; set; }
        public bool Solved { get; set; }

        public void OnGet()
        {
            Board = sodukoService.GetBoard();
            Difficulty = sodukoService.GetDifficulty();
            Solvabel = sodukoService.IsSolvabel();
            Solved = sodukoService.IsSolved() && sodukoService.IsSolvabel();
        }

        public void OnPost()
        {
            var request = Request.Form;

            if (request.TryGetValue("field", out var array))
            {
                var intArray = Array.ConvertAll(array.ToArray(), s => 
                {
                    int result = int.TryParse(s, out int value) ? value : 0;
                    return result <= 9 && result >= 0 ? result : 0;
                });
                
                sodukoService.SetResult(intArray);
            }

            OnGet();
        }

        public void OnPostSolve()
        {
            sodukoService.Solve();
            OnGet();
        }

        public void OnPostChangeDifficulty()
        {
            var request = Request.Form;
            if (request.TryGetValue("Difficulty", out var difficulty))
            {
                Difficulty = int.Parse(difficulty);
            }
            sodukoService.ChangeDifficulty(Difficulty);
            OnGet();
        }

    }
}
