using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor_App.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Tutor_App.Model;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentPage : ContentPage
    {
       
      

        readonly StudentFirebaseHelper firebaseHelper = new StudentFirebaseHelper();
        public StudentPage()
        {
            InitializeComponent();
            
            this.BindingContext = this;
        }

       


        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                await DisplayAlert("Error", " All the fields are required", "OK");
                return;
            }

            var student = await firebaseHelper.GetStudent(TxtName.Text);

            if (student != null)
            {
                await DisplayAlert("Error", "A Student with that name already exist", "OK");
                return;
            }

            await firebaseHelper.AddStudent(TxtName.Text, TxtPhone.Text, TxtAddressLine.Text, TxtCity.Text, TxtEmail.Text, TxtPostal.Text, TxtSchool.Text);

            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtAddressLine.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtSchool.Text = string.Empty;
            TxtPostal.Text = string.Empty;
            TxtCity.Text = string.Empty;


            await DisplayAlert("Success", "Student Added Successfully", "OK");

            await FetchAllStudents();
        }


        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (SelectedStudent == null)
            {
                await DisplayAlert("Error", "A Student must be selected to proceed", "OK");
                return;
            }

            await firebaseHelper.DeleteStudent(SelectedStudent.id);

            await DisplayAlert("Success", "Student Deleted Successfully", "OK");

            await FetchAllStudents();
        }



        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (SelectedStudent == null)
            {
                await DisplayAlert("Error", "A person must be selected to proceed", "OK");
                return;
            }

            if (!IsFormValid())
            {
                await DisplayAlert("Error", "Name and person are required fields", "OK");
                return;
            }

            var student = await firebaseHelper.GetStudent(TxtName.Text);

            if (student != null && student.id != SelectedStudent.id)
            {
                await DisplayAlert("Error", "A Student with that name already exist", "OK");
                return;
            }

            await firebaseHelper.UpdateStudent(SelectedStudent.id, TxtName.Text, TxtPhone.Text, TxtAddressLine.Text, TxtCity.Text, TxtEmail.Text, TxtSchool.Text, TxtPostal.Text);

            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtAddressLine.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtSchool.Text = string.Empty;
            TxtPostal.Text = string.Empty;
            TxtCity.Text = string.Empty;

            await DisplayAlert("Success", "Student Updated Successfully", "OK");

            await FetchAllStudents();
        }



        private async void LstStudents_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var student = await firebaseHelper.GetStudent(SelectedStudent.id);

            TxtName.Text = student.Name;
            TxtPhone.Text = student.Phone;
            TxtAddressLine.Text = student.AddressLine;
            TxtEmail.Text = student.Email;
            TxtSchool.Text = student.School;
            TxtPostal.Text = student.Postal;
            TxtCity.Text = student.City;
        }



        private async Task FetchAllStudents()
        {
            var allStudents = await firebaseHelper.GetAllStudents();

            LstStudents.ItemsSource = allStudents;
        }



        private Student SelectedStudent => (Student)LstStudents.SelectedItem;

        private bool IsFormValid() => IsNameValid() && IsPhoneValid() && IsAddressValid()  && IsEmailValid()   && IscityValid()  && IsSchoolValid() && IsPostValid() ;

        private bool IsNameValid() => !string.IsNullOrWhiteSpace(TxtName.Text);

        private bool IsPhoneValid() => !string.IsNullOrWhiteSpace(TxtPhone.Text);

        private bool IsEmailValid() => !string.IsNullOrWhiteSpace(TxtEmail.Text);
        private bool IsAddressValid() => !string.IsNullOrWhiteSpace(TxtAddressLine.Text);
        private bool IsSchoolValid() => !string.IsNullOrWhiteSpace(TxtSchool.Text);
        private bool IscityValid() => !string.IsNullOrWhiteSpace(TxtCity.Text);
        private bool IsPostValid() => !string.IsNullOrWhiteSpace(TxtPostal.Text);







    }
}