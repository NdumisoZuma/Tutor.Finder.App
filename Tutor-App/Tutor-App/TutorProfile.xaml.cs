using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_App.Helper;
using Tutor_App.Model;

namespace Tutor_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorProfile : ContentPage
    {

        TutorFirebaseHelper firebaseHelper = new TutorFirebaseHelper();
        Tutor tutor = new Tutor(); 
        
        public TutorProfile(Guid id, string Name, string City, string Email, string address, string phone, string exp, string description, string qualification, string fees, string Subjects )
        {
            InitializeComponent();
            TxtName.Text = Name;
            TxtCity.Text = City;
            TxtEmail.Text  = Email;
            TxtAddress.Text = address;
            TxtFees.Text = fees;
            TxtPhone.Text = phone;
            TxtExp.Text = exp;
            TxtDesc.Text = description;
            TxtQualifications.Text = qualification;
            TxtSubjects.Text = Subjects;
            

            


        }

        /*private async void BtnEmail_Clicked(object sender, EventArgs e)
        {
            var email = await firebaseHelper.GetTutor(BtnEmail.Text);

            
        }*/
    }
}