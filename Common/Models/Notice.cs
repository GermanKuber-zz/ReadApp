using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Common.Models
{
    public class NoticeModel
    {
        public List<string> Tags { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }

        public DateTime DateParse
        {
            get
            {
                //Se parsea fecha recibida por el servicio a tipo DateTime
                DateTime dateParse;
                if (DateTime.TryParse(Date, out dateParse))
                {
                    return dateParse;
                }
                return new DateTime();
            }
        }
        public Visibility EmailVisibility
        {
            get
            {
                if (this.DateParse.Year.ToString().Equals(DateTime.Now.Year.ToString()))
                    return Visibility.Visible;

                return Visibility.Collapsed;

            }
        }
    }
}