using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLogT01
{
    public interface ILog02
    {
        void DiplayLog();
    }
    public class Log02 :ILog02
    {
        private readonly ILogger<Log02> _logger;

        public Log02(ILogger<Log02> logger)
        {
            _logger = logger;
        }

        public void DiplayLog()
        {
            _logger.LogInformation("Second log file");
        }
    }
}
