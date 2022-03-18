using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor_App.Model;
using Tutor_App.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : ContentPage
    {
        StudentFirebaseHelper firebaseHelper = new StudentFirebaseHelper();

        
        public List<Student> students;

        public Student student { get; set; }

       
        public StudentProfile( Guid id, string Name, string city, string address, string Email, string phone,  string school)
        {

            InitializeComponent();
            
            student = new Student();

            TxtName.Text = Name;
            TxtAddress.Text = address;
            TxtCity.Text = city;
            txtEmail.Text = Email;
            TxtPhone.Text = phone;
            TxtSchool.Text = school;

        }

        

        //public StudentProfile(List<Student> Allstudents)
        //{
        //    foreach (var item in Allstudents)
        //    {

        //    }
        //}
    }
}