using System.Reflection.Metadata.Ecma335;

namespace Management_System_App.Models
{
    public class student
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public int currentYear { get; set; }

        public bool isGraduated { get; set; }

        public student()
        {
        }
    }
}
