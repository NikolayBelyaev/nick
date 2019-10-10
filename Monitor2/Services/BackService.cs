using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Monitor2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor2.Services
{
    public class BackService : IHostedService
    {
        private Timer _timer;
        public IServiceScopeFactory _serviceScopeFactory;
        private BackgroundServiceOptions _config;

        public BackService(IServiceScopeFactory serviceScopeFactory, IOptions<BackgroundServiceOptions> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = options.Value;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckServices, null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.UpdateTime));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void CheckServices(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IHttpService _httpService = scope.ServiceProvider.GetRequiredService<IHttpService>();
                IDBService _dbService = scope.ServiceProvider.GetRequiredService<IDBService>();
                IRequestDBService _requestDBService = scope.ServiceProvider.GetRequiredService<IRequestDBService>();

                var services = _dbService.GetServices();

                foreach (var service in services)
                {
                    _requestDBService.SetData(await _httpService.CheckService(service));
                }
            }
        }
    }
}
