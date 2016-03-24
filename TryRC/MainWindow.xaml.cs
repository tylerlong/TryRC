using System.Collections.Generic;
using System.Windows;

namespace TryRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RingCentral.SDK.Platform platform;

        private void LoadConfig()
        {
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
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.SmsJson))
            {
                smsTextBox.Text = Properties.Settings.Default.SmsJson;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.RingoutJson))
            {
                ringoutTextBox.Text = Properties.Settings.Default.RingoutJson;
            }
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.FaxJson))
            {
                faxTextBox.Text = Properties.Settings.Default.FaxJson;
            }
        }

        private void SaveConfig()
        {
            Properties.Settings.Default.AppKey = appKeyTextBox.Text;
            Properties.Settings.Default.AppSecret = appSecretTextBox.Text;
            Properties.Settings.Default.APIEndPoint = apiEndPointTextBox.Text;
            Properties.Settings.Default.AppName = appNameTextBox.Text;
            Properties.Settings.Default.AppVersion = appVersionTextBox.Text;
            Properties.Settings.Default.Username = usernameTextBox.Text;
            Properties.Settings.Default.Password = passwordTextBox.Text;
            Properties.Settings.Default.SmsJson = smsTextBox.Text;
            Properties.Settings.Default.RingoutJson = ringoutTextBox.Text;
            Properties.Settings.Default.FaxJson = faxTextBox.Text;
            Properties.Settings.Default.Save();
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void Authorize()
        {
            SaveConfig();
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
        }
        private void AuthorizeButtonClick(object sender, RoutedEventArgs e)
        {
            Authorize();
            MessageBox.Show("Authorized", "Try RingCentral");
        }

        private void smsButton_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
            var request = new RingCentral.SDK.Http.Request("/restapi/v1.0/account/~/extension/~/sms", smsTextBox.Text);
            var response = platform.Post(request);
            MessageBox.Show("sms sent, response status: " + response.GetStatus(), "Try RingCentral");
        }

        private void ringoutButton_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
            var request = new RingCentral.SDK.Http.Request("/restapi/v1.0/account/~/extension/~/ringout", ringoutTextBox.Text);
            var response = platform.Post(request);
            MessageBox.Show("ringout started, response status: " + response.GetStatus(), "Try RingCentral");
        }

        private void faxButton_Click(object sender, RoutedEventArgs e)
        {
            Authorize();

            var attachments = new List<RingCentral.SDK.Helper.Attachment>();

            var textBytes = System.Text.Encoding.UTF8.GetBytes("hello fax");
            var attachment = new RingCentral.SDK.Helper.Attachment(@"test.txt", "application/octet-stream", textBytes);
            attachments.Add(attachment);
            var attachment2 = new RingCentral.SDK.Helper.Attachment(@"test2.txt", "text/plain", textBytes);
            attachments.Add(attachment2);

            // uncomment below to send a pdf file
            //var pdfBytes = File.ReadAllBytes(@"C:\Users\tyler.liu\Desktop\test.pdf");
            //var attachment3 = new RingCentral.SDK.Helper.Attachment("test.pdf", "application/pdf", pdfBytes);
            //attachments.Add(attachment3);

            var request = new RingCentral.SDK.Http.Request("/restapi/v1.0/account/~/extension/~/fax", faxTextBox.Text, attachments);
            var response = platform.Post(request);
            MessageBox.Show("fax sent, response status: " + response.GetStatus(), "Try RingCentral");
        }
    }
}
