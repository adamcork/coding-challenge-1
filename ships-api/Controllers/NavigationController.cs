using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ships_api.Services;

namespace ships_api.Controllers
{
    [Route("api/[controller]")]
    public class NavigationController : Controller
    {
        // POST api/values
        [HttpPost]
        public string Post([FromBody]string navData)
        {
            var lines = navData.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            var gridSizeString = lines[0];
            var gridSize = gridSizeString.Split(' ');
            var gridXMax = Int16.Parse(gridSize[0]);
            var gridYMax = Int16.Parse(gridSize[1]);
            var navigationService = new NavigationService(gridXMax, gridYMax);

            var sb = new StringBuilder();

            for(var i = 1; i < lines.Length; i++)
            {
                if(string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                sb.Append(navigationService.ProcessShipInstructions(lines[i], lines[++i]));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
