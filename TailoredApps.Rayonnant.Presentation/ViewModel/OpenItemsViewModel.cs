using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Regions;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.Commands;
using TailoredApps.Rayonnant.Presentation.Interface.Modularity;
using TailoredApps.Rayonnant.Presentation.Interface.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.ViewModel
{
    /// <summary>
    ///     <see cref="IStatusViewModel" />
    /// </summary>
    public class OpenItemsViewModel : ViewModelBase
    {
        
        private INavigationManager navigationManager;
     
        public OpenItemsViewModel(INavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
            NavigateToOpenedItemCommand = new NavigateToViewModelCommand(this);
        }

        private ObservableCollection<ViewModelInfoAttribute> viewModels;
        public ObservableCollection<ViewModelInfoAttribute> ViewModels
        {
            get { return viewModels; }
            set
            {
                viewModels = value;
                OnPropertyChanged("ViewModels");
            }
            
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            ViewModels = new ObservableCollection<ViewModelInfoAttribute>(navigationManager.OpenedViewModelInfos);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public ICommand NavigateToOpenedItemCommand { get; set; }
    }
}