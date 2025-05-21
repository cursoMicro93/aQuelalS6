using System.Text;
using aQuelalS6.Models;
using Newtonsoft.Json;

namespace aQuelalS6.Views;

public partial class vActionElim : ContentPage
{
	public vActionElim(Docente datos)
	{
		InitializeComponent();
        txtId.Text = datos.id.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();

	}

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var docenteActualizado = new
            {
                id = txtId.Text,
                nombre = txtNombre.Text,
                apellido = txtApellido.Text
            };

            string json = JsonConvert.SerializeObject(docenteActualizado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var url = $"http://127.0.0.1:54382/docentes/{txtId.Text}";
                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Docente actualizado correctamente", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"Código: {(int)response.StatusCode} - {response.ReasonPhrase}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }


    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
       
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"http://127.0.0.1:54382/docentes/{txtId.Text}";
                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Docente eliminado correctamente", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"Código: {(int)response.StatusCode} - {response.ReasonPhrase}", "Cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

}