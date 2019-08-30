namespace NineJoke.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using NineJoke.Data;
    using NineJoke.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ReportServiceTests
    {
        [Fact]
        public void CreateReportShouldCreateReport()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "CreateReportShouldCreateReport")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var reportService = new ReportService(dbContext);

            reportService.CreateReport("test", "asdasd", "asdasd", "asdasd");

            var report = dbContext.ReportPosts;

            Assert.Single(report);
        }

        [Fact]
        public void DeleteAllReportsShouldDeleteAllReports()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "DeleteAllReportsShouldDeleteAllReports")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var reportService = new ReportService(dbContext);

            dbContext.ReportPosts.AddRange(new List<ReportPost>
            {
                new ReportPost(),
                new ReportPost(),
                new ReportPost(),
                new ReportPost(),
                new ReportPost(),
            });
            dbContext.SaveChanges();

            reportService.DeleteAllReports();

            var reports = dbContext.ReportPosts.ToList();

            Assert.Empty(reports);
        }

        [Fact]
        public void DeleteReportShouldDeleteReportById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "DeleteReportShouldDeleteReportById")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var reportService = new ReportService(dbContext);

            var report = new ReportPost();
            dbContext.ReportPosts.Add(report);
            dbContext.SaveChanges();

            reportService.DeleteReport(report.Id);

            var reports = dbContext.ReportPosts.ToList();

            Assert.Empty(reports);
        }

        [Fact]
        public void GetAllShouldReturnAllReportsSortedByDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "DeleteReportShouldDeleteReportById")
                 .Options;

            var dbContext = new ApplicationDbContext(options);

            var reportService = new ReportService(dbContext);

            dbContext.ReportPosts.AddRange(new List<ReportPost>
            {
                new ReportPost{CreatedOn = DateTime.Now, Reason = "Reason" },
                new ReportPost{CreatedOn = DateTime.Now },
                new ReportPost{CreatedOn = DateTime.Now },
                new ReportPost{CreatedOn = DateTime.Now },
                new ReportPost{CreatedOn = DateTime.Now },
            });
            dbContext.SaveChanges();

            var Sort = reportService.GetAll().First();

            Assert.Equal("Reason", Sort.Reason);
        }
    }
}
