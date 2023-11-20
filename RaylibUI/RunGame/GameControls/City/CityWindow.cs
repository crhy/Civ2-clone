using System.Numerics;
using Civ2engine;
using Model;
using Raylib_cs;
using RaylibUI.BasicTypes.Controls;
using RaylibUI.Controls;

namespace RaylibUI.RunGame;

public class CityWindow : BaseDialog
{
    private readonly GameScreen _gameScreen;
    private readonly City _city;
    private readonly CityWindowLayout _cityWindowProps;
    private readonly HeaderLabel _headerLabel;

    public CityWindow(GameScreen gameScreen, City city) : base(gameScreen.Main)
    {
        _gameScreen = gameScreen;
        _city = city;

        _cityWindowProps = gameScreen.Main.ActiveInterface.GetCityWindowDefinition();

        _headerLabel = new HeaderLabel(this, _city.Name);

        LayoutPadding = new Padding(LayoutPadding,_headerLabel.GetPreferredHeight());
        
        DialogWidth = _cityWindowProps.Width + PaddingSide;
        DialogHeight = _cityWindowProps.Height + LayoutPadding.Top + LayoutPadding.Bottom;
        BackgroundImage = ImageUtils.PaintDialogBase(DialogWidth, DialogHeight, LayoutPadding.Top, LayoutPadding.Bottom, LayoutPadding.Left, _cityWindowProps.Image);
        
        Controls.Add(_headerLabel);

        var infoArea = new CityInfoArea(this)
        {
            AbsolutePosition = _cityWindowProps.InfoPanel
        };
        Controls.Add(infoArea);
        
        var buyButton = new Button(this,Labels.For(LabelIndex.Buy))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Buy"]
        };
        Controls.Add(buyButton);

        var changeButton = new Button(this, Labels.For(LabelIndex.Change))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Change"]
        };
        Controls.Add(changeButton);
        var infoButton = new Button(this, Labels.For(LabelIndex.Info))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Info"]
        };
        infoButton.Click += (_, _) => infoArea.Mode = DisplayMode.Info;
        Controls.Add(infoButton);

        // Map button
        var mapButton = new Button(this, Labels.For(LabelIndex.Map))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Map"]
        };
        mapButton.Click += (_, _) => infoArea.Mode = DisplayMode.SupportMap;
        Controls.Add(mapButton);
            

        // Rename button
        var renameButton = new Button(this, Labels.For(LabelIndex.Rename))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Rename"]
        };
        Controls.Add(renameButton);

        // Happy button
        var happyButton = new Button(this, Labels.For(LabelIndex.Happy))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Happy"]
        };
        happyButton.Click += (_, _) => infoArea.Mode = DisplayMode.Happiness;
        Controls.Add(happyButton);

        // View button
        var viewButton = new Button(this, Labels.For(LabelIndex.View))
        {
            AbsolutePosition = _cityWindowProps.Buttons["View"]
        };
        Controls.Add(viewButton);

        // Exit button
        var exitButton = new Button(this, Labels.For(LabelIndex.Close))
        {
            AbsolutePosition = _cityWindowProps.Buttons["Exit"]
        };
        exitButton.Click += CloseButtonOnClick;
        Controls.Add(exitButton);
    }

    private void CloseButtonOnClick(object? sender, MouseEventArgs e)
    {
        _gameScreen.CloseDialog(this);
    }

    public int DialogWidth { get; }

    public int DialogHeight { get; }

    public override void Resize(int width, int height)
    {
        SetLocation(width, DialogWidth, height, DialogHeight);
        _headerLabel.Bounds = new Rectangle(Location.X, Location.Y, DialogWidth, LayoutPadding.Top);
        foreach (var control in Controls)
        {
            control.OnResize();
        }
    }
}