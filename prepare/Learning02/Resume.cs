using System;

    public class Resume
    {
        // Fields or Attributes of Resume Object/Class
        public string _name;
        public List<Job> _jobs = new List<Job>();


         public void ShowResumeDetails() // Resume Method to show resume details
        {
            Console.WriteLine($"\nName: {_name}"); // Display Name
            Console.WriteLine("Jobs:");

            foreach( Job job in _jobs) // Loop through the JOB and set a job variable to hold the list of _jobs
            {
                // Show Job Detais
                //Console.WriteLine($"{job._jobTitle} ({job._company}) {job._startYear}-{job._endYear}");
                job.ShowJobDetails();
            }
            Console.WriteLine("");
        }
    }
