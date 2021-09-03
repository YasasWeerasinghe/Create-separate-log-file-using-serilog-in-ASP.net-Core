using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeriLogT01
{
    public class Log01 : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ILog02 _classLog02;

        public Log01(ILogger<Log01> logger, ILog02 log02)
        {
            _logger = logger;
            _classLog02 = log02;
        }        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Outer method");

            using (LogContext.PushProperty("firstLog", 1))
            {
                DisplayLog();
                await Task.CompletedTask;
            }

            using (LogContext.PushProperty("secondLog", 2))
            {
                _classLog02.DiplayLog();
                await Task.CompletedTask;
            }
        }

        public void DisplayLog()
        {
            _logger.LogInformation("First log file");
        }
    }
}
