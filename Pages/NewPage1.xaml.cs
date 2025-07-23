using seirin1.ViewModels;

namespace seirin1.Pages;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
	}
    private void OnShowWindow1Click(object sender, System.EventArgs e)
    {
        CustomWindow1.IsVisible = true;
        CenterWindow(CustomWindow1);
    }
    private void OnShowWindow2Click(object sender, System.EventArgs e)
    {
        CustomWindow2.IsVisible = true;
        CenterWindow(CustomWindow2);
    }

    private void CenterWindow(Controls.CustomWindow window)
    {
        double windowWidth = window.WidthRequest > 0 ? window.WidthRequest : window.Width;
        double windowHeight = window.HeightRequest > 0 ? window.HeightRequest : window.Height;

        window.TranslationX = (Width - windowWidth) / 2;
        window.TranslationY = (Height - windowHeight) / 2;
    }
}