using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebUITesting
{
    public class PreRequis
    {
        public IWebDriver _driver;
        public TestContext _cntx;
        public WebDriverWait _wait;
        public IJavaScriptExecutor js;
        Random rnd = new Random();
        public Actions _action;
        private IWebElement element;
        private int attemps = 0;
        private readonly string ActualElement, descriptionElement;

        #region Find By
        public IWebElement FindBy(By accessibility, string Description = "")
        {
            TestContext _cntx = TestContext.CurrentContext;
            if (attemps < 3)
            {
                try
                {
                    Thread.Sleep(3000);
                    element = _wait.Until(ExpectedConditions.ElementIsVisible(accessibility));
                    //Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, Description + Environment.NewLine
                    //                                        + "Element HTML: " + "<" + element.TagName + ">; " + element.Text + Environment.NewLine
                    //                                        + "PositionX: " + element.Location.X + "  |  "
                    //                                        + "PositionY: " + element.Location.Y + Environment.NewLine
                    //                                        + "Hauteur: " + element.Size.Height + "  |  "
                    //                                        + "Largeur: " + element.Size.Width + Environment.NewLine
                    //                                        + "Affiché?: " + element.Displayed);
                    attemps = 0;
                    Assert.IsTrue(element.Displayed);// Vérifier qie l'élèment est bien visible.
                    Assert.NotZero(element.Size.Height);// Hauteur non null.
                    Assert.NotZero(element.Size.Width);// Largeur non null aussi.
                }
                catch (Exception ex)
                {
                    attemps++;
                    FindBy(accessibility, Description);
                }
            }
            return element;
        }
        #endregion

        #region Gestion instances drivers
        public void ChooseDriverInstance(string browserType="chrome") // parametre optionnel pour d'autres navigateur a précisier.
        {
            if (browserType == "chrome")
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--window-size=1920,1080");
                //chromeOptions.AddArguments("headless");
                _driver = new ChromeDriver(@"C:\WEBDRIVERS", chromeOptions);

                _driver.Manage().Window.Maximize();
            }
            else if (browserType == "firefox")
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                //firefoxOptions.AddArgument("--headless");
                //firefoxOptions.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\";
                //firefoxOptions.AddArgument("--width=1920");
                //firefoxOptions.AddArgument("--height=1080");
                service.FirefoxBinaryPath = @"C:\WEBDRIVERS";
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                _driver = new FirefoxDriver(@"C:\WEBDRIVERS\", firefoxOptions);
                //_driver.Manage().Window.Size = new Size(1920, 1080);
                //_driver = new FirefoxDriver();
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == "ie")
            {
                _driver = new InternetExplorerDriver(@"C:\WEBDRIVERS");
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == "edge")
            {
                _driver = new EdgeDriver(@"C:\WEBDRIVERS");
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == "opera")
            {

                _driver = new OperaDriver(@"C:\WEBDRIVERS");
                _driver.Manage().Window.Maximize();
            }
        }

        public void DriverKiller(string browserType)
        {
            Process[] listProcessDriver;
            switch (browserType)
            {
                case "chrome":
                    {
                        listProcessDriver = Process.GetProcessesByName("chromedriver");
                        foreach (Process _process in listProcessDriver)
                        {
                            _process.Kill();
                        }
                    }
                    break;
                case "firefox":
                    {
                        listProcessDriver = Process.GetProcessesByName("geckodriver");
                        foreach (Process _process in listProcessDriver)
                        {
                            _process.Kill();
                        }
                    }
                    break;
                case "ie":
                    break;
                case "edge":
                    {
                        listProcessDriver = Process.GetProcessesByName("MicrosoftWebDriver");
                        foreach (Process _process in listProcessDriver)
                        {
                            _process.Kill();
                        }
                    }
                    break;
                case "opera":
                    {
                        listProcessDriver = Process.GetProcessesByName("operadriver");
                        foreach (Process _process in listProcessDriver)
                        {
                            _process.Kill();
                        }
                    }
                    break;
                case "headLess":
                    {
                        listProcessDriver = Process.GetProcessesByName("phantomjs");
                        foreach (Process _process in listProcessDriver)
                        {
                            _process.Kill();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ScreenShots
        public string TakeScreenshot(IWebDriver _driver, String deviceUDID,
                              string type = "SCR")
        {
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);
            PropertyItem prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 33432, "@B3Abdelkader");
            _ceImg.SetPropertyItem(prop);

            prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 315, "automation");
            _ceImg.SetPropertyItem(prop);

            prop = _ceImg.PropertyItems[0];
            SetProperty(ref prop, 270, "Title...");
            _ceImg.SetPropertyItem(prop);

            _ceImg.SetPropertyItem(prop);
            string _nameCaptureEcran = type + DateTime.Now.ToString("MMdHHmmss") + deviceUDID + ".jpg";
            string _cheminCaptureEcran = PathProject() + "RepoScreenShots\\" + _nameCaptureEcran;
            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L) } };

            _ceImg.Save(_cheminCaptureEcran, encoder, encParams);

            return _cheminCaptureEcran;
        }

        //My SetProperty code... (for ASCII property items only!)
        //Ex if 2.2 requires that ASCII property items terminate with a null (0x00).
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
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }

        private static Image ScreenshotToImage(Screenshot screenshot)
        {
            Image screenshotImage;
            using (var memStream = new MemoryStream(screenshot.AsByteArray))
            {
                screenshotImage = Image.FromStream(memStream);
            }
            return screenshotImage;
        }
        #endregion ScreenShots
    }
}
