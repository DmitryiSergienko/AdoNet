using System.Windows;
using System.Windows.Controls;

namespace View.Helpers
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPasswordChanged)
            );

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static bool _isUpdating;

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                if (!_isUpdating && e.NewValue?.ToString() != passwordBox.Password)
                {
                    _isUpdating = true;
                    passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
                    _isUpdating = false;
                }

                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if (!_isUpdating)
                {
                    _isUpdating = true;
                    SetPassword(passwordBox, passwordBox.Password);
                    _isUpdating = false;
                }
            }
        }
    }
}