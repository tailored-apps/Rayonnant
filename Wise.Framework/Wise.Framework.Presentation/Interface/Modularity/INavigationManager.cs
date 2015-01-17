using System;
using System.Collections.Generic;
using Wise.Framework.Presentation.Annotations;
using Wise.Framework.Presentation.ViewModel;

namespace Wise.Framework.Presentation.Interface.Modularity
{
    public interface INavigationManager : IDisposable
    {
        void RegisterTypeForNavigation<T>();
        void RegisterTypeForNavigation(Type viewModelType);
        void CloseItem(ViewModelBase vm);
        void TearOff(ViewModelBase vm);
        void Dock(ViewModelBase vm);

        IEnumerable<ViewModelInfo> OpenedViewModelInfos { get; } 
    }
}