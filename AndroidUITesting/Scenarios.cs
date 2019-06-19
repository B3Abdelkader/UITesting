using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Appium.Android;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace AndroidUITesting
{
    class Scenarios : Fragements
    {
        // Declarations vars
        string email = "Abdelkader+" + (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Replace(",", "") + "@testingdigital.com";


        [TestCase(TestName = "Inscription",  //Ignore = "En cours d'édition",
            Author = "BYRON Group")]
        public void Inscription()
        {

        }

        [TestCase(TestName = "Parametres utilisateur",  //Ignore = "En cours d'édition",
            Author = "BYRON Group")]
        public void Parametres()
        {

        }

        [TestCase(TestName = "LinkedIn",  //Ignore = "En cours d'édition",
            Author = "BYRON Group")]
        public void LinkedIn()
        {

        }

        [TestCase(TestName = "Compte utilisateur",  //Ignore = "En cours d'édition",
            Author = "BYRON Group")]
        public void CompteUtilisateur()
        {
            _driverANDROID.Navigate().GoToUrl("https://www.free.fr");
        }
    }
}