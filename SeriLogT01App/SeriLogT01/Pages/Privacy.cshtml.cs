using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLogT01.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("*** This is Privacy page ***");

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i == 8)
                    {
                        throw new Exception("#Exection from privacy page");  
                    }
                    else
                    {
                        _logger.LogInformation("out put " + i);
                    }
                }

            }
            catch (Exception e)
            {

                _logger.LogError("**privacy error ", e);
            }
          

        }
    }
}
