using Assessment.Phonebook.Services.DTO;
using Assessment.PhoneBook.Infrastructure;
using ClosedXML.Excel;
using FileCreateWorkerService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FileCreateWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _reportMQService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        public Worker(ILogger<Worker> logger, RabbitMQClientService reportMQService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _reportMQService = reportMQService;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _reportMQService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.queueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);
            var excel = JsonSerializer.Deserialize<ReportMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            using var ms = new MemoryStream();

            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable("reports"));
            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);
            MultipartFormDataContent multipartFormDataContent = new();
            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");
            var baseUrl = "https://localhost:44341/api/report/upload";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}?reportId={excel.ReportId}", multipartFormDataContent);

                if (response.IsSuccessStatusCode)
                {
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
        }

        private DataTable GetTable(string tableName)
        {
            var model = new List<GetDataTableModel>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PostgreSqlPhoneBookDbContext>();
                model = (from a in context.Persons.ToList()
                         join b in context.PersonDetails.ToList()
                         on a.Id equals b.PersonId
                         group new { a, b } by new { b.Location, b.PhoneNumber, }
                      into v
                         select new GetDataTableModel
                         {
                             Location = v.Key.Location,
                             PersonCount = v.Count(),
                             PhoneCount = v.Count()
                         }).ToList();

                DataTable table = new DataTable { TableName = tableName };
                table.Columns.Add("Location", typeof(string));
                table.Columns.Add("PersonCount", typeof(int));
                table.Columns.Add("PhoneCount", typeof(int));
                model.ForEach(x =>
                {
                    table.Rows.Add(x.Location, x.PersonCount, x.PhoneCount);

                });
                return table;
            }
        }
    }
}
