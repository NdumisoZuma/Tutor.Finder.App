using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutor_App.Model;
using System.Threading.Tasks;
using Tutor_App.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectPage : ContentPage
    { 
        readonly SubjectFirebaseHelper firebaseHelper = new SubjectFirebaseHelper();
        public SubjectPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchAllSubjects();

        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!isFormValid())
            {
                await DisplayAlert("Error", "Name is a required field", "OK");
                return;
            }

            var subject = await firebaseHelper.GetSubject(TxtName.Text);

            if (subject != null)
            {
                await DisplayAlert("Error", "A subject with that name already exists", "Close");
                return;
            }

            await firebaseHelper.AddSubject(TxtName.Text, TxtLevel.Text);

            TxtName.Text = string.Empty;
            TxtLevel.Text = string.Empty;

            await DisplayAlert("Success", "Person Added Successfully", "Ok");

            await FetchAllSubjects();


        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (SelectedSubject == null)
            {
                await DisplayAlert("Error", "A subject must be selected to proceed ", "OK");
                return;
            }

            if (!isFormValid())
            {
                await DisplayAlert("Error", "Name and subject are required fields", "OK");
                return;
            }

            var subject = await firebaseHelper.GetSubject(TxtName.Text);

            if (subject != null && subject.Code != SelectedSubject.Code)
            {
                await DisplayAlert("Error", "A person with that name already exists", "OK");
                return;
            }

            await firebaseHelper.UpdateSubject(SelectedSubject.Code, TxtName.Text, TxtLevel.Text);


            TxtName.Text = string.Empty;
            TxtLevel.Text = string.Empty;
            await DisplayAlert("Success", "Person Updated Successfully", "OK");

            await FetchAllSubjects();
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (SelectedSubject == null)
            {
                await DisplayAlert("Error", "A Subject must be selected to proceed", "OK");
                return;
            }

            await firebaseHelper.DeleteSubject(SelectedSubject.Code);

            await DisplayAlert("Success", "Subject Deleted Successfully", "OK");

            await FetchAllSubjects();
        }

        private async void LstSubjects_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var subject = await firebaseHelper.GetSubject(SelectedSubject.Code);

            TxtName.Text = subject.Name;
            TxtLevel.Text = subject.Level;
        }

        private async Task FetchAllSubjects()
        {
            var allSubjects = await firebaseHelper.GetAllSubjects();
            LstSubjects.ItemsSource = allSubjects;
        }

        private Subject SelectedSubject => (Subject)LstSubjects.SelectedItem;

        private bool isFormValid() => isNameValid();

        private bool isNameValid() => !string.IsNullOrEmpty(TxtName.Text);





    }
}