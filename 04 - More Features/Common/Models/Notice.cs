using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Common.Models
{
    public class EventModel
    {
        public List<string> Tags { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public bool Emails { get; set; } = false;

        public Visibility EmailVisibility
        {
            get
            {
                if (Emails)
                    return Visibility.Visible;

                return Visibility.Collapsed;

            }
        }
    }
}