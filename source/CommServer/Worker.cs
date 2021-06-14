using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private static IHubContext<CommunicationHub> _hubContext { get; set; }
        public Worker(ILogger<Worker> logger, IConfiguration config,IHubContext<CommunicationHub> hubContext)
        {
            _logger = logger;
            _config = config;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            ConfigurationManager configManager = new ConfigurationManager(_config);

            //DI for the hub context
            FileWatcher fw = new FileWatcher(_hubContext);
            fw.AddDirectoryWatch(ConfigurationManager.DirectoryPathToWatch);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
