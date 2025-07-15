namespace seirin1.Pages;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count+= 10;

        if (count == 1)
            CounterBtn.Text = $"You love Musty {count}";
        else
            CounterBtn.Text = $"You love Musty {count}";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Hello", "World", "OK");
    }

   
}
