using System.Globalization;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace DecisionProblemAutomator;

public class MainPage
{
    private IWebDriver _driver;
    public MainPage(IWebDriver driver) 
    {
        _driver = driver;
        PageFactory.InitElements(driver, this);
    }
    
    [FindsBy(How = How.Id, Using = "clips")]
    private IWebElement _clips;
    
    [FindsBy(How = How.Id, Using = "clipmakerRate")]
    private IWebElement _clipsPerSecond;
    
    [FindsBy(How = How.Id, Using = "avgSales")]
    private IWebElement _salesPerSecond;
    
    [FindsBy(How = How.Id, Using = "btnMakePaperclip")]
    private IWebElement _makePaperclipButton;
    
    [FindsBy(How = How.Id, Using = "btnLowerPrice")]
    private IWebElement _lowerPriceButton;
    
    [FindsBy(How = How.Id, Using = "btnRaisePrice")]
    private IWebElement _raisePriceButton;
    
    [FindsBy(How = How.Id, Using = "btnBuyWire")]
    private IWebElement _buyWireButton;

    [FindsBy(How = How.Id, Using = "wire")]
    private IWebElement _availableWire;
    
    [FindsBy(How = How.Id, Using = "wireCost")]
    private IWebElement _wireCost;

    [FindsBy(How = How.Id, Using = "funds")]
    private IWebElement _funds;

    [FindsBy(How = How.Id, Using = "unsoldClips")]
    private IWebElement _unsoldClips;

    [FindsBy(How = How.Id, Using = "margin")]
    private IWebElement _pricePerClip;
    
    [FindsBy(How = How.Id, Using = "demand")]
    private IWebElement _demand;
    
    [FindsBy(How = How.Id, Using = "adCost")]
    private IWebElement _adCost;
    
    [FindsBy(How = How.Id, Using = "btnExpandMarketing")]
    private IWebElement _upgradeMarketingButton;
    
    [FindsBy(How = How.Id, Using = "btnMakeClipper")]
    private IWebElement _makeAutoClipperButton;
    
    [FindsBy(How = How.Id, Using = "clipperCost")]
    public IWebElement _clipperCost;
    
    [FindsBy(How = How.Id, Using = "btnMakeMegaClipper")]
    private IWebElement _makeMegaClipperButton;
    
    [FindsBy(How = How.Id, Using = "megaClipperCost")]
    public IWebElement _megaClipperCost;

    public bool CanMakePaperclip()
    {
        return _makePaperclipButton.Enabled;
    }

    public void MakePaperClip()
    {
        _makePaperclipButton.Click();
    }

    public void LowerPrice()
    {
        _lowerPriceButton.Click();
    }

    public void RaisePrice()
    {
        _raisePriceButton.Click();
    }

    public bool CanBuyWire()
    {
        return _buyWireButton.Enabled;
    }

    public void BuyWire()
    {
        _buyWireButton.Click();
    }

    public bool CanUpgradeMarketing()
    {
        return _upgradeMarketingButton.Enabled;
    }
    public void UpgradeMarketing()
    {
        _upgradeMarketingButton.Click();
    }
    public bool CanBuyAutoClipper()
    {
        return _makeAutoClipperButton != null && _makeAutoClipperButton.Enabled;
    }
    public void MakeAutoclipper()
    {
        if (_makeAutoClipperButton != null)
            _makeAutoClipperButton.Click();
    }

    public bool CanBuyMegaClipper()
    {
        return _makeMegaClipperButton != null && _makeMegaClipperButton.Enabled;
    }
    public void BuyMegaClipper()
    {
        if (_makeMegaClipperButton != null)
            _makeMegaClipperButton.Click();
    }

    public void BuyBestAvailableClipper()
    {
        if (CanBuyMegaClipper())
            BuyMegaClipper();
        else if (CanBuyAutoClipper())
            MakeAutoclipper();
    }

    public int Clips => int.Parse(_clips.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"));
    public int ClipsPerSecond => int.Parse(_clipsPerSecond.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"));

    public int SalesPerSecond
    {
        get
        {
            try
            {
                return _salesPerSecond != null
                    ? int.Parse(_salesPerSecond.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"))
                    : -1;
            }
            catch
            {
                return -1;
            }
        }
    }
    public int WireRemaining => int.Parse(_availableWire.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"));
    public decimal WireCost => decimal.Parse(_wireCost.Text, NumberStyles.Any, new CultureInfo("en-US"));
    public decimal Funds => decimal.Parse(_funds.Text, NumberStyles.Any, new CultureInfo("en-US"));
    public int UnsoldClips => int.Parse(_unsoldClips.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"));
    public decimal PricePerClip => decimal.Parse(_pricePerClip.Text, NumberStyles.Any, new CultureInfo("en-US"));
    public int Demand => int.Parse(_demand.Text, NumberStyles.AllowThousands, new CultureInfo("en-US"));
    public decimal AdUpgradeCost => decimal.Parse(_adCost.Text, NumberStyles.Any, new CultureInfo("en-US"));
    public decimal AutoClipperCost => decimal.Parse(_clipperCost.Text, NumberStyles.Any, new CultureInfo("en-US"));
    public decimal MegaClipperCost => decimal.Parse(_megaClipperCost.Text, NumberStyles.Any, new CultureInfo("en-US"));

    #region Computation
    
    [FindsBy(How = How.Id, Using = "processors")]
    private IWebElement _processors;
    
    [FindsBy(How = How.Id, Using = "btnAddProc")]
    private IWebElement _addProcessorButton;

    [FindsBy(How = How.Id, Using = "memory")]
    private IWebElement _memory;
    
    [FindsBy(How = How.Id, Using = "btnAddMem")]
    private IWebElement _addMemoryButton;
    
    [FindsBy(How = How.Id, Using = "projectListTop")]
    private IWebElement _projects;
    
    public int Processors => _processors != null ? int.Parse(_processors.Text, NumberStyles.AllowThousands, new CultureInfo("en-US")) : 0;
    public int Memory => _memory != null ? int.Parse(_memory.Text, NumberStyles.AllowThousands, new CultureInfo("en-US")) : 0;

    public bool CanAddProcessor()
    {
        return _addProcessorButton != null && _addProcessorButton.Enabled;
    }
    public void AddProcessor()
    {
        if (_addProcessorButton != null)
            _addProcessorButton.Click();
    }
    public bool CanAddMemory()
    {
        return _addMemoryButton != null && _addMemoryButton.Enabled;
    }
    public void AddMemory()
    {
        if (_addMemoryButton != null)
            _addMemoryButton.Click();
    }

    public int NumberOfClickableProjects()
    {
        return _projects.FindElements(By.ClassName("projectButton")).Count(b => b.Enabled);
    }
    public void RunFirstAvailableProject()
    {
        var first = _projects.FindElements(By.ClassName("projectButton")).FirstOrDefault(b => b.Enabled);
        first?.Click();
    }


    #endregion
}