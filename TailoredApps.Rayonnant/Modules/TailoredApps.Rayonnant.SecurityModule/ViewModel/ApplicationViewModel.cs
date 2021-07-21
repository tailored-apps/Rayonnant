﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Prism.Mvvm;
using TailoredApps.Rayonnant.Presentation.ViewModel;

namespace TailoredApps.Rayonnant.SecurityModule.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private String name;
        private Brush elementColorBrush;
        private int rolesCount;
        private int userCount;

        public String Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public int UserCount
        {
            get { return userCount; }
            set { SetProperty(ref userCount, value); }
        }

        public int RolesCount
        {
            get { return rolesCount; }
            set { SetProperty(ref rolesCount, value); }
        }

        public Brush ElementColorBrush
        {
            get { return elementColorBrush; }
            set { SetProperty(ref elementColorBrush, value); }
        }

    }
}
