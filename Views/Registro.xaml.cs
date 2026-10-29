namespace dquinonezEXAMEN.Views;

public partial class Registro : ContentPage
{
    public Registro(string usuario)
    {
        InitializeComponent();
        lblUsuario.Text = $"Usuario conectado: {usuario}";
        dpFechaInicio.MinimumDate = DateTime.Today;
    }

    private async void btnCalcular_Clicked(object sender, EventArgs e)
    {
        // Validar campos vacios
        if (string.IsNullOrWhiteSpace(txtEdad.Text) || string.IsNullOrWhiteSpace(txtMontoInicial.Text))
        {
            await DisplayAlert("Atención", "Por favor ingresa la edad y el monto inicial.", "OK");
            return;
        }

        // edad mayor o igual a 16
        if (!int.TryParse(txtEdad.Text, out int edad) || edad < 16)
        {
            await DisplayAlert("Atención", "La edad debe ser mayor o igual a 16 años.", "OK");
            return;
        }

        // Monto Inicial entre 120 y 600
        if (!double.TryParse(txtMontoInicial.Text, out double montoInicial) || montoInicial < 120 || montoInicial > 600)
        {
            await DisplayAlert("Atención", "El monto inicial debe estar entre 120 y 600.", "OK");
            return;
        }

        
        double costoAnual = 1200;
        double saldoRestante = costoAnual - montoInicial;

        // 5 cuotas
        double cuotaBase = saldoRestante / 5;

        // 2% sobre el valor de la cuota
        double recargoPorcentaje = cuotaBase * 0.02;

        // Recargo por casillero
        double recargoCasillero = swCasillero.IsToggled ? 10 : 0;

        // Cálculo final
        double pagoMensual = cuotaBase + recargoPorcentaje + recargoCasillero;

        txtPagoMensual.Text = pagoMensual.ToString("F2");
    }

    private async void btnResumen_Clicked(object sender, EventArgs e)
    {
        // Validar que se haya calculado el pago mensual primero
        if (string.IsNullOrWhiteSpace(txtPagoMensual.Text))
        {
            await DisplayAlert("Atención", "Calcula el pago mensual antes de ver el resumen.", "OK");
            return;
        }

        // Extraer valores
        string usuarioActual = lblUsuario.Text.Replace("Usuario conectado: ", "");
        string plan = pckPlan.SelectedItem != null ? pckPlan.SelectedItem.ToString() : "No seleccionado";

        // Convertir montos a double para pasarlos
        double.TryParse(txtMontoInicial.Text, out double montoInicial);
        double.TryParse(txtPagoMensual.Text, out double pagoMensual);

        // Navegar y pasar los parámetros
        await Navigation.PushAsync(new Resumen(
            usuarioActual,
            txtNombre.Text,
            txtApellido.Text,
            txtEdad.Text,
            dpFechaInicio.Date.Value.ToString("d"),
            plan,
            swCasillero.IsToggled,
            montoInicial,
            pagoMensual
        ));
    }

    // limpiar todos los campos
    public void LimpiarCampos()
    {
        txtNombre.Text = string.Empty;
        txtApellido.Text = string.Empty;
        txtEdad.Text = string.Empty;
        txtMontoInicial.Text = string.Empty;
        txtPagoMensual.Text = string.Empty;

        pckPlan.SelectedIndex = -1; 
        swCasillero.IsToggled = false; // Apaga el switch
        dpFechaInicio.Date = DateTime.Today; // Regresa la fecha al día de hoy
    }
}