using System.Collections.Generic;
using seirin1.Data;

namespace seirin1.ViewModels
{
    public class DashboardViewModel
    {
        public List<Person> Data { get; set; }

        public DashboardViewModel()
        {
            Data = new List<Person>
            {
                new Person { Name = "David", Height = 170 },
                new Person { Name = "Michael", Height = 96 },
                new Person { Name = "Steve", Height = 65 },
                new Person { Name = "Joel", Height = 182 },
                new Person { Name = "Bob", Height = 134 }
            };
        }
    }
}
