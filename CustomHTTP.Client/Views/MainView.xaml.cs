namespace CustomHTTP.Client.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }


    private void InsertCar_ButtonClicked(object sender, RoutedEventArgs e)
    {
        var view = new CreateCarView() { Owner = this };
        var viewModel = DataContext as MainViewModel;

        if (view.ShowDialog() == true)
        {
            var car = (view.DataContext as CreateCarViewModel).Car;

            viewModel.Car = car;
            viewModel.InsertCommand.Execute(null);
        }
    }

    private void SetCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = DataContext as MainViewModel;
        var car = CarList.SelectedValue as Car;

        if (viewModel.Cars.Count == 0) viewModel.Car = null;
        else viewModel.Car = new Car(ref car);
    }

    private void AppClose_ButtonClicked(object sender, RoutedEventArgs e) => Close();

    private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

    private void MinimizeWindow_ButtonClicked(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
}
