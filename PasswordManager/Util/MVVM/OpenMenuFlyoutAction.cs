using Microsoft.Xaml.Interactivity;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace PasswordManager.Util.MVVM {

    public class OpenMenuFlyoutAction : DependencyObject, IAction {

        public object Execute(object sender, object parameter) {
            FrameworkElement senderElement = sender as FrameworkElement;
            try {
                FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
                flyoutBase.ShowAt(senderElement);
            }
            catch (Exception ex) {
                //Log.Error("FlyoutBase error: ", ex);
            }
            return null;
        }
    }
}