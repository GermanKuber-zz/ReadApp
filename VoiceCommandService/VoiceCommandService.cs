using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;
using Common.Repositorys;

namespace VoiceCommandService
{
    public sealed class VoiceCommandService : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;

            var triggerDetails = taskInstance.TriggerDetails
                as AppServiceTriggerDetails;

            if (triggerDetails?.Name != nameof(VoiceCommandService))
                return;

            var connection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

            connection.VoiceCommandCompleted += ConnectionOnVoiceCommandCompleted;

            var command = await connection.GetVoiceCommandAsync();

            switch (command.CommandName)
            {
                case "ReadEventsByCortanaCommand":
                    await HandleReadNamedaysCommandAsync(connection);
                    break;
            }


            deferral.Complete();
        }

        private static async Task HandleReadNamedaysCommandAsync(VoiceCommandServiceConnection connection)
        {
            //Genero un mensaje de espera para que el usuario vea
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = "Los eventos que se realizan el dia de hoy son";
            userMessage.SpokenMessage = "Los eventos que se realizan el dia de hoy son";
            var response = VoiceCommandResponse.CreateResponse(userMessage);
            await connection.ReportProgressAsync(response);

            var today = DateTime.Now.Date;
            var notices = await ReadResitory.GetNoticesInDay();


            if (notices.Count() > 1)
            {
                userMessage.SpokenMessage =
                    userMessage.DisplayMessage =
                        $"El dia de hoy se realizan {notices.Count} eventos";

                var tile = new VoiceCommandContentTile();
                tile.ContentTileType = VoiceCommandContentTileType.TitleOnly;
                var titleList = new List<VoiceCommandContentTile>();
                var count = 0;
                foreach (var noticeModel in notices)
                {
                    if (count <= 5)
                    {
                        titleList.Add(new VoiceCommandContentTile
                        {
                            Title = noticeModel.Title.ToString(),
                            ContentTileType = VoiceCommandContentTileType.TitleWithText,
                            TextLine1 = noticeModel.Date

                        });
                        ++count;
                    }
                }
                response = VoiceCommandResponse.CreateResponse(userMessage, titleList);
                await connection.ReportProgressAsync(response);
            }
            else
            {
                if (notices != null)
                {
                    userMessage.SpokenMessage =
                     userMessage.DisplayMessage =
                         $"El evento que se realiza hoy es {notices.First().Title} eventos";
                    response = VoiceCommandResponse.CreateResponse(userMessage);
                }
            }

            await connection.ReportSuccessAsync(response);
        }

        private void ConnectionOnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
        }
    }
}
