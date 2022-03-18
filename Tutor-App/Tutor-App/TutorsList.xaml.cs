using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_App.Model;
using Tutor_App.Helper;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorsList : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        TutorFirebaseHelper firebaseHelper = new TutorFirebaseHelper();

        public TutorsList()
        {
            InitializeComponent();
            base.OnAppearing();

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

           
        }

        protected async override void OnAppearing()
        {
            var tutors =  firebaseHelper.GetAllTutors();
            MyListView.ItemsSource = await tutors;
           
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var mydetails = e.Item as Tutor;

            await Navigation.PushAsync(new TutorProfile(mydetails.Id, mydetails.Name, mydetails.city, mydetails.Email, mydetails.AddressLine,  mydetails.Experiance, mydetails.postal,  mydetails.Description, mydetails.Qualification, mydetails.Fees, mydetails.Subjects));


           

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
