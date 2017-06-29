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
    public class MainPageDataViewModel 
    {

        public MainPageDateService MainPageDateService = new MainPageDateService();

        private ReadRepository _readRepository = new ReadRepository();

        public ConfigurationsViewModel Configurations { get; set; } = new ConfigurationsViewModel();

        #region Public Properties

        //private bool _example;
        //public bool Example
        //{
        //    get { return _example; }
        //    set
        //    {
        //        _example = value;
        //        PropertyChanged?.Invoke(this,
        //           new PropertyChangedEventArgs(nameof(Example)));
        //    }
        //}

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
          

        }

        #endregion

        #region Private Methods


        #endregion








        //private void GenerateDummyData()
        //{

        //    var list = _readRepository.DataForView();
        //    list.ForEach(x =>
        //    {
        //        ReadModels.Add(x);

        //    });

        ////    if (ReadModels != null && ReadModels.Count > 0)
        ////        this.SelectedRead = ReadModels.First();
        //}

    }
}
