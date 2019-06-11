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
        string email = "testingdigitalmeter" + (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Replace(",", "") + "@shapr.net";


        [TestCase(TestName = "Inscription",  //Ignore = "pour CI",
            Author = "TESTING DIGITAL")]
        public void Inscription()
        {
            // --- Page accueil ---
            RPComment("Email generique utilisé:  " + email, ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/button_welcome_fragment_email"), "[Page Accueil], Clique [M'inscrire avec un email]").Click(); //Clique m'inscrire avec email.
            // --- Formulaire E-mail ---
            RPComment(" --- Etape: inscription --- " + email, ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/edit_text_signup_email"), "Saisie adresse email [Champs email]")
                .SendKeys("testingdigitalmeter" + (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString().Replace(",", "") + "@shapr.net"); //Saisie Email
            FindBy(By.Id("com.shapr:id/edit_text_signup_password"), "Saisie mot de passe").SendKeys("Shapr2018*");
            FindBy(By.Id("com.shapr:id/text_input_password_toggle"), "Afficher le mot de passe (icône oeil)").Click();
            FindBy(By.Id("com.shapr:id/button_next"), "Clique [Inscription par email]", false).Click(); //Bouton inscription par email.
            RPComment(" --- Etape: Votre identité --- ", ReportPortal.Client.Models.LogLevel.Warning);
            // ------  Formulaire personnel  -------- //
            FindBy(By.Id("com.shapr:id/edit_text_first_name"), "Saisie prenom", false, true).SendKeys("Testeur"); //prenom
            FindBy(By.Id("com.shapr:id/edit_text_last_name"), "Saisie nom", false, true).SendKeys("Testeur"); //nom
            // -------  Photo profile  --------- //
            FindBy(By.Id("com.shapr:id/image_view_upload_btn"), "Clique [Icône edit photo]").Click(); //icone edit photo.
            FindBy(By.Id("com.shapr:id/photo_source_camera_btn"), "Choix [Prendre une photo]").Click(); //choix "prendre une photo"
            Thread.Sleep(2000);
            _driverANDROID.PressKeyCode(AndroidKeyCode.Keycode_CAMERA);
            Thread.Sleep(5000);
            SelectionCamera(UDID);
            Thread.Sleep(3000);
            FindBy(By.Id("com.shapr:id/button_menu_save"), "Photo prise, Bouton [Enregistrer]").Click(); //enregistrer
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Suivant]", false).Click(); //suivant
            // -------  Exp utilisateur  --------- //
            RPComment(" --- Etape: Expérience Professionnelle --- ", ReportPortal.Client.Models.LogLevel.Warning);
            Thread.Sleep(3000);
            // je suis etudiant
            FindBy(By.Id("com.shapr:id/switch_student"), "Clique Switch [Etudiant]").Click();
            IWebElement year = _driverANDROID.FindElementsByXPath("//*[@class=\"android.widget.NumberPicker\"]").FirstOrDefault();
            _driverANDROID.Swipe(year.Location.X, year.Location.Y, year.Location.X, year.Location.Y - 250, 500);

            IWebElement month = _driverANDROID.FindElementsByXPath("//*[@class=\"android.widget.NumberPicker\"]").LastOrDefault();
            _driverANDROID.Swipe(month.Location.X, month.Location.Y, month.Location.X, month.Location.Y - 250, 500);

            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Suivant]").Click();//Suivant

            // -------  Mon activité  --------- //
            RPComment(" --- Etape: Mon activité --- ", ReportPortal.Client.Models.LogLevel.Warning);
            IWebElement role = FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Saisie [Champs poste]", false, true);
            role.SendKeys("Testeur");  role.SendKeys("Testeur"); //rôle
            IWebElement company = FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Saisie [Champs entreprise]", false, true);
            company.SendKeys("TD"); //entreprise.
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Enregistrer]").Click(); //enregistrer.
            // -------  Formation  --------- //
            RPComment(" --- Etape: Formation --- ", ReportPortal.Client.Models.LogLevel.Warning);
            IWebElement diplome = FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Saisie [Champs diplôme]", false, true);
                diplome.SendKeys("Testeur"); //Ecole
            IWebElement univ = FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Saisie [Champs université]", false, true);
                univ.SendKeys("TD"); //entreprise.
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Enregistrer]", true).Click(); //enregistrer.
            // --------   Interêts   -------- //
            RPComment(" --- Etape: Interêts --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/edit_text_interest"), "Saisie du 1er interêt", false, true).SendKeys("Management"); //saisie interet
            FindBy(By.Id("com.shapr:id/text_view_add"), "Lien [Ajouter]").Click(); //ajouter l'interet
            FindBy(By.Id("com.shapr:id/edit_text_interest"), "Saisie du 2eme interêt").SendKeys("Testeur"); //saisie interet
            FindBy(By.Id("com.shapr:id/text_view_add"), "Lien [Ajouter]").Click(); //ajouter l'interet
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Suivant]", true).Click();
            // ----------   Objectifs   ------------ //
            RPComment(" --- Etape: Objectifs --- ", ReportPortal.Client.Models.LogLevel.Warning);
            Thread.Sleep(2500);
            FindBy(By.XPath("//*[@text=\"Développer mon business\"]"), "Choix d'un objectif").Click(); //choix d'un objectif
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Suivant]").Click(); //Suivant
            FindBy(By.Id("com.shapr:id/edit_text_signup_bio"), "Saisie [texte presentation]").SendKeys(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                " Ut enim ad minim veniam, quis"); //Text presentation
            FindBy(By.Id("com.shapr:id/button_next"), "Bouton [Allons-y]", true).Click(); //valider
            RPComment(" --- Page accueil /Connecté --- ", ReportPortal.Client.Models.LogLevel.Warning);
        }

        [TestCase(TestName = "Parametres utilisateur",  //Ignore = "pour CI",
            Author = "TESTING DIGITAL")]
        public void Parametres()
        {
            Connexion();
            ProfileUtilisateur();
            FindBy(By.Id("com.shapr:id/button_profile_menu_settings"), "Bouton entête [parametres]").Click();
            Thread.Sleep(3500);
            // -------------  Alertes e-mail  ------------------//
            FindBy(By.Id("com.shapr:id/switch_settings_fragment_notifications"), "Switch [Notifications]").Click();
            FindBy(By.Id("com.shapr:id/switch_settings_fragment_newsletter"), "Switch [Newsletter]", false).Click();
            //Thread.Sleep(15000);
            ////-------------  Mon Compte  ------------------//
            ////-----  CG
            //FindBy(By.XPath("//*[@content-desc='Plus d'options']"), "plus d'options [...]").Click();  //plus d'options [...]
            //FindBy(By.XPath("//*[@text=\"Conditions\"]"), "Conditions").Click();
            //HeaderRetour();
            ////-----  Politiques de confidentialité
            //FindBy(By.XPath("//*[@content-desc=\"Plus d'options\"]"), "plus d'options [...]").Click();  //plus d'options [...]
            //FindBy(By.XPath("//*[@text=\"Politique de confidentialité\"]"), "Politique de confidentialité").Click();
            //HeaderRetour();
            ////-----  Suppression comptes
            //FindBy(By.XPath("//*[@content-desc=\"Plus d'options\"]"), "plus d'options [...]").Click();  //plus d'options [...]
            //FindBy(By.XPath("//*[@text=\"Supprimer mon compte\"]"), "Supprimer mon compte").Click();
            //HeaderRetour();
            //-----  Modification Email
            RPComment(" --- Etape: Modification Email --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_settings_fragment_change_email"), "Lien [Modifier mon e-mail]").Click();
            FindBy(By.Id("com.shapr:id/edit_text_email"), "Champs e-mail").SendKeys(email); //Champs email
            Assert.IsNotNull(FindBy(By.Id("com.shapr:id/done_btn"), "Verif. presence bouton [TERMINE]", true)); //Verif. presence bouton
            HeaderRetour();
            // -----  Modification mdp
            RPComment(" --- Etape: Modification mot de passe --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_settings_fragment_change_password"), "Lien [Modifier mon mot de passe]").Click(); //acceder a la page mot de passe
            FindBy(By.Id("com.shapr:id/password_current_txt"), "Champ [Entrer mdp actuel]").SendKeys("");
            FindBy(By.Id("com.shapr:id/password_new_txt"), "Champ [Entrer nouveau mdp (8-32car.)]").SendKeys("");
            FindBy(By.Id("com.shapr:id/password_confirm_txt"), "Champ [Confirmer le nouveau mdp]").SendKeys("");
            Assert.IsNotNull(FindBy(By.Id("com.shapr:id/password_forgotten"), "Lien [Je ne connais pas mon mot de passe]"));
            Assert.IsNotNull(FindBy(By.Id("com.shapr:id/done_btn"), "Verif. presence bouton [TERMINE]", false)); //Verif. presence bouton
            HeaderRetour();
            // -----  FAQ & Nous contacter
            RPComment(" --- Etape: FAQ & Nous contacter --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_settings_fragment_faq_and_contact"), "Lien [FAQ & Nous contacter]").Click();
            Assert.IsNotNull(FindBy(By.Id("com.shapr:id/button_menu_contact_us"), "NOUS CONTACTER")); //Verifier presence
            HeaderRetour();
            // -----  Restaurer les achats // Version
            try
            {
                Assert.IsNotNull(FindBy(By.Id("com.shapr:id/text_view_settings_fragment_restore_purchase"), "Lien [Restaurer mes achats]"));
            }
            catch (Exception)
            {
                //Scroll a ajouter pour ecrans reduit.
            }
        }

        [TestCase(TestName = "LinkedIn",  //Ignore = "pour CI",
            Author = "TESTING DIGITAL")]
        public void LinkedIn()
        {
            Deconnexion();
            RPComment(" --- Accueil --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/button_welcome_fragment_linkedin"), "Bouton [m'inscrire avec email]").Click(); //Clique m'inscrire avec email.
            RPComment(" --- Page LinkedIn --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("username"), "Saisie Email").SendKeys("default@testingdigital.com"); //Saisie Email
            FindBy(By.Id("password"), "Saisie mot de passe").SendKeys("Byron2017*"); //Clique me connecter.
            //FindBy(By.Id("com.shapr:id/button_signin_fragment_b_signin"), "Bouton [Connexion]").Click();
        }

        [TestCase(TestName = "Compte utilisateur")]
        public void CompteUtilisateur()
        {
            ToolTips();//1
            ToolTips();//2
            Connexion();
            ProfileUtilisateur();
            //Identité
            RPComment(" --- Identité --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_view_card_header_full_name"), "Page profil, Lien Prenom|Nom", true)
                .Click(); //lien
            FindBy(By.Id("com.shapr:id/identity_firstname_txt"), "Page profil, Champs Prénom").Clear(); //champs prenom
            FindBy(By.Id("com.shapr:id/identity_firstname_txt"), "Page profil, Champs Prénom", false)
                .SendKeys("TestAUTO"); //champs prenom
            FindBy(By.Id("com.shapr:id/identity_lastname_txt"), "Page profil, Champs Nom").Clear(); //champs nom
            FindBy(By.Id("com.shapr:id/identity_lastname_txt"), "Page profil, Champs Nom", false, true).SendKeys("TesteurAUTO"); //champs nom
            HeaderRetour();
            //poste actuel
            RPComment(" --- Poste actuel --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/text_view_view_card_header_occupation"), "profile -> Poste").Click();
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Page post actuel, supprimer poste actuel")
                .Clear(); //champs poste
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Page post actuel, Saisie poste actuel", false, true)
                .SendKeys("Automation"); //champs poste
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Page post actuel, supprimer organisation actuel")
                .Clear(); //champs organisation
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Page post actuel, Saisie organisation actuel", false, true)
                .SendKeys("TestingDigital"); //champs organisation
            HeaderRetour();
            // check interet
            RPComment(" --- Interêts --- ", ReportPortal.Client.Models.LogLevel.Warning);
            FindBy(By.Id("com.shapr:id/layout_view_card_poi"), "Interêts").Click();
            Assert.IsTrue(
            FindBy(By.Id("com.shapr:id/edit_text_interest"), "Verif. Presence des interêts").Displayed);
            HeaderRetour();

            //objectifs
            RPComment(" --- Objectifs --- ", ReportPortal.Client.Models.LogLevel.Warning);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector()" +
                    ".textContains(\"Objectifs actuels\"))")
                    .Click();

            //FindBy(By.Id("com.shapr:id/text_view_save"), "Bouton [Enregistrer]").Click(); //Enregistrer
            HeaderRetour();

            //Bio
            RPComment(" --- Bio --- ", ReportPortal.Client.Models.LogLevel.Warning);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector()" +
                ".textContains(\"Lorem ipsum\"))")
                .Click();

            HeaderRetour();
            //contact
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector()" +
                ".textContains(\"Pour Prendre Contact\"))")
                .Click();
            RPComment(" --- Contact --- ", ReportPortal.Client.Models.LogLevel.Warning);
            HeaderRetour();

            //Secteur d'activité
            RPComment(" --- Experience --- ", ReportPortal.Client.Models.LogLevel.Warning);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/layout_view_profile_experience\"))").Click();
            HeaderRetour();
            //Secteur d'activité
            RPComment(" --- Secteur d'activité --- ", ReportPortal.Client.Models.LogLevel.Warning);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/layout_view_profile_industries\"))")
                .Click();
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();
            //Organisation precedente
            RPComment(" --- Organisation precedente --- ", ReportPortal.Client.Models.LogLevel.Warning);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/layout_view_profile_organizations\"))")
                .Click();
            FindBy(By.Id("com.shapr:id/button_menu_sortable_list_add"), "Bouton [Ajouter]").Click();
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Champs [Poste]").SendKeys("");
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Champs [Organisation]", false).SendKeys("");
            //Assert.IsTrue( //Check enregistrer
            //    FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Displayed);
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();
            Thread.Sleep(1500);
            if (UDID != "ce03171339068c0b0c")
                HeaderRetour();
            //Formation
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/layout_view_profile_education\"))")
                .Click();
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_role"), "Champs [Diplôme]").SendKeys("");
            FindBy(By.Id("com.shapr:id/text_view_item_edit_occupation_company"), "Champs [Ecole]").SendKeys("");
            //Assert.IsTrue( //Check enregistrer
            //    FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer", true).Displayed);
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();
            //URL LinkedIn
            try
            {
                FindBy(By.Id("com.shapr:id/text_view_view_card_links_linkedin"), "Lien [LinkedIn]").Click();
            }
            catch (Exception)
            {
                RPComment(" --- Profile -> LinkedIn --- ", ReportPortal.Client.Models.LogLevel.Info);
                _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                               ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                               "com.shapr:id/text_view_view_card_links_linkedin\"))").Click();
            }

            var champsLinkedIn = FindBy(By.Id("com.shapr:id/edit_text_edit_profile_link_content"), "Champs url [LinkedIn]");
            champsLinkedIn.Clear();
            champsLinkedIn.SendKeys("LinkedInTxt");
            //champsLinkedIn.Clear();
            //Assert.IsTrue( //Check enregistrer
            //    FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. presence bouton [Enregistrer]").Displayed);
            //Thread.Sleep(2500);
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();

            //URL web
            RPComment(" --- Profile -> Web --- ", ReportPortal.Client.Models.LogLevel.Info);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/text_view_view_card_links_website\"))")
                .Click();

            var champsLink = FindBy(By.Id("com.shapr:id/edit_text_edit_profile_link_content"), "Champs url [Web]");
            champsLink.Clear();
            champsLink.SendKeys("JustTextNotURL");
            champsLink.Clear();
            champsLink.SendKeys("https://testingdigital.com");
            //Assert.IsTrue( //Check enregistrer
            //    FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. presence bouton [Enregistrer]", true).Displayed);
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();

            //url twitter
            RPComment(" --- Profile -> Twitter --- ", ReportPortal.Client.Models.LogLevel.Info);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/text_view_view_card_links_twitter\"))")
                .Click();
            var champsTwitter = FindBy(By.Id("com.shapr:id/edit_text_edit_profile_link_content"), "Champs url [Twitter]");
            champsTwitter.Clear();
            champsTwitter.SendKeys("https://testingdigital.com");
            champsTwitter.Clear();
            champsTwitter.SendKeys("JustTextNotURL");

            //Assert.IsTrue( //Check enregistrer
            //    FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. presence bouton [Enregistrer]", false).Displayed);
            //HeaderRetour();
            FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. bouton Enregistrer").Click();

            //url instagram
            RPComment(" --- Profile -> Instagram --- ", ReportPortal.Client.Models.LogLevel.Info);
            _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
                                                           ".scrollIntoView(new UiSelector().resourceId(\"" +
                                                           "com.shapr:id/text_view_view_card_links_instagram\"))")
                .Click();
            var champsInstagram = FindBy(By.Id("com.shapr:id/edit_text_edit_profile_link_content"), "Champs url [Instagram]");
            champsInstagram.Clear();
            champsInstagram.SendKeys("JustTextNotURL");
            champsInstagram.Clear();
            champsInstagram.SendKeys("https://testingdigital.com");
            Assert.IsTrue( //Check enregistrer
                FindBy(By.Id("com.shapr:id/text_view_save"), "Verif. presence bouton [Enregistrer]", true).Displayed);
            HeaderRetour();
        }

        [TestCase(TestName = "Recherche",  //Ignore = "en cours..",
            Author = "TESTING DIGITAL")]
        public void Recherche()
        {
            Inscription();
            ToolTips();//1
            ToolTips();//2
            FindBy(By.Id("com.shapr:id/image_view_search"), "Switch [Recherche]").Click(); //recherche
                                                                                           // -------- Recherche avec resultat -------- //
                                                                                           //poste
            FindBy(By.Id("com.shapr:id/edit_text_free_text"), "Champs [Poste]").SendKeys("Testeur"); //
            FindBy(By.Id("com.shapr:id/text_view_add"), "[Ajouter]").Click(); //

            //objectif
            //tag //localisation
            IReadOnlyCollection<IWebElement> inputsElement =
                _driverANDROID.FindElementsByXPath("//*[@resource-id='com.shapr:id/edit_text_filter']"); //
            foreach (IWebElement input in inputsElement)
            {
                input.SendKeys("Paris");
            }
            //Check resultat !!
            FindBy(By.Id("com.shapr:id/button_swipe_profile"), "Validation du formulaire de recherche", false).Click(); //Valider le formulaire
            FindBy(By.Id("com.shapr:id/view_swipe_fragment_results_bar"), "", true);
            FindBy(By.Id("com.shapr:id/card_view_view_card"), "Présence de carte utilisateur en resultat"); //Verifier la presence de la carte
            FindBy(By.Id("com.shapr:id/view_swipe_fragment_results_bar"), "Switch [Recherche]").Click();//Effacher tout!
            FindBy(By.Id("com.shapr:id/button_clear_all"), "Bouton [Effacer tout!]").Click();
            // -------- Recherche sans resultat -------- //
            FindBy(By.Id("com.shapr:id/edit_text_free_text"), "Champs [Poste]").SendKeys("sdsdsdsdsdsd"); //
            FindBy(By.Id("com.shapr:id/text_view_add"), "[Ajouter]").Click(); //
            FindBy(By.Id("com.shapr:id/button_swipe_profile"), "Validation du formulaire de recherche").Click(); //Valider le formulaire
            FindBy(By.Id("com.shapr:id/smart_text_view_search_form_header_content")
                            , "Presence du bandau & message suite a la recherche sans resultat")
                            .Text.Contains("Pas de résultats");
        }

        [Order(1)]
        [TestCase(TestName = "Swipe (Nouveau Utilisateur)",  //Ignore = "en cours",
            Author = "TESTING DIGITAL")]
        public void Swipe()
        {
            Inscription(); //NSCRIPTION NECESSAIRE
            //---------------  MatchinBox  --------------
            //FindBy(By.Id("com.shapr:id/image_view_home_swipe_rewind"), "Verif. presence [Rewind]");  
            //FindBy(By.Id("com.shapr:id/image_view_home_swipe_super_swipe"), "Verif. presence [SuperSwipe]");  
            FindBy(By.Id("com.shapr:id/home_swipe_pass_btn"), "Verif. presence [PASS]");  
            FindBy(By.Id("com.shapr:id/home_swipe_meet_btn"), "MEET avec la premiere carte").Click();  
            //-----  confirmation PopIn(MEET)
            try
            {
                // constat: uniquement apres l'installation de l'app
                FindBy(By.Id("android:id/button1"), "confirmation MEET").Click(); //OK
            }
            catch (Exception)
            {}
            //-----  MATCH  --  it's a MATCH  --  Nv message
            FindBy(By.Id("com.shapr:id/match_close_btn"), "PopIn (its a MATCH!), Verif. Presence bouton Fermer [x]");
            FindBy(By.Id("com.shapr:id/match_message_btn"), "Verif. presence du message").Click();
            //---------------  Icebreaker / Envoie message  --------------
            FindBy(By.Id("com.shapr:id/text_view_icebreaker_content"), "Verif. Presence texte IceBreaker");
            FindBy(By.Id("com.shapr:id/image_arrow"), "Flêche, pour basculer au prochain message"); // fleche navigation ice breaker
            FindBy(By.Id("com.shapr:id/edit_text_messaging_message"), "Champs de texte").Click();   // champs texte
            HeaderRetour();
            FindBy(By.Id("com.shapr:id/image_view_home_swipe_super_swipe"), "Clique icone Super swipe").Click();

            _driverANDROID.CloseApp();
            _driverANDROID.LaunchApp();

            //---------------  Super swipe  --------------
            ToolTips(); ToolTips(); ToolTips();
            try
            {
                FindBy(By.Id("com.shapr:id/image_view_home_swipe_super_swipe"), "Clique icone Super swipe").Click();
            }
            catch (Exception)
            {
            }

            //-----  PopIn Premium
            FindBy(By.Id("com.shapr:id/image_view_premium_carousel_icon"), "PopIn offres [PREMIUM], Verif. icône du milieur presente");
            FindBy(By.Id("com.shapr:id/text_view_premium_carousel_title"), "PopIn offres [PREMIUM], Verif. titre");
            FindBy(By.Id("com.shapr:id/text_view_unlock"), "PopIn offres[PREMIUM], Verif.icône du milieur presente");
            FindBy(By.Id("com.shapr:id/text_view_term_of_use"), "PopIn offres[PREMIUM], Conditions");
            FindBy(By.Id("com.shapr:id/text_view_policy_privacy"), "PopIn offres[PREMIUM], Privacy");
            FindBy(By.Id("com.shapr:id/image_view_premium_close"), "PopIn offres[PREMIUM], fermer").Click(); //fermer
            //-----  PASS
            FindBy(By.Id("com.shapr:id/home_swipe_pass_btn"), "carte PASS").Click();
            PopUpOk();
            //---------------  Rewind  --------------
            FindBy(By.Id("com.shapr:id/image_view_home_swipe_rewind"), "Clique [Rebobiner]").Click();
            //---------------  Daily limite (timer)  --------------
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    FindBy(By.Id("com.shapr:id/home_swipe_pass_btn"), "PASS carte N: "+i).Click(); // En boucle
                }
            }
            catch (Exception)
            {
                //FindBy(By.Id("com.shapr:id/home_timer_tv"), "Limite atteinte, Timer indique:");// Timer UP // afficher le temps de recup)
                //FindBy(By.Id("com.shapr:id/text_view_home_timer_doubled_batch"), "Voir + de profile");
                //FindBy(By.Id("com.shapr:id/title_button_shapr_pro"), "Voir +"); // Bandau Shapr Pro (Voir +)
            }
            ToolTips();
            //popup  id	com.shapr:id/text_view_tooltip_ok
            //Dite Bonjour
            //if (UDID!= "2XJDU17725001264" || UDID!= "1cc466b440027ece" || UDID != "0123456789ABCDEF")
            //{
            //    try
            //    {
            //        var DiteBonjour = _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
            //                                       ".scrollIntoView(new UiSelector().resourceId(\"" +
            //                                       "com.shapr:id/text_view_item_meet_pending_action\"))");
            //        Thread.Sleep(1500);
            //        DiteBonjour.Click();
            //    }
            //    catch (Exception)
            //    {
            //        var CardMatch = _driverANDROID.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true))" +
            //                                       ".scrollIntoView(new UiSelector().resourceId(\"" +
            //                                       "com.shapr:id/image_view_item_meet_pending_background\"))");
            //        Thread.Sleep(1500);
            //        FindBy(By.Id("com.shapr:id/text_view_item_meet_pending_action")).Click();

            //    }

            //    FindBy(By.Id("com.shapr:id/edit_text_messaging_message")).SendKeys("Hello");
            //    FindBy(By.Id("com.shapr:id/image_view_messaging_send")).Click();
            //}

        }

        [TestCase(TestName = "Paiement",  //Ignore = "en cours",
            Author = "TESTING DIGITAL")]
        public void Paiement()
        {
            Connexion();
            // Parametres
            ProfileUtilisateur();
            //FindBy(By.Id("com.shapr:id/button_profile_menu_settings"), "Bouton entête [parametres]").Click();
            Thread.Sleep(3500);
            //try
            //{//Normalement
            //    FindBy(By.Id("com.shapr:id/text_view_settings_fragment_restore_purchase"));
            //    RPComment(" --- Process paiement --- ", ReportPortal.Client.Models.LogLevel.Warning);
            //    //Application du paiement ICI
            //    if (UDID!="")//Note S8+
            //    {
            //        FindBy(By.Id("com.shapr:id/text_view_premium_carousel_title")).Click();
            //        FindBy(By.Id("com.shapr:id/text_view_unlock")).Click();
            //        FindBy(By.XPath("//*[@content-desc=\"S'abonner\"]")).Click();
            //        FindBy(By.Id("com.android.vending:id/input")).SendKeys("Byron!019");
            //        FindBy(By.Id("//android.widget.Button[@content-desc=\"Valider\"]")).Click();
            //        //
            //        FindBy(By.Id("//*[@class='android.widget.RadioButton']"));
            //    }
            //}
            //catch (Exception)
            //{
            //    RPComment(" --- Compte en Mode Premium - Validé --- ", ReportPortal.Client.Models.LogLevel.Warning);
            //}


            FindBy(By.Id("com.shapr:id/text_view_teleport_title"), "Teleporte / Premium", true).Click(); // Teleporte 
            FindBy(By.Id("com.shapr:id/text_view_unlock"), "").Click(); // Offre - activer
        }
    }
}