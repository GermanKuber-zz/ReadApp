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
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageDateService MainPageDateService = new MainPageDateService();

        private ReadRepository _readRepository = new ReadRepository();

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

        #region Public Properties

        public ObservableCollection<CommunityModel> ReadModels { get; set; } = new ObservableCollection<CommunityModel>();


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

        private ICommand _addNoticeToCommand;
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
        private ICommand _sendEmailCommand;
        public ICommand SendEmailCommand
        {
            get
            {
                if (_sendEmailCommand == null)
                {
                    _sendEmailCommand = new CommandHandler((async (obj) =>
                    {
                        await MainPageDateService.SendEmailAsync((EventModel)obj, SelectedRead.Email);
                    }));
                }
                return _sendEmailCommand;
            }
            set { _sendEmailCommand = value; }
        }

        #endregion


        #region Private Properties



        #endregion


        #region Constructor

        public MainPageDataViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                GenerateDummyData();
            else
                FilterTextAsync();

            this.Configurations = new ConfigurationsViewModel();
        }

        #endregion

        #region Private Methods

        private async Task FilterTextAsync()
        {
            if (_filter == null)
                _filter = "";

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
