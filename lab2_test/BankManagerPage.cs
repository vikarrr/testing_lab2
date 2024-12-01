using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests.PageObjects
{
    public class BankingPage
    {
        private readonly IWebDriver driver;

        public BankingPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/");
        }

        public void ClickBankManagerLogin()
        {
            driver.FindElement(By.CssSelector(".center:nth-child(3) > .btn")).Click();
        }

        public void ClickCustomersButton()
        {
            driver.FindElement(By.CssSelector(".btn-lg:nth-child(3)")).Click();
        }

        public void DeleteFirstCustomer()
        {
            var firstCustomerDeleteButton = driver.FindElement(By.CssSelector("table tbody tr:nth-child(1) td:nth-child(5) button"));
            Console.WriteLine("Deleting first customer: " + GetCustomerInfo(0));
            firstCustomerDeleteButton.Click();
        }

        public int GetCustomersCount()
        {
            var rows = driver.FindElements(By.XPath("//tbody/tr")); // Без перевірки на Displayed

            Console.WriteLine($"Total customers found (including hidden): {rows.Count}");

            // Перевірка та логування кожного рядка, щоб знайти причину помилки
            foreach (var row in rows)
            {
                Console.WriteLine($"Row Text: {row.Text}, Displayed: {row.Displayed}");
            }

            return rows.Count;
        }

        public int GetCustomersCountWithLog()
        {
            var rows = GetCustomerRows();
            Console.WriteLine($"Found {rows.Count} visible customers.");


            return rows.Count;
        }

        public string GetCustomerInfo(int index)
        {
            var rows = GetCustomerRows();
            return index < rows.Count ? rows[index].Text : "Customer not found";
        }

        private IList<IWebElement> GetCustomerRows()
        {
            return driver.FindElements(By.XPath("//tbody/tr")).Where(e => e.Displayed).ToList();
        }
    }
}
