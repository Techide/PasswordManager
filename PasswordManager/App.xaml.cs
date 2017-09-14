using Microsoft.EntityFrameworkCore;
using PasswordManager.Models.Data.EF;
using PasswordManager.Services.Settings;
using PasswordManager.ViewModels;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace PasswordManager {

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application {

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() {
            InitializeComponent();
            UnhandledException += App_UnhandledException;
            Suspending += OnSuspending;

            using (var db = new PasswordManagerContext()) {
                try {
                    db.Database.Migrate();
                }
                catch (Exception ex) {
                    //Log.Error(ex.Message, ex);
                }
                try {
                    var t = db.Settings.FirstOrDefaultAsync(x => x.Name == AppSettings.MASTER_PASSWORD_KEY).Result;
                    if (t == null) {
                        db.Settings.Add(new Setting { Name = AppSettings.MASTER_PASSWORD_KEY });
                        db.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    //Log.Error(ex.Message, ex);
                }
            }
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            //Log.Error("An unhandled exception was caught.", e);
            e.Handled = true;
        }

        protected override void OnActivated(IActivatedEventArgs args) {
            base.OnActivated(args);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            if (e == null) {
                throw new ArgumentNullException(nameof(e));
            }

            var framehandler = ViewModelLocator.FrameService;
            framehandler.Initialize(e);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();
            //Log.CloseAndFlush();
            AppSettings.MasterPassword = null;
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}