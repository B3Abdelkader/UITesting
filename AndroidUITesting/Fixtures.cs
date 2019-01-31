using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidUITesting
{
    public class Fixtures : Scripts
    {
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
            _cap.SetCapability(MobileCapabilityType.NewCommandTimeout, 18000);
            _cap.SetCapability(MobileCapabilityType.Udid, UDID);
            _cap.SetCapability(MobileCapabilityType.DeviceName, "Téléphone");
            _cap.SetCapability(AndroidMobileCapabilityType.UnicodeKeyboard, true);
            _cap.SetCapability(AndroidMobileCapabilityType.ResetKeyboard, true);
            #endregion Capabilitées

            StartRemoteAppiumNode(PORT.ToString(), UDID, GetLocalIPAddress());
            _driverANDROID = new AndroidDriver<AndroidElement> // Serveur de lancement d'application
                            (new Uri("http://" + GetLocalIPAddress() + ":" + PORT.ToString() + "/wd/hub"), _cap);
            _wait = new WebDriverWait(_driverANDROID, TimeSpan.FromSeconds(20));
            _driverANDROID.Navigate(). // Endpoint/page de depart
                        GoToUrl("**");
        }

        [TearDown]
        public void TearDown()
        {
            // Fermeture Application
            _driverANDROID.Quit();
        }
    }
}
