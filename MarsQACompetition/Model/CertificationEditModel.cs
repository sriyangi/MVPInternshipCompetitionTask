using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQACompetition.Model
{
    public class CertificationEditModel
    {
        public string certificationName { get; set; }
        public string certificationBody { get; set; }
        public string graduationYear { get; set; }
        public string oldCertificationName { get; set; }
        public string oldCertificationBody { get; set; }
        public string oldGraduationYear { get; set; }

    }
}
