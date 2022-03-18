using System;
using System.Collections.Generic;
using System.Text;

namespace Tutor_App.Model
{
   public  class Student
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
    }
}
