using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using Wise.Framework.Interface.ExceptionHandling;

namespace Wise.Framework.Presentation.Window
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExceptionWindow : WindowBase, IExceptionService
    {
        private bool? status;

        public ExceptionWindow()
        {
            InitializeComponent();
            CloseWindowCommand = new ActionCommand(closeAction);
            ResumeCommand = new ActionCommand(resumeAction);
        }

        #region [IExceptionService]

        public bool? ShowDialog(Exception exception, ExceptionOptions options)
        {
            return ShowDialog(exception, options, string.Empty);
        }

        public bool? ShowDialog(Exception exception, ExceptionOptions options, string message)
        {
            SetupButtonVisibility(options);
            Exception = exception;
            Message = message;
            ShowDialog();
            return status;
        }

        #endregion [IExceptionService]

        #region [Private Methods]

        private void SetupButtonVisibility(ExceptionOptions options)
        {
            switch (options)
            {
                case ExceptionOptions.Continue:
                    ContinueButtonVisibility = Visibility.Visible;
                    ExitButtonVisibility = Visibility.Collapsed;
                    break;
                case ExceptionOptions.Exit:
                    ContinueButtonVisibility = Visibility.Collapsed;
                    ExitButtonVisibility = Visibility.Visible;
                    break;
                case ExceptionOptions.ExitOrContinue:
                    ContinueButtonVisibility = Visibility.Visible;
                    ExitButtonVisibility = Visibility.Visible;
                    break;
            }
        }

        private void closeAction(object obj)
        {
            status = false;
            Close();
        }

        private void resumeAction(object obj)
        {
            status = true;
            Close();
        }

        #endregion [Private Methods]

        #region [Properties]

        public static readonly DependencyProperty ExceptionProperty = DependencyProperty.Register(
            "Exception", typeof (Exception), typeof (ExceptionWindow), new PropertyMetadata(default(Exception)));

        public static readonly DependencyProperty ContinueButtonVisibilityProperty = DependencyProperty.Register(
            "ContinueButtonVisibility", typeof (Visibility), typeof (ExceptionWindow),
            new PropertyMetadata(default(Visibility)));

        public static readonly DependencyProperty ExitButtonVisibilityProperty = DependencyProperty.Register(
            "ExitButtonVisibility", typeof (Visibility), typeof (ExceptionWindow),
            new PropertyMetadata(default(Visibility)));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof (string), typeof (ExceptionWindow), new PropertyMetadata(default(string)));

        public ICommand CloseWindowCommand { get; set; }
        public ICommand ResumeCommand { get; set; }

        public Exception Exception
        {
            get { return (Exception) GetValue(ExceptionProperty); }
            set { SetValue(ExceptionProperty, value); }
        }

        public Visibility ContinueButtonVisibility
        {
            get { return (Visibility) GetValue(ContinueButtonVisibilityProperty); }
            set { SetValue(ContinueButtonVisibilityProperty, value); }
        }

        public Visibility ExitButtonVisibility
        {
            get { return (Visibility) GetValue(ExitButtonVisibilityProperty); }
            set { SetValue(ExitButtonVisibilityProperty, value); }
        }

        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        #endregion [Properties]
    }
}