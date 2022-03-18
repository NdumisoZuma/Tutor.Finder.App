using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tutor_App.Model;
using System.Collections.Generic;
using Tutor_App.Helper;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentsList : ContentPage
    {

        StudentFirebaseHelper firebaseHelper = new StudentFirebaseHelper();



       

        public ObservableCollection<string> Items { get; set; }

        public StudentsList()
        {
            InitializeComponent();



            base.OnAppearing();
            



           
            

            
        }


        protected async   override void OnAppearing()
        {
            var students = firebaseHelper.GetAllStudents();
            
            MyListView.ItemsSource = await students;

        }







        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;


            var mydetails = e.Item as Student;

            await Navigation.PushAsync(new StudentProfile(mydetails.id, mydetails.Name, mydetails.City, mydetails.AddressLine, mydetails.Email, mydetails.Phone,  mydetails.School   ));

           /* var btn = (Button)sender;
            var act = (Student)btn.BindingContext;
            var id = act.id;

            var response = await DisplayActionSheet("Options", "Cancel", "Delete user", "Email Student", "Go To profile");

            if (response == "Go to profile")
            {
                await Navigation.PushModalAsync(new StudentProfile(id));
            }*/



            




          

            //Deselect Item
            ((ListView)sender).SelectedItem = null;






        }





        private async void LstTutors_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tutor = await firebaseHelper.GetStudent(SelectedStudent.id);

           


        }

        private Student SelectedStudent => (Student)MyListView.SelectedItem;








    }
}
