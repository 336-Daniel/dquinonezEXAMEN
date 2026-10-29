namespace dquinonezEXAMEN.Views;

public partial class Resumen : ContentPage
{
    public Resumen(string usuario, string nombre, string apellido, string edad, string fecha, string plan, bool casillero, double montoInicial, double pagoMensual)
    {
        InitializeComponent();

        lblUsuario.Text = $"Usuario conectado: {usuario}";
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad;
        lblFecha.Text = fecha;
        lblPlan.Text = plan;
        lblCasillero.Text = casillero ? "Si" : "No";
        lblMontoInicial.Text = montoInicial.ToString("F2");
        lblPagoMensual.Text = pagoMensual.ToString("F2");

        // Calcular el Pago Total
        double pagoTotal = montoInicial + (pagoMensual * 5);
        lblPagoTotal.Text = pagoTotal.ToString("F2");
    }

    private async void btnNuevoRegistro_Clicked(object sender, EventArgs e)
    {
        
        var paginas = Navigation.NavigationStack;

        
        if (paginas.Count >= 2)
        {
            var paginaRegistro = paginas[paginas.Count - 2] as Registro;

            
            if (paginaRegistro != null)
            {
                paginaRegistro.LimpiarCampos();
            }
        }

        
        await Navigation.PopAsync();
    }

    private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Recopilar y dar formato a todos los datos que están en la pantalla
            string contenido = "--- REPORTE DE SOCIO ---\n" +
                               $"{lblUsuario.Text}\n" +
                               $"Nombre: {lblNombre.Text}\n" +
                               $"Apellido: {lblApellido.Text}\n" +
                               $"Edad: {lblEdad.Text}\n" +
                               $"Fecha de inicio: {lblFecha.Text}\n" +
                               $"Plan: {lblPlan.Text}\n" +
                               $"Casillero: {lblCasillero.Text}\n" +
                               $"Monto Inicial: {lblMontoInicial.Text}\n" +
                               $"Pago Mensual: {lblPagoMensual.Text}\n" +
                               $"Pago Total: {lblPagoTotal.Text}\n";

            // 2. Definir el nombre del archivo (usando el nombre y apellido para identificarlo)
            string nombreArchivo = $"Reporte_{lblNombre.Text}_{lblApellido.Text}.txt";

            // 3. Obtener la ruta de almacenamiento seguro de la aplicación en el dispositivo
            string rutaDirectorio = FileSystem.AppDataDirectory;
            string rutaCompleta = Path.Combine(rutaDirectorio, nombreArchivo);

            // 4. Crear el archivo y escribir el texto en él
            File.WriteAllText(rutaCompleta, contenido);

            // 5. Mostrar la alerta requerida con el nombre y la ruta
            await DisplayAlert("Exportación Exitosa",
                               $"Archivo generado correctamente.\n\n" +
                               $"Nombre: {nombreArchivo}\n\n" +
                               $"Ruta: {rutaCompleta}",
                               "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al exportar: {ex.Message}", "OK");
        }
    }
}