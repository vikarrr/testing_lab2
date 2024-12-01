using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.PageObjects;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumTests.Steps
{
    [Binding]
    public class BankingSteps
    {
        private IWebDriver driver;
        private BankingPage bankingPage;
        private int initialCustomerCount;


        [Given(@"I am on the Banking Project homepage")]
        public void GivenIAmOnTheBankingProjectHomepage()
        {
            driver = new ChromeDriver();
            bankingPage = new BankingPage(driver);
            bankingPage.GoToHomePage();
            Thread.Sleep(1000);
        }

        [When(@"I click on the Bank Manager Login button")]
        public void WhenIClickOnTheBankManagerLoginButton()
        {
            bankingPage.ClickBankManagerLogin();
            Thread.Sleep(1000);
        }

        [When(@"I click on the Customers button")]
        public void WhenIClickOnCustomersButton()
        {
            bankingPage.ClickCustomersButton();
            Thread.Sleep(1000);
            initialCustomerCount = bankingPage.GetCustomersCountWithLog();  // Отримуємо кількість клієнтів після відкриття
            Thread.Sleep(1000);
        }

        [When(@"I delete the first customer in the list")]
        public void WhenIDeleteTheFirstCustomerInTheList()
        {
            bankingPage.DeleteFirstCustomer();
            Thread.Sleep(5000);
        }

        [Then(@"the customer should be removed from the list")]
        public void ThenTheCustomerShouldBeRemovedFromTheList()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Очікуємо, поки кількість клієнтів зменшиться
            bool isCustomerRemoved = wait.Until(driver =>
            {
                var updatedCustomersCount = bankingPage.GetCustomersCountWithLog();  // Отримуємо актуальну кількість клієнтів
                Console.WriteLine($"Initial customer count: {initialCustomerCount}, Current customer count: {updatedCustomersCount}");

                return updatedCustomersCount < initialCustomerCount;
            });

            Assert.IsTrue(isCustomerRemoved, "Customer was not deleted.");
        }



        [AfterScenario]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
