namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data.Models;

    public interface IReportService
    {
        void CreateReport(string report, string postId, string reportedUserId, string reporterId);

        void DeleteReport(string id);

        void DeleteAllReports();

        ReportPost GetById(string id);

        IQueryable<ReportPost> GetAll();

        IQueryable<ReportPost> GetReportsByPostId(string id);
    }
}
