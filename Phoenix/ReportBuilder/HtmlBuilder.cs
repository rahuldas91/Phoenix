using Phoenix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ReportBuilder
{
    public class HtmlBuilder
    {
        public string GenerateHTMLReport(PhoenixReport report)
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html lang=\"en\">");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<meta charset=\"UTF-8\">");
            htmlBuilder.Append("<title>Test Report</title>");
            htmlBuilder.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            htmlBuilder.Append("<!-- Bootstrap CDN -->");
            htmlBuilder.Append("<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\">");
            htmlBuilder.Append("<!-- Font Awesome CDN -->");
            htmlBuilder.Append("<link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css\">");
            htmlBuilder.Append("<!-- Chart.js CDN -->");
            htmlBuilder.Append("<script src=\"https://cdn.jsdelivr.net/npm/chart.js\"></script>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append(".test-list { max-height: 80vh; overflow-y: auto; }");
            htmlBuilder.Append(".test-details { display: none; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body class=\"bg-light text-dark p-4\">");
            htmlBuilder.Append("<div class=\"container\">");
            htmlBuilder.Append("<h1 class=\"text-center mb-4\">Test Report</h1>");

            htmlBuilder.Append(GenerateTabs());
            htmlBuilder.Append("<div class=\"tab-content\" id=\"myTabContent\">");
            htmlBuilder.Append(GenerateDashboardTab(report));
            htmlBuilder.Append(GenerateTestsTab(report));
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</div>");

            htmlBuilder.Append("<!-- Optional Bootstrap JS -->");
            htmlBuilder.Append("<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js\"></script>");
            htmlBuilder.Append(GenerateJavaScript(report));
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            return htmlBuilder.ToString();
        }

        private string GenerateTabs()
        {
            var tabsBuilder = new StringBuilder();
            tabsBuilder.Append("<ul class=\"nav nav-tabs\" id=\"myTab\" role=\"tablist\">");
            tabsBuilder.Append("<li class=\"nav-item\" role=\"presentation\">");
            tabsBuilder.Append("<button class=\"nav-link active\" id=\"dashboard-tab\" data-bs-toggle=\"tab\" data-bs-target=\"#dashboard\" type=\"button\" role=\"tab\" aria-controls=\"dashboard\" aria-selected=\"true\">Dashboard</button>");
            tabsBuilder.Append("</li>");
            tabsBuilder.Append("<li class=\"nav-item\" role=\"presentation\">");
            tabsBuilder.Append("<button class=\"nav-link\" id=\"tests-tab\" data-bs-toggle=\"tab\" data-bs-target=\"#tests\" type=\"button\" role=\"tab\" aria-controls=\"tests\" aria-selected=\"false\">Tests</button>");
            tabsBuilder.Append("</li>");
            tabsBuilder.Append("</ul>");
            return tabsBuilder.ToString();
        }

        private string GenerateDashboardTab(PhoenixReport report)
        {
            var dashboardBuilder = new StringBuilder();
            dashboardBuilder.Append("<div class=\"tab-pane fade show active\" id=\"dashboard\" role=\"tabpanel\" aria-labelledby=\"dashboard-tab\">");
            dashboardBuilder.Append("<div class=\"row mt-4\">");
            dashboardBuilder.Append(GenerateDashboardCard("Total Tests", report.Tests.Count.ToString()));
            dashboardBuilder.Append(GenerateDashboardCard("Project Name", report.ProjectName));
            dashboardBuilder.Append(GenerateDashboardCard("Suite Name", report.SuiteName));
            dashboardBuilder.Append(GenerateDashboardCard("Report Generated On", report.Timestamp.ToString()));
            dashboardBuilder.Append("<div class=\"col-md-6\">");
            dashboardBuilder.Append("<div class=\"card mb-4 shadow-sm\">");
            dashboardBuilder.Append("<div class=\"card-body\">");
            dashboardBuilder.Append("<h5 class=\"card-title\">Test Status</h5>");
            dashboardBuilder.Append("<canvas id=\"testStatusChart\"></canvas>");
            dashboardBuilder.Append("</div>");
            dashboardBuilder.Append("</div>");
            dashboardBuilder.Append("</div>");
            dashboardBuilder.Append("</div>");
            dashboardBuilder.Append("</div>");
            return dashboardBuilder.ToString();
        }

        private string GenerateDashboardCard(string title, string content)
        {
            var cardBuilder = new StringBuilder();
            cardBuilder.Append("<div class=\"col-md-6\">");
            cardBuilder.Append("<div class=\"card mb-4 shadow-sm\">");
            cardBuilder.Append("<div class=\"card-body\">");
            cardBuilder.Append($"<h5 class=\"card-title\">{title}</h5>");
            cardBuilder.Append($"<p class=\"card-text\">{content}</p>");
            cardBuilder.Append("</div>");
            cardBuilder.Append("</div>");
            cardBuilder.Append("</div>");
            return cardBuilder.ToString();
        }

        private string GenerateTestsTab(PhoenixReport report)
        {
            var testsBuilder = new StringBuilder();
            testsBuilder.Append("<div class=\"tab-pane fade\" id=\"tests\" role=\"tabpanel\" aria-labelledby=\"tests-tab\">");
            testsBuilder.Append("<div class=\"row mt-4\">");
            testsBuilder.Append("<div class=\"col-md-4\">");
            testsBuilder.Append("<input type=\"text\" id=\"search\" class=\"form-control mb-3\" placeholder=\"Search tests...\">");
            testsBuilder.Append("<div class=\"list-group test-list\">");

            foreach (var test in report.Tests)
            {
                testsBuilder.Append($"<a href=\"#\" class=\"list-group-item list-group-item-action\" data-test-id=\"{test.GUID}\"><i class=\"fas fa-vial\"></i> {test.Name}</a>");
            }

            testsBuilder.Append("</div>");
            testsBuilder.Append("</div>");
            testsBuilder.Append("<div class=\"col-md-8\">");

            foreach (var test in report.Tests)
            {
                testsBuilder.Append(GenerateTestDetails(test));
            }

            testsBuilder.Append("</div>");
            testsBuilder.Append("</div>");
            testsBuilder.Append("</div>");
            return testsBuilder.ToString();
        }

        private string GenerateTestDetails(PhoenixTest test)
        {
            var testDetailsBuilder = new StringBuilder();
            testDetailsBuilder.Append($"<div class=\"test-details\" id=\"test-{test.GUID}\">");
            testDetailsBuilder.Append($"<h3><i class=\"fas fa-vial\"></i> {test.Name}</h3>");
            testDetailsBuilder.Append($"<p><i class=\"fa-solid fa-user-tie\"></i> <strong>Author:</strong> {test.Author}</p>");
            testDetailsBuilder.Append($"<p><strong>Description:</strong> {test.Description}</p>");
            testDetailsBuilder.Append($"<p><i class=\"fa-solid fa-bug\"></i> <strong>Defect Link:</strong> <a href='{test.DefectLink}'>{test.DefectLink}</a></p>");
            testDetailsBuilder.Append($"<p><i class=\"fa-solid fa-hourglass\"></i> <strong>Timestamp:</strong> {test.Timestamp}</p>");
            testDetailsBuilder.Append("<p><i class=\"fa-solid fa-tags\"></i> <strong>Tags:</strong> " + string.Join(", ", test.Tags) + "</p>");
            testDetailsBuilder.Append("<h4 class=\"mt-4\"><i class=\"fas fa-list\"></i> Events</h4>");

            foreach (var evt in test.Events)
            {
                testDetailsBuilder.Append(GenerateEventDetails(evt));
            }

            testDetailsBuilder.Append("</div>");
            return testDetailsBuilder.ToString();
        }

        private string GenerateEventDetails(PhoenixEvent evt)
        {
            var eventDetailsBuilder = new StringBuilder();
            eventDetailsBuilder.Append("<div class=\"bg-light border-start border-4 border-secondary ps-3 py-2\">");
            eventDetailsBuilder.Append($"<p><strong>Type:</strong> {evt.Type}</p>");
            eventDetailsBuilder.Append($"<p><strong>Message:</strong> {evt.Message}</p>");
            eventDetailsBuilder.Append($"<p><strong>Timestamp:</strong> {evt.Timestamp}</p>");
            eventDetailsBuilder.Append($"<p><strong>Status:</strong> {evt.Status}</p>");
            if (evt.Screenshot != null)
            {
                eventDetailsBuilder.Append($"<p><strong>Screenshot:</strong> <img src='{evt.Screenshot}' alt='Screenshot' /></p>");
            }
            eventDetailsBuilder.Append("</div>");
            return eventDetailsBuilder.ToString();
        }

        private string GenerateJavaScript(PhoenixReport report)
        {
            var jsBuilder = new StringBuilder();
            jsBuilder.Append("<script>");
            jsBuilder.Append("document.addEventListener('DOMContentLoaded', function() {");
            jsBuilder.Append("const testLinks = document.querySelectorAll('.list-group-item');");
            jsBuilder.Append("const testDetails = document.querySelectorAll('.test-details');");
            jsBuilder.Append("const searchInput = document.getElementById('search');");

            jsBuilder.Append("testLinks.forEach(link => {");
            jsBuilder.Append("link.addEventListener('click', function(e) {");
            jsBuilder.Append("e.preventDefault();");
            jsBuilder.Append("const testId = this.getAttribute('data-test-id');");
            jsBuilder.Append("testDetails.forEach(detail => {");
            jsBuilder.Append("detail.style.display = 'none';");
            jsBuilder.Append("});");
            jsBuilder.Append("document.getElementById('test-' + testId).style.display = 'block';");
            jsBuilder.Append("});");
            jsBuilder.Append("});");

            jsBuilder.Append("searchInput.addEventListener('input', function() {");
            jsBuilder.Append("const searchTerm = this.value.toLowerCase();");
            jsBuilder.Append("testLinks.forEach(link => {");
            jsBuilder.Append("const testName = link.textContent.toLowerCase();");
            jsBuilder.Append("if (testName.includes(searchTerm)) {");
            jsBuilder.Append("link.style.display = 'block';");
            jsBuilder.Append("} else {");
            jsBuilder.Append("link.style.display = 'none';");
            jsBuilder.Append("}");
            jsBuilder.Append("});");
            jsBuilder.Append("});");

            // Chart.js for Test Status
            jsBuilder.Append("const ctxStatus = document.getElementById('testStatusChart').getContext('2d');");
            jsBuilder.Append("const testStatusChart = new Chart(ctxStatus, {");
            jsBuilder.Append("type: 'pie',");
            jsBuilder.Append("data: {");
            jsBuilder.Append("labels: ['Passed', 'Failed'],");
            jsBuilder.Append("datasets: [{");
            jsBuilder.Append("data: [");
            jsBuilder.Append($"{report.Tests.Count(t => t.Status)},");
            jsBuilder.Append($"{report.Tests.Count(t => !t.Status)}");
            jsBuilder.Append("],");
            jsBuilder.Append("backgroundColor: ['#28a745', '#dc3545'],");
            jsBuilder.Append("}]");
            jsBuilder.Append("},");
            jsBuilder.Append("options: {");
            jsBuilder.Append("responsive: true,");
            jsBuilder.Append("plugins: {");
            jsBuilder.Append("legend: {");
            jsBuilder.Append("position: 'top',");
            jsBuilder.Append("},");
            jsBuilder.Append("}");
            jsBuilder.Append("}");
            jsBuilder.Append("});");

            jsBuilder.Append("});");
            jsBuilder.Append("</script>");
            return jsBuilder.ToString();
        }
    }
}
