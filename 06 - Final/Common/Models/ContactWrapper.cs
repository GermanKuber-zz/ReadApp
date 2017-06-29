using System;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Common.Models
{
    public class ContactWrapper
    {
        public Contact Contact { get; }

        public ContactWrapper(Contact contact)
        {
            Contact = contact;
        }

        public ContactWrapper(string firsName, string lastName)
        {
            Contact = new Contact
            {
                FirstName = firsName,
                LastName = lastName
            };
        }

        public string Initials => GetFirstCharacter(Contact.FirstName) +
            GetFirstCharacter(Contact.LastName);

        private string GetFirstCharacter(string s) =>
            string.IsNullOrEmpty(s) ? "" : s.Substring(0, 1);



        public ImageBrush Picture
        {
            get
            {
                if (Contact.SmallDisplayPicture == null) //No thumbnal
                    return null;

                var image = new BitmapImage();

                image.SetSource(Contact.SmallDisplayPicture.OpenReadAsync()
                    .GetAwaiter().GetResult());

                return new ImageBrush { ImageSource = image };
            }
        }
    }
}
