using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace databaseGame
{
    class Transat
    {
        static string url = "https://www.transatagentdirect.com/Pages/Login.aspx?ReturnUrl=%2f";
        public string lastName;
        public string bookingNum;
        public string departureDate;

        public Transat(string lN, string bN, string dD)
        {
            lastName = lN;
            bookingNum = bN;
            departureDate = dD;
        }

        static void textFill(IWebDriver driver, string xPath, string message)
        {
            IWebElement text = driver.FindElement(By.XPath(xPath));
            text.SendKeys(message);
        }

        static void buttonClick(IWebDriver driver, string xPath)
        {
            IWebElement button = driver.FindElement(By.XPath(xPath));
            button.Click();
        }

        public void transatRun()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            ////click Access
            string accessPath = "//*[@id=\"divText\"]/a[1]/img";
            buttonClick(driver, accessPath);

            ////change individual to group
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#rdBookingType > label:nth-child(4)")));
            IWebElement radioButton = driver.FindElement(By.CssSelector("#rdBookingType > label:nth-child(4)"));
            radioButton.Click();


            //fill lastName
            ////WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            ////wait.Until(ExpectedConditions.ElementIsVisible(By.Id("txtName_989067bd-f28f-44af-b569-799fec9846ff")));
            //IWebElement text = driver.FindElement(By.CssSelector("#txtName_5767ab87-8f9a-490d-a937-663e3e857502"));
            IList<IWebElement> elements = driver.FindElements(By.ClassName("boxForm"));
            IReadOnlyList<IWebElement> childs = elements[0].FindElements(By.XPath(".//*"));

            //text.SendKeys(lastName);
            IWebElement lastNameBox = childs[12];
            lastNameBox.SendKeys(lastName);

            //text.SendKeys(booking number);
            IWebElement bookNumBox = childs[22];
            bookNumBox.SendKeys(bookingNum);
            IWebElement departureDateBox = childs[27];
            departureDateBox.Clear();

            //text.SendKeys(departure date);
            departureDateBox.SendKeys(departureDate);

            //click generate button
            IWebElement generateButton = childs[44];
            Thread.Sleep(3000);
            generateButton.Click();

            //download pdf
            using (WebClient client = new WebClient())
            {
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                client.DownloadFile(driver.Url, @"C:\data\Temp.pdf");
            }

            //send email with attachment
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("webmail.toureast.com");

            mail.From = new MailAddress("no-reply@hopper.com");
            mail.To.Add("eric.li@toureast.com");
            //mail.To.Add("herman.h.tam@gmail.com");
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP mail webmail.toureast.com";
            Attachment attachment = new System.Net.Mail.Attachment(@"C:\data\Temp.pdf");
            mail.Attachments.Add(attachment);

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
            //SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            //errorStatus.Text = "Mail send successfully!";
            MessageBox.Show("mail Send");


            //IWebElement printLink = driver.FindElements(By.LinkText("Print"));
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.executeScript("arguments[0].setAttribute(arguments[1],arguments[2])", printLink, "download", "");
            //js.executeScript("arguments[0].setAttribute(arguments[1],arguments[2])", printLink, "target", "_blank");
        }
    }
}
