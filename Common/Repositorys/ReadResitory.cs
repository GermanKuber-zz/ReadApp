using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Common.Models;
using Newtonsoft.Json;
using Common.ViewModels;

namespace Common.Repositorys
{
    public class ReadRepository
    {
        private List<CommunityModel> _readCache;

        public ReadRepository()
        {

        }
        public async Task GenerateAsync()
        {
            if (_readCache == null)
                _readCache = GenerateDataDummy();
        }
        public async Task<List<CommunityModel>> GetAllAsync()
        {
            await GenerateAsync();
            return _readCache;
        }

        public async Task<List<CommunityModel>> GetByNameAsync(string name)
        {
            await GenerateAsync();
            return _readCache.Where(d => d.Name.ToLowerInvariant()
                .Contains(name.ToLowerInvariant()))
                .ToList(); ;
        }



        public async Task<List<EventModel>> GetNoticesInDay()
        {

            var now = DateTime.Now;

            var selectedChildren = _readCache.SelectMany(x => x.Events).Where(s => s.Date.Day == now.Day).ToList();

            return selectedChildren;
            // return await ReadFromFile();
        }

        public async Task<List<CommunityModel>> ReadFromFile()
        {
            //El archivo data.json debe esta en : ReadApp\bin\x86\Debug\AppX\data.json
            StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///data.json"));
            var file = File.ReadAllText(storageFile.Path);
            var listReturn = JsonConvert.DeserializeObject<List<CommunityModel>>(file);
            return listReturn;
        }
        public List<CommunityModel> GenerateDataDummy()
        {

            var local = new List<CommunityModel> {
            new CommunityModel{
                                Name = "[Net-Baires] Comunidad de .NET en Buenos Aires",
                                Link = "https://www.meetup.com/es-ES/Net-Baires/",
                                Picture ="https://secure.meetupstatic.com/photos/event/9/6/6/c/600_460418508.jpeg",
                                Email = "net-baires@outlook.com",
                                Events = new List<EventModel>{
                                         new EventModel{
                                              Emails= true,
                                        Date = DateTime.Now.AddDays(-40),
                                    Title="MeetUp Mensual Agosto ",
                                    Image="http://ourcodeworld.com/public-media/gallery/categorielogo-5713d627ccabf.png",
                                    Tags = new List<string>{
                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    },
                                    new EventModel{
                                        Date = DateTime.Now.AddDays(3),
                                    Title=".NET Conf AR v2017",
                                    Emails= true,
                                    Image="https://secure.meetupstatic.com/photos/event/9/d/8/2/highres_279340322.jpeg",
                                    Tags = new List<string>{
                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    },  new EventModel{
                                        Date =    DateTime.Now.AddDays(9),
                                    Title="Net-Baires Reunión Mensual",
                                    Image="https://secure.meetupstatic.com/photos/event/c/6/5/8/highres_456650776.jpeg",
                                    Tags = new List<string>{
                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="Este evento es un complemento para #XamarinDevDaysEl formato de evento es similiar a Dev Days, pero más corto, la agenda es-30 mins Registro-30 mins Introducción a Xamarin -50 mins Presentación del tema de la sesión-10 mins Descanso-120 mins Taller asistido (Hands On Lab)En tema del taller va a ser Xamarin y Azure, donde vamos a desarrolar una app desde cero utilizando servicios de Azure como notification hubs.Traído a ustedes gracias a• Sebastián Pérez(https://twitter.com/garudaslap)• Germán Kuber(https://twitter.com/germankube Leonardo Micheloni(https://twitter.com/leomicheloni)Es requisito tener el ambiente con Xamarin funcionando y conocimientos básicos de Xamarin. Nota: Por favor anotar el DNI porque es requisito para ingresar a las oficinas de Microsoft."
                                    },new EventModel{
                                        Date =    DateTime.Now.AddDays(20),
                                    Title="Xamarin Fest ",
                                    Image="https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png",
                                    Tags = new List<string>{

                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    }
                                }
                        },
             new CommunityModel{
                                Name = "MeetUp.Js",
                                Link = "https://www.meetup.com/es-ES/Meetup-js/",
                                Picture ="http://blob.todoexpertos.com/topics/lg/219.jpg",
                                Email = "meetup-js@outlook.com",
                                Events = new List<EventModel>{
                                    new EventModel{
                                        Date =    DateTime.Now.AddDays(3),
                                    Title=".Js Conf AR v2017",
                                    Image="https://www.codementor.io/assets/page_img/learn-javascript.png",
                                    Tags = new List<string>{
                                                 "Tecnología","Js","MeetUp.Js"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    },  new EventModel{
                                         Emails= true,
                                        Date =    DateTime.Now.AddDays(9),
                                    Title="Meetup.js Reunión Mensual",
                                    Image="https://secure.meetupstatic.com/photos/event/c/6/5/8/highres_456650776.jpeg",
                                    Tags = new List<string>{
                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    },new EventModel{
                                         Emails= true,
                                        Date =    DateTime.Now.AddDays(20),
                                    Title="JavaScript Day",
                                    Image="http://blob.todoexpertos.com/topics/lg/219.jpg",
                                    Tags = new List<string>{

                                                 "Tecnología","Net","Net-Baires"
                                    },
                                    Text="'IMPORTANTE: Mas información y registro en: http://netconfar.com. No se abrirá el registros por acá. .NET Conf AR v2017 se realizará los días 29, 30 de Junio y 1ero de Julio en la Facultad de Ciencias Económicas de la Universidad de Buenos Aires. <p><br/>.NET Conf es el <b>evento más importante de Latinoamérica sobre tecnologías .NET y Azure. Los últimos tres años se ha realizado en Montevideo, Uruguay, teniendo en el 2016 un total de +500 inscriptos, +30 speakers nacionales e internacionales y +40 charlas doble track a lo largo de 3 días. Este evento se realiza sin fines de lucro y es organizado por la comunidad de desarrolladores de Argentina y Uruguay. <p>Este año se realizará también en Argentina donde tenemos un día con <b>8 workshops y 2 días con 10 charlas cada uno de la mano de 24 speakers hablando de temas como: <i>Azure, Windows 10, Universal Apps, ASP.NET, MVC, WebAPI, Xamarin, Windows Mobile, Data Management, Sharepoint, Application Lifecycle Management, Agility, Visual Studio, C#, VB.NET, Emprendedurismo, Internet of Things, Aspect Oriented Programming, y mucho más!Para mas información del evento y registrarte: http://netconfar.com/ NOTA: No se abrirán los registros por acá."
                                    }
                                }
                        }
            };
            return local;
        }

        public List<CommunityModel> DataForView()
        {

            var list = new List<CommunityModel>();
            //Solo se carga en el modo diseño
            for (int i = 0; i < 5; i++)
            {
                var readModel = new CommunityModel
                {
                    Email = $"mail@prueba{i}.com",
                    Picture = "http://placehold.it/400x400",
                    Name = $"Nombre : {i}",
                    Events = new List<EventModel>
                    {
                        new EventModel
                        {
                            Date = RandomDay(),
                            Id = i,
                            Tags = new List<string>
                            {
                                $"Tag - {i}",
                                $"Tag - {i - 5}"
                            },
                            Text =
                                "Ex cupidatat culpa consequat enim laborum in deserunt anim occaecat. Deserunt eiusmod quis occaecat id deserunt est voluptate do fugiat adipisicing. Ut laboris in magna adipisicing amet non nulla in. Duis irure qui mollit ea et amet esse tempor dolor reprehenderit do.",
                            Title = $"Titulo numero  : {i}",
                            Image = "http://placehold.it/400x400"
                        },
                        new EventModel
                        {
                            Date = RandomDay(),
                            Id = i,
                            Tags = new List<string>
                            {
                                $"Tag - {i}",
                                $"Tag - {i - 5}"
                            },
                            Text =
                                "Ex cupidatat culpa consequat enim laborum in deserunt anim occaecat. Deserunt eiusmod quis occaecat id deserunt est voluptate do fugiat adipisicing. Ut laboris in magna adipisicing amet non nulla in. Duis irure qui mollit ea et amet esse tempor dolor reprehenderit do.",
                            Title = $"Titulo numero  : {i}",
                            Image = "http://placehold.it/400x400"
                        },
                        new EventModel
                        {
                            Date = RandomDay(),
                            Id = i,
                            Tags = new List<string>
                            {
                                $"Tag - {i}",
                                $"Tag - {i - 5}"
                            },
                            Text =
                                "Ex cupidatat culpa consequat enim laborum in deserunt anim occaecat. Deserunt eiusmod quis occaecat id deserunt est voluptate do fugiat adipisicing. Ut laboris in magna adipisicing amet non nulla in. Duis irure qui mollit ea et amet esse tempor dolor reprehenderit do.",
                            Title = $"Titulo numero  : {i}",
                            Image = "http://placehold.it/400x400"
                        },
                        new EventModel
                        {
                            Date = RandomDay(),
                            Id = i,
                            Tags = new List<string>
                            {
                                $"Tag - {i}",
                                $"Tag - {i - 5}"
                            },
                            Text =
                                "Ex cupidatat culpa consequat enim laborum in deserunt anim occaecat. Deserunt eiusmod quis occaecat id deserunt est voluptate do fugiat adipisicing. Ut laboris in magna adipisicing amet non nulla in. Duis irure qui mollit ea et amet esse tempor dolor reprehenderit do.",
                            Title = $"Titulo numero  : {i}",
                            Image = "http://placehold.it/400x400"
                        }
                    }
                };

                list.Add(readModel);

            }
            return list;
        }
        private DateTime RandomDay()
        {

            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
