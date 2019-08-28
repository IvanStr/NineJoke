namespace NineJoke.Services
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext context;

        public ReportService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateReport(string reason, string postId, string reportedUserId, string reporterId)
        {
            var report = new ReportPost
            {
                Reason = reason,
                PostId = postId,
                UploaderId = reportedUserId,
                ReporterId = reporterId,
                CreatedOn = DateTime.Now,
            };

            this.context.ReportPosts.Add(report);
            this.context.SaveChanges();
        }

        public void DeleteAllReports()
        {
            var allReports = this.context.ReportPosts;

            this.context.RemoveRange(allReports);
            this.context.SaveChanges();
        }

        public void DeleteReport(string id)
        {
            var report = this.GetById(id);

            this.context.ReportPosts.Remove(report);
            this.context.SaveChanges();
        }

        public IQueryable<ReportPost> GetAll()
        {
            return this.context.ReportPosts
                .Include(x => x.Post)
                .Include(x => x.Uploader)
                .Include(x => x.Reporter)
                .OrderBy(x => x.CreatedOn);
        }

        public ReportPost GetById(string id)
        {
            return this.context.ReportPosts.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<ReportPost> GetReportsByPostId(string id)
        {
            return this.context.ReportPosts.Where(x => x.PostId == id);
        }
    }
}
