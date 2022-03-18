using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;

namespace Tutor_App.Helper
{
   public class FirebaseStorageHelper
    {

    FirebaseStorage firebaseStorage = new FirebaseStorage("xamarin-tutorapp.appspot.com");

            //upload file to firebase

        public async Task<string> UploadFile(Stream fileStream, string fileName)
            {
                var imageUrl = await firebaseStorage
                    .Child("XamerinTutor")
                    .Child(fileName)
                    .PutAsync(fileStream);
                 return imageUrl;
            }

        //Get file from FireBase

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("XamarinTutor")
                .Child(fileName)
                .GetDownloadUrlAsync(); 
        }


        //Delete File in Firebase
        public async Task DeleteFile(string fileName)
        {
            await firebaseStorage
                .Child("xamarinTutor")
                .Child(fileName)
                .DeleteAsync();
        }









    }
}
