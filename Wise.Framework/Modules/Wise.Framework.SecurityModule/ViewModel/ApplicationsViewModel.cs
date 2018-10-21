using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Principal;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.SecurityModule.ViewModel
{
    [MenuItem(Path ="Options|Security", DisplayName ="Applications")]
    public class ApplicationsViewModel : ViewModelBase
    {

        public ApplicationsViewModel()
        {
            Title = "Security - Application View";
            AvailableApplications = new ObservableCollection<ApplicationViewModel>(
                new[] { 
                    new ApplicationViewModel {
                        Name   = "Application One",
                        UserCount = 10,
                        RolesCount = 3,
                        ElementColorBrush = new SolidColorBrush(Colors.SeaGreen)
                    },
                    new ApplicationViewModel {
                        Name   = "Application Two",
                        UserCount = 25,
                        RolesCount = 33,
                        ElementColorBrush = new SolidColorBrush(Colors.CornflowerBlue)
                    }
                });

          
        }
        private ObservableCollection<ApplicationViewModel> availableApplications;

        public ObservableCollection<ApplicationViewModel> AvailableApplications
        {
            get { return availableApplications; }
            set
            {
                SetProperty(ref availableApplications, value);
                ApplicationsCollectionView = CollectionViewSource.GetDefaultView(value);
                ApplicationsCollectionView.Filter += FilterElement;
            }
        }

        private bool FilterElement(object obj)
        {
            var element = obj as ApplicationViewModel;
            return string.IsNullOrWhiteSpace(ApplicationFilterName) || element.Name.ToLower().Contains(ApplicationFilterName.ToLower());
        }

        private string applicationFilterName;

        public string ApplicationFilterName
        {
            get { return applicationFilterName; }
            set
            {
                SetProperty(ref applicationFilterName, value);
                ApplicationsCollectionView.Refresh();
            }
        }

        private ICollectionView applicationsCollectionView;

        public ICollectionView ApplicationsCollectionView
        {
            get { return applicationsCollectionView; }
            set { SetProperty(ref applicationsCollectionView, value); }
        }
        private ApplicationViewModel selectedApplicationViewModel;

        public ApplicationViewModel SelectedApplicationViewModel
        {
            get { return selectedApplicationViewModel; }
            set { SetProperty(ref selectedApplicationViewModel, value); }
        }
    }
}
