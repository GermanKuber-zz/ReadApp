using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Common.Models;
using Common.Repositorys;
using Common.ViewModels;

namespace BackgroundServices
{
    public sealed class ReadAppBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            var settings = new ConfigurationsViewModel();

            if (settings.NotificationsEnabled)
                await SendNotificationAsync();

            if (settings.LiveTileEnabled)
                await UpdateTilesAsync();

            _deferral.Complete();
        }

        private async Task SendNotificationAsync()
        {
            try
            {
                ReadRepository ReadRepository = new ReadRepository();
                var events = await ReadRepository.GetNextEvents();
                if (events == null && events.Count > 0)
                    return;

                ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
                XmlDocument content = ToastNotificationManager.GetTemplateContent(
                        ToastTemplateType.ToastImageAndText02);

                var texts = content.GetElementsByTagName("text");

                texts[0].InnerText = $"Usted tiene un evento próximo!!";

                texts[1].InnerText = $"El evento {events.First().Title} , se realizara el dia  {events.First().Date.ToLocalTime()}";

                var image = content.GetElementsByTagName("image");
                image[0].Attributes[1].InnerText = events.First().Image;

                notifier.Show(new ToastNotification(content));
     

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static async void Register()
        {
            //Recorro todas las task y verifico si se encuentra registrada
            var isRegistered = BackgroundTaskRegistration.AllTasks.Values.Any(
                t => t.Name == nameof(ReadAppBackgroundTask));

            if (isRegistered)
                return;
            //Pedimos acceso para registrarla, si el usuario no acepta se retorna
            if (await BackgroundExecutionManager.RequestAccessAsync()
                == BackgroundAccessStatus.Denied)
                return;

            //Registramos
            var builder = new BackgroundTaskBuilder
            {
                Name = nameof(ReadAppBackgroundTask),
                TaskEntryPoint = $"{nameof(BackgroundServices)}.{nameof(ReadAppBackgroundTask)}"
            };

            builder.SetTrigger(new TimeTrigger(120, false));

            builder.Register();
        }
        private async static Task UpdateTilesAsync()
        {
            var noticeInDay = await GetRandomNotice();
            if (noticeInDay == null)
                return;

            var template =
                    GenerateTemplate();

            var content = string.Format(template, noticeInDay.Title, noticeInDay.Date, noticeInDay.Image);
            var doc = new XmlDocument();
            doc.LoadXml(content);

            TileUpdateManager.CreateTileUpdaterForApplication().
                Update(new TileNotification(doc));

        }

        private static string GenerateTemplate()
        {
            //return TemplateContent.Simple;
            return TemplateContent.WithImage;
        }

        //Muestro una noticia random
        private static async Task<EventModel> GetRandomNotice()
        {
            ReadRepository ReadRepository = new ReadRepository();
            var noticeInDay = await ReadRepository.GetNextEvents();
            var random = new Random();
            var number = random.Next(0, noticeInDay.Count - 1);

            return noticeInDay[number];

        }
    }
}
