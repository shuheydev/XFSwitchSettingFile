using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFSwitchSettingFile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _apiUrlBase;
        public string ApiUrlBase
        {
            get
            {
                return _apiUrlBase;
            }
            set
            {
                _apiUrlBase = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            #region Embedded resourceファイルを読み込む.
            var assembly = Assembly.GetExecutingAssembly();

            var resName = assembly.GetManifestResourceNames()
                ?.FirstOrDefault(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase));

            using var file = assembly.GetManifestResourceStream(resName);

            using var sr = new StreamReader(file);

            var jsonText = sr.ReadToEnd();
            #endregion

            //定義が未知のJsonを扱う
            //https://rksoftware.hatenablog.com/entry/2020/02/11/164102
            var jsonDocument = JsonDocument.Parse(jsonText);
            jsonDocument.RootElement.TryGetProperty("apiUrlBase", out var apiUrlBaseElement);
            var apiUrlBase = apiUrlBaseElement.GetString();

            this.ApiUrlBase = apiUrlBase;
        }
    }
}
