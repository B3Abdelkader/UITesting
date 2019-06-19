using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Interfaces;

namespace AndroidUITesting
{
    /// <summary>
    /// Class de definition de methodes utilisé dans plusieurs tests afin de limiter la repetition.
    /// </summary>
    public class Fragements : Fixtures
    {
        public void AcceptAlerte()
        {
            Thread.Sleep(2500);
            try
            {
                _driverANDROID.SwitchTo().Alert().Accept();
            }
            catch (Exception)
            {
            }
        }

    }
}
