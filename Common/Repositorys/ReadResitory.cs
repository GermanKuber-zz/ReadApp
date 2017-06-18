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

namespace Common.Repositorys
{
    public  class ReadRepository
    {
        private  List<ReadModel> _readCache;

        public ReadRepository()
        {
            
        }
        public async Task GenerateAsync()
        {
            if (_readCache == null)
           _readCache =  await ReadFromFile();
        }
        public async Task<List<ReadModel>> GetAllAsync()
        {
            await GenerateAsync();
            return _readCache;
        }

        public async Task<List<ReadModel>> GetByNameAsync(string name)
        {
            await GenerateAsync();
            return _readCache.Where(d => d.Name.ToLowerInvariant()
                .Contains(name.ToLowerInvariant()))
                .ToList(); ;
        }



        public  async Task<List<NoticeModel>> GetNoticesInDay()
        {

            var now = DateTime.Now;

            var selectedChildren = _readCache.SelectMany(x => x.Notices).Where(s => s.DateParse.Day == now.Day).ToList();

            return selectedChildren;
            // return await ReadFromFile();
        }

        public  async Task<List<ReadModel>> ReadFromFile()
        {
            //El archivo data.json debe esta en : ReadApp\bin\x86\Debug\AppX\data.json
            StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///data.json"));
            var file = File.ReadAllText(storageFile.Path);
            var listReturn = JsonConvert.DeserializeObject<List<ReadModel>>(file);
            return listReturn;
        }
    }
}