﻿using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Presentation.Interface;
using Wise.Framework.Presentation.Modularity;

namespace Wise.Framework.Presentation.Commands
{
    public class NavigateToHomeCommand :BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var messanger = DependencyInjection.Container.Current.Resolve<IMessanger>();
            var preferenceManager = DependencyInjection.Container.Current.Resolve<IPreferenceManager>();
            
            var home =preferenceManager.GetUserHomeView();
            if (string.IsNullOrEmpty(home))
            {
                home = "Wise.Framework.Presentation.ViewModel.UserPreferencesSettingsViewModel";
            }

            messanger.Publish(new NavigationRequest() { ViewModelFullName = home});
        }

    }
}
