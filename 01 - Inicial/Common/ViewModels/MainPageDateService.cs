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

     


        #region Public Methods

        public async Task SendEmailAsync(EventModel notice, string email)
        {
            EmailAdmin emailAdmin = new EmailAdmin();
            //ContactAdmin contactAdmin = new ContactAdmin();
            //var contact = await contactAdmin.GetAllContacts();
            if (notice.Title != null)
                await emailAdmin.SendEmailAsync(email, notice.Title, notice.Text);
        }

        public async void AddNoticeToCalendarAsync(EventModel notice)
        {
            if (notice == null)
                throw new ArgumentNullException(nameof(notice));

            var appointment = new Appointment();
            appointment.Subject = "Evento : " + notice.Title;
            appointment.AllDay = true;
            appointment.BusyStatus = AppointmentBusyStatus.Free;
            var dateThisYear = new DateTime(
                DateTime.Now.Year, notice.Date.Month, notice.Date.Day);
            appointment.StartTime = dateThisYear;

            await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
        }
        #endregion
    }
}
