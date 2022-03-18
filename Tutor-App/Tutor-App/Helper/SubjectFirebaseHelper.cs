using System;
using System.Collections.Generic;
using System.Text;
using Tutor_App.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace Tutor_App
{
   public class SubjectFirebaseHelper
     {
            private readonly string ChildName = "Subjects";

            readonly FirebaseClient firebase = new FirebaseClient("https://xamarin-tutorapp-default-rtdb.firebaseio.com/");

            
            public async Task<List<Subject>> GetAllSubjects()
             {
                return (await firebase
                .Child(ChildName)
                .OnceAsync<Subject>()).Select(item => new Subject
                {
                    Name = item.Object.Name,
                    Code = item.Object.Code,
                    Level = item.Object.Level



                }).ToList();
             }


        public async Task AddSubject(string name, string level)
        {
            await firebase
                .Child(ChildName)
                .PostAsync(new Subject() { Code = Guid.NewGuid(), Name = name, Level = level });
        }

        //Get subject by ID 
        //returns all subject containing enetered id
        public async Task<Subject> GetSubject(Guid code)
        {
            var allSubjects = await GetAllSubjects();
            await firebase
                .Child(ChildName)
                .OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.Code == code);
        }

        // Gets subject by name 
        // returns all subjects containg entered Name
        public async Task<Subject> GetSubject (string name)
        {
            var allSubjects = await GetAllSubjects();
            await firebase
                .Child(ChildName)
                .OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdateSubject(Guid code, string name, string level )
        {
            //First gets subject by id from firebase to update
            var toUpdateSubject = (
                await firebase
                .Child(ChildName)
                .OnceAsync<Subject>()).FirstOrDefault(a => a.Object.Code == code);

            await firebase
                .Child(ChildName)
                .Child(toUpdateSubject.Key)
                .PutAsync(new Subject()
                {
                    Code = code,
                    Name = name,
                    Level = level
                });
        }

        public async Task DeleteSubject(Guid code)
        {
            var toDeleteSubject = (await firebase
                .Child(ChildName)
                .OnceAsync<Subject>()).FirstOrDefault(a => a.Object.Code == code);
            await firebase.Child(ChildName).Child(toDeleteSubject.Key).DeleteAsync();
        }

         
    }
}
