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
    public class Fragements : Fixtures
    {
        public void Deconnexion()
        {
            ToolTips();
            ToolTips();
            try
            {
                FindBy(By.Id("com.shapr:id/home_current_user_btn"), "Clqiue bouton Header [Profile]").Click(); //profile
                FindBy(By.Id("com.shapr:id/button_profile_menu_settings"), "Parametres, Scroll [Deconnexion]")
                    .Click(); //parametres
                Thread.Sleep(2500);
                _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                               ".scrollIntoView(new UiSelector().resourceId(\"com.shapr:id/text_view_settings_fragment_logout\"))")
                    .Click();
                FindBy(By.Id("android:id/button1"), "Confirmer la [Deconnexion]").Click(); //confirmer la deconnexion
            }
            catch (Exception)
            {
            }
            Thread.Sleep(5000);
        }

        public void ConnexionOLD()
        {
            Deconnexion();
            FindBy(By.Id("com.shapr:id/text_view_welcome_fragment_B_sign_in"), "M'inscrir avec un email")
                .Click(); //Clique m'inscrire avec email.
            FindBy(By.Id("com.shapr:id/edit_text_signin_fragment_b_mail"), "Champs email", true)
                .SendKeys("abdelkader@testingdigital.com"); //Saisie Email
            FindBy(By.Id("com.shapr:id/edit_text_signin_fragment_b_password"), "Champs mot de passe")
                .SendKeys("Byron2019*");
            FindBy(By.Id("com.shapr:id/button_signin_fragment_b_signin"), "Bouton [Me connecter]", true)
                .Click(); //Clique me connecter.
            FindBy(By.Id("com.shapr:id/text_view_next"), "Bouton [Suivant]").Click(); //Suivant
            FindBy(By.Id("com.shapr:id/text_view_update"), "Bouton [Valider]").Click(); //Valider
            ToolTips(); //1
            ToolTips(); //2
            _driverANDROID.CloseApp();
            _driverANDROID.LaunchApp();
        }

        public void Connexion()
        {
            RPComment(" --- Connexion --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_welcome_fragment_sign_in"), "Clique Lien [Deja inscrit(e), Me connecter]")
                .Click(); //Clique m'inscrire avec email.
            FindBy(By.Id("com.shapr:id/edit_text_signin_fragment_mail"), "Saisie Email [Champs email]", true)
                .SendKeys("abdelkader@testingdigital.com"); //Saisie Email
            FindBy(By.Id("com.shapr:id/edit_text_signin_fragment_password"), "Champs mot de passe")
                .SendKeys("Byron2019*");
            FindBy(By.Id("com.shapr:id/button_signin_fragment_signin"), "Bouton [Connexion]", true)
                .Click(); //Clique me connecter.
            ToolTips(); //1
            ToolTips(); //2
            _driverANDROID.CloseApp();
            _driverANDROID.LaunchApp();
        }

        public void Suppression()
        {
            Thread.Sleep(3500);
            _driverANDROID.FindElementByAccessibilityId("Plus d'options").Click();
            FindBy(By.Id("android:id/button1"), "Confirmer la [Suppression]").Click(); //confirmer la suppression
        }

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

        public void ToolTips()
        {
            Thread.Sleep(1500);
            try
            {
                FindBy(By.Id("com.shapr:id/text_view_tooltip_ok"), " [Tooltip] -> OK").Click();
            }
            catch (Exception)
            {
            }
        }

        public void PopUpOk()
        {
            Thread.Sleep(1500);
            try
            {
                FindBy(By.Id("android:id/button1"), "OK").Click();
            }
            catch (Exception)
            {
            }
        }

        public void HeaderRetour()
        {
            try
            {
                Thread.Sleep(2500);
                FindBy(By.XPath("//android.widget.ImageButton[@content-desc=\"Revenir en haut de la page\"]"),
                    "Bouton [Retour]", true).Click(); //Retour
            }
            catch (Exception)
            {
                Thread.Sleep(2500);
                _driverANDROID.FindElementByAccessibilityId("Revenir en haut de la page").Click();
            }
        }

        public void ResetResearch()
        {
            try
            {
                FindBy(By.Id("com.shapr:id/image_view_search"), "Recherche").Click(); //recherche
                FindBy(By.Id("com.shapr:id/view_swipe_fragment_results_bar"), "").Click();
                FindBy(By.Id("com.shapr:id/button_clear_all"), "[Clear all]").Click();
            }
            catch (Exception)
            {
            }
        }

        public void ProfileUtilisateur()
        {
            try
            {
                Thread.Sleep(7500);
                FindBy(By.XPath("//*[@resource-id='com.shapr:id/home_current_user_btn']"), "Bouton Header -> [Profile]").Click(); //profile
            }
            catch (Exception)
            {
                FindBy(By.Id("//*[@resource-id='com.shapr:id/image_view_view_card_header_picture']"), "Bouton Header -> [Profile]").Click(); //profile
            }
        }

        #region CAMERA par model
        public void SelectionCamera(string udid)
        {
            string mssgLog = "App [Appareil Photo], Bouton  [Confirmation]";
            switch (udid)
            {
                //HUAWEI P10 Lite 
                case "2XJDU17725001264":
                    FindBy(By.Id("com.huawei.camera:id/btn_review_confirm"), mssgLog).Click();
                    break;
                case "3300ec7c93ab338f":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //LG G6
                case "LGH870731a1c72":
                    try
                    {
                      //FindBy(By.Id("com.lge.camera:id/shutter_large_comp"), mssgLog).Click();
                        FindBy(By.Id("com.lge.camera:id/btn_ok"), mssgLog).Click();
                    }
                    catch (Exception)
                    {
                    }
                    break;
                //Honor 8
                case "73QFL17828000061":
                    try
                    {
                        FindBy(By.Id("com.huawei.camera:id/shutter_button"), mssgLog).Click();
                    }
                    catch (Exception) { }
                    FindBy(By.Id("com.huawei.camera:id/btn_review_confirm"), mssgLog).Click();
                    break;
                //Wiko Lenny
                case "0123456789ABCDEF":
                    FindBy(By.Id("com.android.gallery3d:id/camera_shutter"), mssgLog).Click();
                    FindBy(By.Id("com.android.gallery3d:id/btn_done"), mssgLog).Click();
                    break;
                //Wiko Wax
                case "0000000010FE8300":
                    FindBy(By.Id("com.android.gallery3d:id/camera_shutter"), mssgLog).Click();
                    FindBy(By.Id("com.android.gallery3d:id/btn_done"), mssgLog).Click();
                    break;
                //Huawei 5X
                case "W6HDU17616002733":
                    try
                    {
                        FindBy(By.Id("com.huawei.camera:id/shutter_button"), mssgLog).Click();
                    }
                    catch (Exception)
                    { }
                    FindBy(By.Id("com.huawei.camera:id/menu"), mssgLog).Click();
                    break;
                case "H8WDU16629002724":
                    try
                    {
                        FindBy(By.Id("com.huawei.camera:id/shutter_button"), mssgLog).Click();
                    }
                    catch (Exception)
                    { }
                    FindBy(By.Id("com.huawei.camera:id/menu"), mssgLog).Click();
                    break;
                //Galaxy A3
                case "85946ab2":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Galaxy S8
                case "ce0117119b24d62d01":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                case "RF8M323KZMY":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Galaxy S8 plus
                case "ce051715d018af3c03":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Galaxy S8
                case "ce03171339068c0b0c":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY S9
                case "1cc466b440027ece":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY S9 Plus
                case "1c91a23c670d7ece":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY Note 8
                case "ce061716c39258a30d7e":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY S6 Edge
                case "1115fb745e5e3e05":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY S6
                case "02157df28d5d822f":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
                //Samsung GALAXY J5
                case "0b23c81a":
                    FindBy(By.Id("com.sec.android.app.camera:id/okay"), mssgLog).Click();
                    break;
            }
        }
        #endregion

        public void InscriptionOLD()
        {
            Deconnexion();
            FindBy(By.Id("com.shapr:id/button_welcome_fragment_B_email"), "M'inscrir avec un email").Click(); //Clique m'inscrire avec email.
            FindBy(By.Id("com.shapr:id/check_email_txt"), "Champs email")
                .SendKeys("testingdigitalmeter" + (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Replace(",", "") + "@shapr.net"); //Saisie Email
            FindBy(By.Id("com.shapr:id/check_email_btn"), "bouton [Continuer]", true).Click(); //Clique me connecter.
            // ------  Formulaire personnel  -------- //
            FindBy(By.Id("com.shapr:id/account_creation_first_name_txt"), "Champs prenom").SendKeys("abdelkader"); //prenom
            FindBy(By.Id("com.shapr:id/account_creation_last_name_txt"), "Champs nom").SendKeys("abdelkader"); //nom
            FindBy(By.Id("com.shapr:id/account_creation_password_txt"), "Champs mot de passe").SendKeys("abdel1989!"); //mdp
            FindBy(By.Id("com.shapr:id/text_input_password_toggle"), "Icône Oeil").Click(); //suivant
            FindBy(By.Id("com.shapr:id/menu_next_btn"), "Bouton [Suivant]", true).Click(); //suivant
            // -------  Mon activité  --------- //
            FindBy(By.Id("com.shapr:id/occupation_role_txt"), "Champs rôle").SendKeys("Testeur"); //rôle
            FindBy(By.Id("com.shapr:id/occupation_company_txt"), "Champs entreprise").SendKeys("TD"); //entreprise.
            FindBy(By.Id("com.shapr:id/button_menu_save"), "Bouton [Enregistrer]").Click(); //enregistrer.
            // -------  Photo profile  --------- //
            FindBy(By.Id("com.shapr:id/signup_picture_btn"), "Icône edit photo").Click(); //icone edit photo.
            FindBy(By.Id("com.shapr:id/photo_source_camera_btn"), "Choix [Prendre une photo]").Click(); //choix "prendre une photo"
            Thread.Sleep(2000);
            _driverANDROID.PressKeyCode(AndroidKeyCode.Keycode_CAMERA);
            Thread.Sleep(5000);
            SelectionCamera(UDID);
            Thread.Sleep(3000);
            FindBy(By.Id("com.shapr:id/button_menu_save"), "Bouton [Enregistrer]").Click(); //enregistrer
            FindBy(By.Id("com.shapr:id/menu_next_btn"), "Bouton [Suivant]", true).Click(); //suivant
            // --------   Interêts   -------- //
            FindBy(By.Id("com.shapr:id/interest_tag_txt"), "Saisie du 1er interêt").SendKeys("Management"); //saisie interet
            FindBy(By.Id("com.shapr:id/interest_add_btn"), "Lien [Ajouter]").Click(); //ajouter l'interet
            FindBy(By.Id("com.shapr:id/interest_tag_txt"), "Saisie du 2eme interêt").SendKeys("Test"); //saisie interet
            FindBy(By.Id("com.shapr:id/interest_add_btn"), "Saisie du 1er interêt").Click(); //ajouter l'interet
            FindBy(By.Id("com.shapr:id/done_btn"), "Saisie du 1er interêt", true).Click();
            // ----------   Objectifs   ------------ //
            Thread.Sleep(2500);
            FindBy(By.XPath("//*[@text=\"Trouver des mentors\"]"), "Choix d'un objectif").Click(); //choix d'un objectif
            FindBy(By.Id("com.shapr:id/text_view_next"), "Bouton [Suivant]").Click(); //Suivant
            FindBy(By.Id("com.shapr:id/edit_text_goals"), "Saisie [texte presentation]").SendKeys(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                " Ut enim ad minim veniam, quis"); //Text presentation
            FindBy(By.Id("com.shapr:id/text_view_update"), "Bouton [Valider]", true).Click(); //valider
        }
    }
}
