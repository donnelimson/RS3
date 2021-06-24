using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    [Serializable]
    public class QualificationReportViewModel
    {
        public List<KeyValuePair<string, string>> Eligibilities { get; set; }
        public string SelectedEligibilities { get; set; }

        public List<KeyValuePair<string, string>> Trainings { get; set; }
        public string SelectedTrainings { get; set; }

        public List<KeyValuePair<string, string>> CoursesMajors { get; set; }
        public string SelectedCoursesMajors { get; set; }
    }
}