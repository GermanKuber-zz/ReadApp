using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Common.ForViews;
using Common.Models;
using Common.Repositorys;
using Windows.UI.Xaml;

namespace Common.ViewModels
{
    //TODO : 04 - Implemento la interface INotifyPropertyChanged
    public class MainPageDataViewModel : INotifyPropertyChanged
    {
        //implemento la propiead de la interface.
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageDateService MainPageDateService = new MainPageDateService();

        private ReadRepository _readRepository = new ReadRepository();

        public ConfigurationsViewModel Configuration = new ConfigurationsViewModel();

        #region Public Properties

        //TODO : 03 - Agrego una lista observable, para derectar los cambios en la view
        public ObservableCollection<CommunityModel> ReadModels { get; set; } = new ObservableCollection<CommunityModel>();

        //TODO : 05 - Propiedad que se utilizara bindear contra el item seleccionado
        private CommunityModel _selectedRead;
        public CommunityModel SelectedRead
        {
            get { return _selectedRead; }
            set
            {
                _selectedRead = value;

                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedRead)));

            }
        }
        //TODO : 06 - Agrego propiedad para  bindear contra el texto que el usuario filtra
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter)
                    return;

                _filter = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Filter)));

                FilterTextAsync();
            }
        }


        #endregion


        #region Commands

        //private ICommand _exampleCommand;
        //public ICommand ExampleCommand
        //{
        //    get
        //    {
        //        if (_exampleCommand == null)
        //        {
        //            _exampleCommand = new CommandHandler(((obj) =>
        //            {
        //                ...
        //            }));
        //        }
        //        return _exampleCommand;
        //    }
        //    set { _exampleCommand = value; }
        //}

        #endregion


        #region Private Properties



        #endregion


        #region Constructor

        public MainPageDataViewModel()
        {
            GenerateDummyData();

        }

        #endregion

        #region Private Methods

        //TODO : 07 - Agrego metodo para filtrar y cargar los modelos a la lista
        private async Task FilterTextAsync()
        {
            if (_filter == null)
                _filter = "";

            //Se filtran los readModels por Name
            var result = await _readRepository.GetByNameAsync(this.Filter);

            ReadModels.Clear();
            result.ForEach(x =>
            {
                ReadModels.Add(x);
            });
            if (ReadModels.Count > 0)
                this.SelectedRead = ReadModels.First();
        }

        #endregion

        private void GenerateDummyData()
        {

            var list = _readRepository.DataForView();
            ReadModels.Clear();
            list.ForEach(x =>
            {
                ReadModels.Add(x);

            });

            if (ReadModels != null && ReadModels.Count > 0)
                this.SelectedRead = ReadModels.First();
        }

    }
}
