using System;
using System.Collections.Generic;
using TailoredApps.Rayonnant.Presentation.Annotations;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.Presentation.Interface.Modularity
{
    public interface INavigationManager : IDisposable
    {
        void RegisterTypeForNavigation<T>();
        void RegisterTypeForNavigation(Type viewModelType);
        void CloseItem(ViewModelBase vm);
        void TearOff(ViewModelBase vm);
        void Dock(ViewModelBase vm);

        IEnumerable<ViewModelInfoAttribute> OpenedViewModelInfos { get; }
        IEnumerable<ViewModelInfoAttribute> RegisteredViewModels { get; } 

    }
}