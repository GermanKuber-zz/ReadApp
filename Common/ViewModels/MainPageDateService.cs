using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Appointments;
using Common.Admins;
using Common.ForViews;
using Common.Models;
using Common.Repositorys;
using Windows.UI.Xaml;

namespace Common.ViewModels
{
    public class MainPageDateService {
        //Sacar

        public DateTime RandomDay()
        {

            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }


        #region Public Methods

        public async Task SendEmailAsync(NoticeModel notice)
        {
            EmailAdmin emailAdmin = new EmailAdmin();
            ContactAdmin contactAdmin = new ContactAdmin();
            var contact = await contactAdmin.GetAllContacts();
            if (notice.Title != null)
                await emailAdmin.SendEmailAsync(contact.First(x => x.Contact.Emails?.Count > 0).Contact, notice.Title, notice.Text);
        }

        public async void AddNoticeToCalendarAsync(NoticeModel notice)
        {
            if (notice == null)
                throw new ArgumentNullException(nameof(notice));

            var appointment = new Appointment();
            appointment.Subject = "Evento : " + notice.Title;
            appointment.AllDay = true;
            appointment.BusyStatus = AppointmentBusyStatus.Free;
            var dateThisYear = new DateTime(
                DateTime.Now.Year, notice.DateParse.Month, notice.DateParse.Day);
            appointment.StartTime =
                dateThisYear < DateTime.Now ? dateThisYear.AddYears(1) : dateThisYear;

            await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
        }
        #endregion
    }
}
