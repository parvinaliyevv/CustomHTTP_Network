namespace CustomHTTP.Client.Views;

public partial class CreateCarView : Window
{
    public CreateCarView()
    {
        InitializeComponent();

        DataContext = new CreateCarViewModel();
    }


    private void AppClose_ButtonClicked(object sender, RoutedEventArgs e) => Close();

    private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

    private void SaveCar_ButtonClicked(object sender, RoutedEventArgs e) => DialogResult = true;
}
