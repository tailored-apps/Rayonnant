using System;
using System.Windows;
using Wise.Framework.Interface.ExceptionHandling;
using Wise.Framework.Interface.Window;

namespace Wise.Framework.Presentation.Window
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExceptionWindow : WindowBase, IShellWindow, IExceptionService
    {
        public ExceptionWindow()
        {
            InitializeComponent();
        }


        public bool? ShowDialog(Exception exception, ExceptionOptions options)
        {
            Exception = exception;
            
            return ShowDialog();
        }

        public bool? ShowDialog(Exception exception, ExceptionOptions options, string message)
        {
            throw new NotImplementedException();
        }

        public static readonly DependencyProperty ExceptionProperty = DependencyProperty.Register(
            "Exception", typeof (Exception), typeof (ExceptionWindow), new PropertyMetadata(default(Exception)));

        public Exception Exception
        {
            get { return (Exception) GetValue(ExceptionProperty); }
            set { SetValue(ExceptionProperty, value); }
        }   
    }
}