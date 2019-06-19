using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TDDevices;

namespace AndroidUITesting
{ //cmd appops set <package> READ_CLIPBOARD ignore
    public class Fixtures
    {
        public AndroidDriver<AndroidElement> _driverANDROID;
        public WebDriverWait _wait;
        string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string UDID = "85946ab2"; 
        public string computerName = Environment.MachineName;
        public int checkAppiumCount = 0, attemps = 0, PORT = 4723;
        public IWebElement element;
        Random rnd = new Random();

        private TestContext contexte;
        public int Height, Width, x, y, starty, endy, startx;
        public double startTime, endTime;

        [SetUp]
        public void SetUp()
        {
            startTime = (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            DesiredCapabilities _cap = new DesiredCapabilities();

            #region Si Application (.apk)
            //_cap.SetCapability("autoGrantPermissions", true);
            //_cap.SetCapability("autoDismissAlerts", true);
            //_cap.SetCapability(MobileCapabilityType.App, @"C:\Users\CONNECTEUR-ROG-C\Downloads\686.apk");
            //_cap.SetCapability(AndroidMobileCapabilityType.AppPackage, "com.shapr");
            //_cap.SetCapability(AndroidMobileCapabilityType.AppActivity, "com.shapr.feature.splash.SplashActivity");
            ////_cap.SetCapability(MobileCapabilityType.FullReset, true);
            ////_cap.SetCapability(MobileCapabilityType.NoReset, false);
            #endregion Si Application (.apk)

            #region Si Navigateur Mobile (Chrome)
            _cap.SetCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
            //_cap.SetCapability("chromedriverExecutable", @"C:\WEBDRIVERS\chromedriver.exe");
            #endregion Si Navigateur Mobile (Chrome)

            #region Capabilitées
            _cap.SetCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
            _cap.SetCapability(MobileCapabilityType.NewCommandTimeout, 36000);
            _cap.SetCapability(MobileCapabilityType.Udid, UDID);
            _cap.SetCapability(MobileCapabilityType.DeviceName, "Téléphone");
            _cap.SetCapability(AndroidMobileCapabilityType.UnicodeKeyboard, true);
            _cap.SetCapability(AndroidMobileCapabilityType.ResetKeyboard, true);
            #endregion Capabilitées

            StartRemoteAppiumNode(PORT.ToString(), UDID, LocalIPAddress);
            _driverANDROID = new AndroidDriver<AndroidElement> // Serveur de lancement d'application
                            (new Uri("http://" + LocalIPAddress + ":" + PORT.ToString() + "/wd/hub"), _cap);
            _wait = new WebDriverWait(_driverANDROID, TimeSpan.FromSeconds(20));
            // Caches et donnée a supprimer
            // _driverANDROID.ResetApp()
        }

        [TearDown]
        public void TearDown()
        {
            endTime = (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            RPScreenshot("Status avant la fin de l'execution");
            //Fermeture Application
            _driverANDROID.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        #region Attachements SLACK
        private readonly List<TDDevices.Action> cxx = new List<TDDevices.Action>
        {
            new TDDevices.Action {Text = "Rapports 📃", Type = "button", Url = "http://www.automationasaservice.com/ui/#shapr/launches/all"}
        };
        public List<Attachment> GetAttchement()
        {
            return new List<Attachment> {
                new Attachment
            {
                Actions = cxx,
                Fallback = "les rapports d'exécution sur:  http://www.automationasaservice.com/ui/#shapr/launches/all"
            }};

        }
        #endregion

        #region Find By
        public IWebElement FindBy(By accessibility, string Description = "", bool Scr = false, bool testInput= false) // à ameliorer
        {
            TestContext _cntx = TestContext.CurrentContext;
            try
            {
                if (Scr.Equals(true))//screenshot a volonté :'(
                    RPScreenshot();
                Thread.Sleep(3000);
                element = _wait.Until(ExpectedConditions.ElementExists(accessibility));
                Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, "Element: " + "<" + element.TagName + ">; " + element.Text + Environment.NewLine
                                                        + "PositionX: " + element.Location.X + "  |  "
                                                        + "PositionY: " + element.Location.Y + Environment.NewLine
                                                        + "Hauteur: " + element.Size.Height + "  |  "
                                                        + "Largeur: " + element.Size.Width + Environment.NewLine
                                                        + "Affiché?: " + element.Displayed);
                Assert.IsTrue(element.Displayed, " element " + accessibility + " non visible");// Vérifier qie l'élèment est bien visible.
                Assert.NotZero(element.Size.Height, " hauteur element " + accessibility + " null ..");// Hauteur non null.
                Assert.NotZero(element.Size.Width, " largeur element " + accessibility + " null ..");// Largeur non null aussi.
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new WebDriverTimeoutException("Element non localisé(introuvable), reference: " + accessibility, ex);
            }
            return element;
        }
        #endregion Find By

        #region ReportPortal API
        public void RPComment(string comment, ReportPortal.Client.Models.LogLevel logLevel)
        {
            Bridge.LogMessage(logLevel, comment);
        }
        public void RPComment(IWebElement element, string comment = "")
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, comment 
                                                                                + "Element: " + "<" + element.TagName + ">; " + element.Text
                                                                                + Environment.NewLine
                                                                                + "PositionX: " + element.Location.X + "  |  "
                                                                                + "PositionY: " + element.Location.Y + Environment.NewLine
                                                                                + "Hauteur: " + element.Size.Height + "  |  "
                                                                                + "Largeur: " + element.Size.Width + Environment.NewLine
                                                                                + "Affiché?: " + element.Displayed);
        }
        public void RPScreenshot(string comment = "")
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, comment
                                                                        + " {rp#file#" + TakeScreenshot(_driverANDROID, rnd.Next().ToString()).Item1 + "}");
        }
        #endregion ReportPortal API

        #region APPIUM Handler
        public void StartRemoteAppiumNode(string portMobile, string deviceUDID, string Ip)
        {

            ConnectionOptions con = new ConnectionOptions();
            var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", computerName));
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

            var processToRun = new[] { "cmd.exe /c appium -a "+ Ip  +" -p "+portMobile+
                                       " -bp "+(Int32.Parse(portMobile)-2472)+
                                       " --udid " +deviceUDID+
                                       " --chromedriver-port "+(Int32.Parse(portMobile)+4795)+
                                       " --command-timeout " +180+" --local-timezone --session-override --no-reset"};
            wmiProcess.InvokeMethod("Create", processToRun);
            Task.Delay(5500).Wait();
            string xx = GetAppiumServerResponse(portMobile, Ip);
            while (xx != "OK" && checkAppiumCount < 4)
            {
                StartRemoteAppiumNode(portMobile, deviceUDID, Ip);
                checkAppiumCount++;
            }
        }

        public string GetAppiumServerResponse(string portMobile, string Ip)
        {
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("http://" + Ip + ":" + portMobile + "/wd/hub/sessions");
                webReq.UseDefaultCredentials = true;
                webReq.PreAuthenticate = true;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Method = "GET";
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                return webResp.StatusDescription;
            }
            catch (Exception)
            {
                return "NOK";
            }
        }
        #endregion APPIUM Handler

        #region variété Systeme
        public string LocalIPAddress
        {
            get
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("Y'a pas d'IP4 :O, pas d'adaptateur ou de carte reseau ou je ne sait quoi!");
            }
        }

        public static void UpdateSetting(string Value, string param)
        {
            try
            {
                System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings[param].Value = Value;
                configuration.Save();
            }
            catch (Exception)
            {
            }
            ConfigurationManager.RefreshSection("appSettings");
        }

        private string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        #endregion variété Systeme

        #region ScreenShot ANDROID
        private static Image ScreenshotToImage(Screenshot screenshot)
        {
            Image screenshotImage;
            using (var memStream = new MemoryStream(screenshot.AsByteArray))
            {
                screenshotImage = Image.FromStream(memStream);
            }
            return screenshotImage;
        }
        public Tuple<string, string> TakeScreenshot(AndroidDriver<AndroidElement> _driver, String deviceUDID,
                              string type = "SCR")
        {
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);
            PropertyItem prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 33432, "© 2016 Testing Digital");
            _ceImg.SetPropertyItem(prop);

            prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 315, "AutomateV2");
            _ceImg.SetPropertyItem(prop);

            prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 270, "Title...");
            _ceImg.SetPropertyItem(prop);

            _ceImg.SetPropertyItem(prop);
            string _nameCaptureEcran = type + DateTime.Now.ToString("MMdHHmmss") + deviceUDID + ".jpg";
            string _cheminCaptureEcran = PathProject() + "ANDROIDSCR\\" + _nameCaptureEcran;
            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 60L) } };

            _ceImg.Save(_cheminCaptureEcran, encoder, encParams);

            return new Tuple<string, string>(_cheminCaptureEcran, _nameCaptureEcran);
        }
        //My SetProperty code... (for ASCII property items only!)
        //Exif 2.2 requires that ASCII property items terminate with a null (0x00).
        private void SetProperty(ref PropertyItem prop, int iId, string sTxt)
        {
            int iLen = sTxt.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)sTxt[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = iId;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
        }
        public string PathProject()
        {
            string path = Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }
        #endregion ScreenShot ANDROID
    }
}