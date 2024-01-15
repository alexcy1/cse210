using System;

    public class Job
    {
        // Fields or Attributes of Job Object/Class
        public string  _company;
        public string  _jobTitle;
        public int  _startYear;
        public int  _endYear;

        public void ShowJobDetails() // Job details method to show job details
        {
            // Show Job details 
            Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
        }
    }
