using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TryRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RingCentral.SDK.Platform platform;
        private Dictionary<string, string> dict;

        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppKey))
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
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.JsonString))
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Settings.Default.JsonString);
            }
            else
            {
                dict = new Dictionary<string, string>();
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.APIPath))
            {
                foreach (var item in apiPathComboBox.Items)
                {
                    var comboBoxItem = item as ComboBoxItem;
                    if (Properties.Settings.Default.APIPath == (comboBoxItem.Content as string))
                    {
                        apiPathComboBox.SelectedItem = item;
                        APIPathComboBoxSelectionChanged(null, null);
                    }
                }
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
            Properties.Settings.Default.APIPath = (apiPathComboBox.SelectedItem as ComboBoxItem).Content as string;
            dict[Properties.Settings.Default.APIPath] = jsonStringTextBox.Text;
            Properties.Settings.Default.JsonString = JsonConvert.SerializeObject(dict);
            Properties.Settings.Default.Save();

            if (platform == null)
            {
                platform = new RingCentral.SDK.SDK(appKeyTextBox.Text, appSecretTextBox.Text,
                    apiEndPointTextBox.Text, appNameTextBox.Text, appVersionTextBox.Text).GetPlatform();
            }

            if (!platform.IsAuthorized())
            {
                var tokens = usernameTextBox.Text.Split('-');
                var username = tokens[0];
                var extension = tokens.Length > 1 ? tokens[1] : null;
                platform.Authorize(username, extension, passwordTextBox.Text, true);
            }

            var request = new RingCentral.SDK.Http.Request("/restapi/v1.0" + (apiPathComboBox.SelectedItem as ComboBoxItem).Content as string, jsonStringTextBox.Text);
            var response = platform.Post(request);
            Debug.WriteLine(response.GetStatus());
            MessageBox.Show("Call API Completed", "Try RingCentral");
        }

        private void APIPathComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dict == null)
            {
                return;
            }
            var apiPath = (apiPathComboBox.SelectedItem as ComboBoxItem).Content as string;
            if (dict.ContainsKey(apiPath))
            {
                jsonStringTextBox.Text = dict[apiPath];
            }
            else
            {
                jsonStringTextBox.Text = "";
            }
        }
    }
}
