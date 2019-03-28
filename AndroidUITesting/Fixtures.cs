using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidUITesting
{
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

        [SetUp]
        public void SetUp()
        {
            DesiredCapabilities _cap = new DesiredCapabilities();
            _cap.SetCapability("autoGrantPermissions", true);

            #region Si Application (.apk)
            //_cap.SetCapability("autoDismissAlerts", true);
            //_cap.SetCapability(MobileCapabilityType.App, documents + @"" + "***.apk");
            //_cap.SetCapability(AndroidMobileCapabilityType.AppPackage, "com.shapr");
            //_cap.SetCapability(AndroidMobileCapabilityType.AppActivity, "com.shapr.feature.splash.SplashActivity");
            //_cap.SetCapability(MobileCapabilityType.FullReset, true);
            //_cap.SetCapability(MobileCapabilityType.NoReset, false);
            #endregion Si Application (.apk)

            #region Si Navigateur Mobile (Chrome)
            _cap.SetCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
            _cap.SetCapability("chromedriverExecutable", @"C:\WEBDRIVERS\chromedriver.exe");
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
                            (new Uri("http://" + LocalIPAddress+ ":" + PORT.ToString() + "/wd/hub"), _cap);
            _wait = new WebDriverWait(_driverANDROID, TimeSpan.FromSeconds(20));
            _driverANDROID.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driverANDROID.Navigate(). // Endpoint/page de depart
                        GoToUrl("http://free.fr");
        }

        [TearDown]
        public void TearDown()
        {
            // Fermeture Application
            _driverANDROID.Quit();
        }

        #region Find By
        public IWebElement FindBy(By accessibility, string Description = "") // a ameliorer
        {
            TestContext _cntx = TestContext.CurrentContext;
            try
            {
                Thread.Sleep(3000);
                element = _wait.Until(ExpectedConditions.ElementIsVisible(accessibility));
                Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, Description + Environment.NewLine
                                                        + "Element HTML: " + "<" + element.TagName + ">; " + element.Text + Environment.NewLine
                                                        + "PositionX: " + element.Location.X + "  |  "
                                                        + "PositionY: " + element.Location.Y + Environment.NewLine
                                                        + "Hauteur: " + element.Size.Height + "  |  "
                                                        + "Largeur: " + element.Size.Width + Environment.NewLine
                                                        + "Affiché?: " + element.Displayed);
                // Vérifier qie l'élèment est bien visible.
                Assert.IsTrue(element.Displayed, " element " + accessibility + " non visible");
                // Hauteur non null.
                Assert.NotZero(element.Size.Height, " hauteur element " + accessibility + " null ..");
                // Largeur non null aussi.
                Assert.NotZero(element.Size.Width, " largeur element " + accessibility + " null ..");
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new WebDriverTimeoutException("Element non localisé(introuvable), reference: " + accessibility, ex);
            }
            return element;
        }
        #endregion Find By

        #region ReportPortal API
        public void RPComment(string comment)
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, comment);
        }
        public void RPComment(IWebElement element, string comment = "")
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, comment + Environment.NewLine
                                                                                + "Element HTML: " + "<" + element.TagName + ">; " + element.Text
                                                                                + Environment.NewLine
                                                                                + "PositionX: " + element.Location.X + "  |  "
                                                                                + "PositionY: " + element.Location.Y + Environment.NewLine
                                                                                + "Hauteur: " + element.Size.Height + "  |  "
                                                                                + "Largeur: " + element.Size.Width + Environment.NewLine
                                                                                + "Affiché?: " + element.Displayed);
        }
        public void RPScreenshot(string comment = "")
        {
            Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, comment + Environment.NewLine + _driverANDROID.Url
                                                                        + Environment.NewLine + _driverANDROID.Title
                                                                        + " {rp#file#" + TakeScreenshot(_driverANDROID, rnd.Next().ToString()) + "}");
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
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L) } };

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