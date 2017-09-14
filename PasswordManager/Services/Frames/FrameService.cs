using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using PasswordManager.ViewModels;
using PasswordManager.Views;
using System;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PasswordManager.Services.Frames {
    public class FrameService {

        private static Frame _rootFrame;

        private static Frame _contentFrame;
        private static string _contentFrameNavigationState;

        public DelegateCommand<Frame> SetContentFrameCommand { get; set; }

        public FrameService() {
            SetContentFrameCommand = new DelegateCommand<Frame>(SetContentFrame);
        }

        public void Initialize(LaunchActivatedEventArgs e) {
            _rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (_rootFrame == null) {
                // Create a Frame to act as the navigation context and navigate to the first page

                _rootFrame = new Frame();

                _rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = _rootFrame;
            }

            if (e.PrelaunchActivated == false) {
                if (!AppSettings.HasMasterPassword) {
                    _rootFrame.Navigate(typeof(MasterPasswordCreatePage), e.Arguments);
                }

                _rootFrame.Navigate(typeof(MasterPasswordQueryPage), e.Arguments);

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        public void SetContentFrame(Frame frame) {
            _contentFrame = frame ?? throw new ArgumentNullException("frame");
            RegisterFrameEvents(frame);
            frame.CacheSize = 2;

            if (!string.IsNullOrWhiteSpace(_contentFrameNavigationState)) {
                frame.SetNavigationState(_contentFrameNavigationState);
            }
            else {
                frame.Navigate(typeof(MainPage));
            }
        }

        public Frame GetFrame(Type viewModel) {
            if (viewModel == null) {
                throw new ArgumentNullException("viewModel");
            }

            var interfaces = viewModel.GetInterfaces().ToList();
            return interfaces.Contains(typeof(IRootViewModel)) ? _rootFrame : _contentFrame;
        }

        private void RegisterFrameEvents(Frame frame) {
            frame.NavigationFailed += OnNavigationFailed;
            frame.Navigated += _contentFrame_Navigated;
            frame.Unloaded += _contentFrame_Unloaded;
        }

        private void UnRegisterFrameEvents(Frame frame) {
            frame.NavigationFailed -= OnNavigationFailed;
            frame.Navigated -= _contentFrame_Navigated;
            frame.Unloaded -= _contentFrame_Unloaded;
        }

        private void _contentFrame_Unloaded(object sender, RoutedEventArgs e) {
            var frame = sender as Frame;
            if (frame == null) {
                throw new ArgumentException(string.Format("Expected sender as Frame, got {0}", sender.GetType()));
            }

            UnRegisterFrameEvents(frame);
            _contentFrameNavigationState = frame.GetNavigationState();
        }

        private void _contentFrame_Navigated(object sender, NavigationEventArgs e) {
            var frame = sender as Frame;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

    }
}
