namespace dquinonezEXAMEN.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
        string[,] credenciales = new string[4, 2]
        {
        { "recepcion", "gym2026" },
        { "entrenador", "fit_01" },
        { "uisrael", "2026" },
        { "gerente", "admin123" }
        };

        string usuario = txtUsuario.Text;
        string contrasena = txtContraseña.Text;
        bool credencialesValidas = false;

        for (int i = 0; i < 4; i++)
        {
            if (credenciales[i, 0] == usuario && credenciales[i, 1] == contrasena)
            {
                credencialesValidas = true;
                break;
            }
        }

        if (credencialesValidas)
        {
            await Navigation.PushAsync(new Registro(usuario));
        }
        else
        {
            await DisplayAlert("Atención", "Credenciales Incorrectas", "OK");
        }
    }

    private void btnAcercaDe_Clicked(object sender, EventArgs e)
    {
        DisplayAlertAsync("Acerca de", "Nombre: Daniel Quiñonez\n" +
                                        "Carrera: Ing. en Sistemas\n"
                                        + "Paralelo: A", "ok");

    }
}