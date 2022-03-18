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
    public partial class TutorPage : ContentPage
    {

        readonly TutorFirebaseHelper firebaseHelper = new TutorFirebaseHelper(); 
       

        public TutorPage()
        {
            InitializeComponent();
           
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        //When add button is clicked
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //Tell the user if the form is not valid the field must be answered
            if (!IsFormValid())
            {
                await DisplayAlert("Error", "Required Fields must be entered", "OK");
                return;
            }
            // create an integer variable to hold the result returned by getTutor(which is to return a tutor with specified name)
            //basically means go find a tutor with the entered name
            //(if not found tutor will hold null)
            //if found tutor will hold the found Tutor
            var tutor = await firebaseHelper.GetTutor(TxtName.Text);

            // if the name is found inside the database return an error message
            //if tutor doesnt hold null print error
            if (tutor != null)
            {
                await DisplayAlert("Error", "A Tutor with this name already exists", "Ok");
                return;
            }
            //if the name is not found add it to the database
            //otherwise if tutor holds null enter that  Tutors by its variables

            await firebaseHelper.AddTutor(TxtName.Text, TxtPhone.Text, TxtDescription.Text, TxtAddress.Text, TxtCity.Text, TxtPostal.Text, TxtQualification.Text, TxtEmail.Text, TxtExperience.Text, TxtFees.Text, TxtSubjects.Text);

            //Make all fields empty after adding new Tutor
            TxtAddress.Text = string.Empty;
            TxtCity.Text = string.Empty;
            TxtDescription.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtExperience.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtPostal.Text = string.Empty;
            TxtQualification.Text = string.Empty;
            TxtFees.Text = string.Empty;
            TxtSubjects.Text = string.Empty;

            await DisplayAlert("Success", "Tutor Added successfully", "Ok");
            await FetchAllTutors();


        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (SelectedTutor == null)
            {
                await DisplayAlert("Error", "A Tutor must be selected", "Ok");
                return;
            }


            if (!IsFormValid())
            {
                await DisplayAlert("Error", "Please enter all fields", "OK");
                return;
            }

            // create a variable to store result returned by GetTutor by name method

            var tutor = await firebaseHelper.GetTutor(TxtAddress.Text);

            if (tutor != null && tutor.Id != SelectedTutor.Id)
            {
                await DisplayAlert("Error", "A Tutor with that name already exist", "OK");
                return;
            }



            await firebaseHelper.UpdateTutor(SelectedTutor.Id, TxtName.Text, TxtPhone.Text, TxtDescription.Text, TxtAddress.Text, TxtCity.Text, TxtPostal.Text, TxtQualification.Text, TxtEmail.Text, TxtExperience.Text, TxtFees.Text, TxtSubjects.Text);

            TxtAddress.Text = string.Empty;
            TxtCity.Text = string.Empty;
            TxtDescription.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtExperience.Text = string.Empty;
            TxtName.Text = string.Empty;
            TxtPhone.Text = string.Empty;
            TxtPostal.Text = string.Empty;
            TxtQualification.Text = string.Empty;
            TxtFees.Text = string.Empty;
            TxtSubjects.Text = string.Empty;

            await DisplayAlert("Success", "Tutor Updated Successfully", "OK");

            await FetchAllTutors();
        }


       /* private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (SelectedTutor == null)
            {
                await DisplayAlert("Error", "A Tutor must be selected to proceed", "OK");
                return;
            }

            await firebaseHelper.DeleteTutor(SelectedTutor.Id);

            await DisplayAlert("Success", "Tutor Deleted Successfully", "OK");

            await FetchAllTutors();
        }*/



        private async void LstTutors_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tutor = await firebaseHelper.GetTutor(SelectedTutor.Id);

            TxtName.Text = tutor.Name;
            TxtPhone.Text = tutor.Phone;

            TxtAddress.Text = tutor.AddressLine;
            TxtCity.Text = tutor.city;
            TxtDescription.Text = tutor.Description;
            TxtEmail.Text = tutor.Email;
            TxtExperience.Text = tutor.Experiance;
            TxtPostal.Text = tutor.postal;
            TxtQualification.Text = tutor.Qualification;
            TxtFees.Text = tutor.Fees;
            TxtSubjects.Text = tutor.Subjects;



        }

        private async Task FetchAllTutors()
        {
            var allTutors = await firebaseHelper.GetAllTutors();

            LstTutors.ItemsSource = allTutors;
        }


        private Tutor SelectedTutor => (Tutor)LstTutors.SelectedItem;

        private bool IsFormValid() => IsNameValid() && IsPhoneValid() && IsQualificationValid() && IsPostalValid() && IsEmail() && IsCityValid() && IsAddressValid() && IsExpValid() && IsFeesValid();

        private bool IsNameValid() => !string.IsNullOrEmpty(TxtName.Text);
        private bool IsPhoneValid() => !string.IsNullOrEmpty(TxtPhone.Text);
        private bool IsQualificationValid() => !string.IsNullOrEmpty(TxtDescription.Text);
        private bool IsEmail() => !string.IsNullOrEmpty(TxtEmail.Text);
        private bool IsAddressValid() => !string.IsNullOrEmpty(TxtAddress.Text);
        private bool IsCityValid() => !string.IsNullOrEmpty(TxtCity.Text);
        private bool IsPostalValid() => !string.IsNullOrEmpty(TxtPostal.Text);
        private bool IsExpValid() => !string.IsNullOrEmpty(TxtExperience.Text);
        private bool IsFeesValid() => !string.IsNullOrEmpty(TxtFees.Text);
       



    }
}