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
    public class MainPageDataViewModel : INotifyPropertyChanged
    {
        public MainPageDateService MainPageDateService = new MainPageDateService();

        #region Public Properties
        public Visibility VisibleMenu
        {
            get { return _visibleMenu; }
            set
            {
                if (value == _visibleMenu)
                    return;

                _visibleMenu = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(VisibleMenu)));
            }
        }
        public ObservableCollection<CommunityModel> ReadModels { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                    return;
                _title = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Title)));
            }
        }
        public ObservableCollection<ContactWrapper> Contacts = new ObservableCollection<ContactWrapper>();
        public event PropertyChangedEventHandler PropertyChanged;
        public CommunityModel SelectedRead
        {
            get { return _selectedRead; }
            set
            {
                _selectedRead = value;
                if (value == null)
                    Title = Welcome;
                else
                    Title = $"Saludos : {_selectedRead.Name}";
                PropertyChanged?.Invoke(this,
                   new PropertyChangedEventArgs(nameof(SelectedRead)));
                //if (VisibleMenu == Visibility.Visible)
                //    this.OpenMenu = false;
            }
        }
        public bool OpenMenu
        {
            get { return _openMenu; }
            set
            {
                _openMenu = value;
                PropertyChanged?.Invoke(this,
                   new PropertyChangedEventArgs(nameof(OpenMenu)));
            }
        }
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
        private ConfigurationsViewModel _configurations;
        public ConfigurationsViewModel Configurations
        {
            get { return _configurations; }
            set
            {
                if (value == _configurations)
                    return;

                _configurations = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Filter)));
            }
        }
        public LoadingStates LoadingState
        {
            get { return _loadingState; }
            set
            {
                if (value == _loadingState)
                    return;

                _loadingState = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(LoadingState)));
            }
        }
        #endregion


        #region Commands

        public ICommand AddNoticeToCommand
        {
            get
            {
                if (_addNoticeToCommand == null)
                {
                    _addNoticeToCommand = new CommandHandler(((obj) =>
                    {
                        MainPageDateService.AddNoticeToCalendarAsync((EventModel)obj);
                    }));
                }
                return _addNoticeToCommand;
            }
            set { _addNoticeToCommand = value; }
        }
        public ICommand SendEmailCommand
        {
            get
            {
                if (_sendEmailCommand == null)
                {
                    _sendEmailCommand = new CommandHandler((async (obj) =>
                    {
                        await MainPageDateService.SendEmailAsync((EventModel)obj,SelectedRead.Email);
                    }));
                }
                return _sendEmailCommand;
            }
            set { _sendEmailCommand = value; }
        }

        public ICommand SwitchMenuCommand
        {
            get
            {
                if (_switchMenuCommand == null)
                {
                    _switchMenuCommand = new CommandHandler(((obj) =>
                    {
                        this.OpenMenu = !this.OpenMenu;
                    }));
                }
                return _switchMenuCommand;
            }
            set { _switchMenuCommand = value; }
        }

        #endregion


        #region Private Properties
        private ReadRepository _readRepository = new ReadRepository();

        private const string Welcome = "Bienvenido ReadApp";
        private string _title;

        private List<CommunityModel> _readModels = new List<CommunityModel>();

        private CommunityModel _selectedRead;

        private string _filter;

        private ICommand _addNoticeToCommand;
        private ICommand _sendEmailCommand;

        private LoadingStates _loadingState = LoadingStates.Loading;
        private bool _openMenu;
        private ICommand _switchMenuCommand;
        private Visibility _visibleMenu;

        #endregion


        #region Constructor

        public MainPageDataViewModel()
        {
            this.Title = Welcome;
            this.Configurations = new ConfigurationsViewModel();
            this.LoadingState = LoadingStates.Loading;

            ReadModels = new ObservableCollection<CommunityModel>();
            //Genero data
            if (DesignMode.DesignModeEnabled)
                GenerateDummyData();
            else
                LoadData();

            this.VisibleMenu = Visibility.Visible;

        }

        #endregion

        #region Private Methods

        private async void LoadData()
        {
            try
            {
                _readModels = await _readRepository.GetAllAsync();
                await FilterTextAsync();
                LoadingState = LoadingStates.Loaded;
                var random = new Random();

                SelectedRead = ReadModels?.ElementAt(random.Next(0, ReadModels.Count - 1));
            }
            catch (Exception ex)
            {
                LoadingState = LoadingStates.Error;
            }
        }

        #endregion

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
        //Ya escrito

        private void GenerateDummyData()
        {

            var list = _readRepository.DataForView();
            list.ForEach(x =>
            {
                ReadModels.Add(x);

            });

            if (ReadModels != null && ReadModels.Count > 0)
                this.SelectedRead = ReadModels.First();
            //FilterText();
        }

    }
}
