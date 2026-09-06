using StudentAdministration.UI.Views;

namespace StudentAdministration.UI;

public partial class AppShell : Shell
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AppShell" /> class.
    /// </summary>
    /// <remarks>
    ///     This constructor registers the Syncfusion license and initializes the components of the shell.
    /// </remarks>
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AddEditStudentPage), typeof(AddEditStudentPage));
        Routing.RegisterRoute(nameof(AddGradePage), typeof(AddGradePage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}