using System.Windows;

namespace TryRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppKey))
            {
                appKeyTextBox.Text = Properties.Settings.Default.AppKey;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppSecret))
            {
                appSecretTextBox.Text = Properties.Settings.Default.AppSecret;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.APIEndPoint))
            {
                apiEndPointTextBox.Text = Properties.Settings.Default.APIEndPoint;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppName))
            {
                appNameTextBox.Text = Properties.Settings.Default.AppName;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppVersion))
            {
                appVersionTextBox.Text = Properties.Settings.Default.AppVersion;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Username))
            {
                usernameTextBox.Text = Properties.Settings.Default.Username;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Password))
            {
                passwordTextBox.Text = Properties.Settings.Default.Password;
            }
        }

        private void AppKeyTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (appKeyTextBox.Text == "App Key")
            {
                appKeyTextBox.Text = "";
            }
        }

        private void AppKeyTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(appKeyTextBox.Text))
            {
                appKeyTextBox.Text = "App Key";
            }
        }

        private void AppSecretTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(appSecretTextBox.Text))
            {
                appSecretTextBox.Text = "App Secret";
            }
        }

        private void AppSecretTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (appSecretTextBox.Text == "App Secret")
            {
                appSecretTextBox.Text = "";
            }
        }

        private void ApiEndPointTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if(apiEndPointTextBox.Text == "API EndPoint")
            {
                apiEndPointTextBox.Text = "";
            }
        }

        private void ApiEndPointTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(apiEndPointTextBox.Text))
            {
                apiEndPointTextBox.Text = "API EndPoint";
            }
        }

        private void AppNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(appNameTextBox.Text))
            {
                appNameTextBox.Text = "App Name";
            }
        }

        private void AppNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (appNameTextBox.Text == "App Name")
            {
                appNameTextBox.Text = "";
            }
        }

        private void AppVersionTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (appVersionTextBox.Text == "App Version")
            {
                appVersionTextBox.Text = "";
            }
        }

        private void AppVersionTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(appVersionTextBox.Text))
            {
                appVersionTextBox.Text = "App Version";
            }
        }

        private void UsernameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                usernameTextBox.Text = "Phone number with or without extension, separated by -";
            }
        }

        private void UsernameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "Phone number with or without extension, separated by -")
            {
                usernameTextBox.Text = "";
            }
        }

        private void PasswordTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                passwordTextBox.Text = "Password";
            }
        }

        private void PasswordTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Text == "Password")
            {
                passwordTextBox.Text = "";
            }
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AppKey = appKeyTextBox.Text;
            Properties.Settings.Default.AppSecret = appSecretTextBox.Text;
            Properties.Settings.Default.APIEndPoint = apiEndPointTextBox.Text;
            Properties.Settings.Default.AppName = appNameTextBox.Text;
            Properties.Settings.Default.AppVersion = appVersionTextBox.Text;
            Properties.Settings.Default.Username = usernameTextBox.Text;
            Properties.Settings.Default.Password = passwordTextBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
