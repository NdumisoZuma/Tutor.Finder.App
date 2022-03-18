using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Tutor_App.Model;
using System.Threading.Tasks;
using System.Linq;

namespace Tutor_App.Helper
{
    public class StudentFirebaseHelper
    {

        //create table name
        private readonly string ChildName = "students";

        //assign firebase as access point to Firebase Database 
         readonly FirebaseClient firebase = new FirebaseClient("https://xamarin-app-68883.firebaseio.com/");

        // get all the students 
        public async Task<List<Student>> GetAllStudents()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Student>()).Select(item => new Student
                {
                    id = item.Object.id,
                    Name = item.Object.Name,
                    Email = item.Object.Email,
                    AddressLine = item.Object.AddressLine,
                    City = item.Object.City,
                    Phone = item.Object.Phone,
                    School = item.Object.School
                }).ToList();
        }
        public async Task AddStudent(string name, string phone , string school, string city, string address, string postal, string email)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Student() { id = Guid.NewGuid(), Name = name, Phone = phone, AddressLine = address, School = school, City = city, Email = email, Postal = postal });
        }



        public async Task<Student> GetStudent(Guid id)
        {
            var allStudents = await GetAllStudents();
            await firebase.Child(ChildName).OnceAsync<Student>();
            return allStudents.FirstOrDefault(a => a.id == id);
        }

        public async Task<Student> GetStudent(string name)
        {
            var allStudents = await GetAllStudents();
            await firebase.Child(ChildName).OnceAsync<Student>();
            return allStudents.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdateStudent(Guid id, string name, string email, string phone, string address, string postal, string school , string city)
        {

            var tutorToUpdate = (await firebase.Child(ChildName).OnceAsync<Student>()).FirstOrDefault(a => a.Object.id == id);

            await firebase.Child(ChildName).Child(tutorToUpdate.Key)
                .PutAsync(new Student() {

                    id = id,
                    Name = name,
                    Email = email,
                    AddressLine = address,
                    City =  city,
                    Phone = phone,
                    School = school,
                    Postal = postal
                });
        }

        public async Task DeleteStudent(Guid Id)
        {
            var toDeletePerson = (await firebase
                .Child(ChildName)
                .OnceAsync<Student>()).FirstOrDefault(a => a.Object.id == Id);
            await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
        }






    }
}
