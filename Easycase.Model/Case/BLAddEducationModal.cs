using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Case
{
    public class BLAddEducationModal
    {
        public EducationHistory EducationHistory { get; set; }
        public BLSpouseEducation SpouseEducation { get; set; }
        public string ECACompleted { get; set; }
        public string CICEnglishExam { get; set; }
        public string CICEnglishExamSpouse { get; set; }
        public long CaseId { get; set; }
        public long EducationId { get; set; }
    }

    public class EducationHistory
    {
        public string[] Level { get; set; }
        public string[] FieldOfStudy { get; set; }
        public string[] Institution { get; set; }
        public string[] Duration { get; set; }
        public string[] Country { get; set; }
    }
    public class BLSpouseEducation
    {
        public string[] Level { get; set; }
        public string[] FieldOfStudy { get; set; }
        public string[] Institution { get; set; }
        public string[] Duration { get; set; }
        public string[] Country { get; set; }
    }
}
