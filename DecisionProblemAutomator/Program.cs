// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Runtime.CompilerServices;
using DecisionProblemAutomator;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

Console.WriteLine("Hello, Paperclip-World!");


var options = new ChromeOptions();
options.AddExcludedArgument("enable-automation");
options.AddAdditionalOption("useAutomationExtension", false);
IWebDriver _driver = new ChromeDriver(options);
MainPage p = new MainPage(_driver);
_driver.Navigate().GoToUrl("https://www.decisionproblem.com/paperclips/index2.html");
while (true)
{
    if (Console.KeyAvailable)
    {
        Console.ReadLine();
        Console.WriteLine("Press any key to continue execution...");
        Console.ReadLine();
    }
    try
    {
        Console.WriteLine("Clips: {3}, Demand: {0}, Unsold: {1}, Wire: {2}", p.Demand, p.UnsoldClips, p.WireRemaining, p.Clips);
        if (p is { ClipsPerSecond: < 100, WireRemaining: > 20 })
        {
            for (int i = 0; i < 25; i++)
                p.MakePaperClip();
        }

        if (p.SalesPerSecond != -1)
        {
            if (p.PricePerClip > 0.01m && ((p.UnsoldClips > 300 && p.SalesPerSecond <= p.ClipsPerSecond) || p.UnsoldClips / (p.SalesPerSecond - p.ClipsPerSecond) > 20))
                p.LowerPrice();
            else if (p.SalesPerSecond > p.ClipsPerSecond && p.UnsoldClips < 50)
                p.RaisePrice();
        }
        else
        {
            switch (p)
            {
                case { UnsoldClips: > 500, PricePerClip: > 0.02m, WireRemaining: < 100 }:
                case { UnsoldClips: > 100, Demand: < 20, PricePerClip: >= 0.05m }:
                case { UnsoldClips: > 2000, PricePerClip: >= 0.01m }:
                    p.LowerPrice();
                    break;
                case { UnsoldClips: <= 2000, PricePerClip: < 0.03m }:
                    p.RaisePrice();
                    break;
                default:
                {
                    if (p is { Demand: > 100, UnsoldClips: < 10 })
                    {
                        p.RaisePrice();
                    }

                    break;
                }
            }
        }

        if (p.WireRemaining < 3000 && p.WireCost < 17)
        {
            p.BuyWire();
        }
        else if (p.WireRemaining < 300 && p.CanBuyWire())
        {
            p.BuyWire();
        }

        if (p.WireRemaining > 500 && p.CanUpgradeMarketing())
        {
            p.UpgradeMarketing();
        }

        if (p is { WireRemaining: > 500 })
        {
            p.BuyBestAvailableClipper();
        }

        if (p.CanAddProcessor())
        {
            if (p.Processors > p.Memory)
                p.AddMemory();
            else p.AddProcessor();
        }

        if (p.NumberOfClickableProjects() > 0)
        {
            p.RunFirstAvailableProject();
        }
    }
    catch {}

}