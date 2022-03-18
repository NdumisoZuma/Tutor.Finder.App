using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Tutor_App.Model;

namespace Tutor_App.Helper
{
   public class TutorFirebaseHelper
    {

        private readonly string ChildName = "Tutors";

        readonly FirebaseClient firebase = new FirebaseClient("https://xamarin-app-68883.firebaseio.com/");

        public async Task<List<Tutor>> GetAllTutors()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Tutor>()).Select(item => new Tutor
                {
                    Name = item.Object.Name,
                    Id = item.Object.Id,
                    Phone = item.Object.Phone,
                    Description = item.Object.Description,
                    AddressLine = item.Object.AddressLine,
                    city = item.Object.city,
                    postal = item.Object.postal,
                    Qualification = item.Object.Qualification,
                    Email = item.Object.Email,
                    Experiance = item.Object.Experiance,
                    Fees = item.Object.Fees,
                    Subjects = item.Object.Subjects

                }).ToList();
        }

        public async Task AddTutor(string name, string phone,
            string description, string addressLine, string city, string postal, string qualification,
            string email, string experiance , string fees, string Subjects)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Tutor() { Id = Guid.NewGuid(), Name = name, AddressLine = addressLine, city = city, Description = description, Email = email, Experiance = experiance, Fees = fees, Phone = phone, postal = postal, Qualification = qualification, Subjects =Subjects });

        }

        public async Task<Tutor> GetTutor(Guid id)
        {
            //create variable to store all Tutors
            var allTutors = await GetAllTutors();
            //asynchrounousely access firebase
            await firebase
                // Look for the child of the data route
                .Child(ChildName)
                .OnceAsync<Tutor>();
            return allTutors.FirstOrDefault(a => a.Id == id);
        }
        //Finding a tutor name inside firebase list of all Tutors
        // Tutor name entered 
        public async Task<Tutor> GetTutor(string name)
        {
            var allTutors = await GetAllTutors();
            await firebase
                .Child(ChildName)
                .OnceAsync<Tutor>();
            return allTutors.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdateTutor(Guid id , string name, string decription, string email, string phone, string addressLine, string fees, string city, string postal, string qualification, string experiance, string subjects)
        {
            var toUpdateTutor = (await firebase
                .Child(ChildName)
                .OnceAsync<Tutor>()).FirstOrDefault(a => a.Object.Id == id);

            await firebase
                .Child(ChildName)
                .Child(toUpdateTutor.Key)
                .PutAsync(new Tutor()
                {
                    Id = id,
                    Name = name,
                    Description = decription,
                    Email = email,
                    AddressLine = addressLine,
                    Experiance = experiance,
                    city = city,
                    postal = postal,
                    Fees = fees,
                    Phone = phone,
                    Qualification = qualification,
                    Subjects = subjects



                });
        }

        public async Task DeleteTutor(Guid Id)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Tutor>()).FirstOrDefault(a => a.Object.Id == Id);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }






    }
}
