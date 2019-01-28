using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUITesting
{
    public class Fixtures : PreRequis
    {
        [SetUp]
        public void SetUp()
        {
            #region OneTimeSetup
            ChooseDriverInstance();// Lancer le Navigateur.
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15)); // Wait definie a X secondes.
            js = (IJavaScriptExecutor)_driver; // Au cas ou on aura besoin d'exectuter les scripts Js
            _action = new Actions(_driver); // pour actions telle que ( move to / perform / scroll to ....
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            #region OneTimeTearDown
            //Bridge.LogMessage(ReportPortal.Client.Models.LogLevel.Info, _driver.Url +
            //                                        Environment.NewLine + _driver.Title +
            //                                        " {rp#file#" + TakeScreenshot(_driver, nav) + "}");
            _driver.Manage().Cookies.DeleteAllCookies(); // Supprimer les cookies
            _driver.Quit();  // Fermer le navigateur
            _driver.Dispose(); // Liberer la ressource
            #endregion
        }
    }
}