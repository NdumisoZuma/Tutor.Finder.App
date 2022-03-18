using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor_App;
using Tutor_App.Model;
using Tutor_App.Helper;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async void BtnStudents_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentsList());
        }

        async void BtnTutors_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorsList());
        }





    }
}