using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace aQuelalS6.Views;

public partial class vAnadirDocente : ContentPage
{
	public vAnadirDocente()
	{
		InitializeComponent();
	}

    private async void  btnAnadir_Clicked(object sender, EventArgs e)
    {
        {
            try
            {
                var docente = new
                {
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text
                };

                string json = JsonConvert.SerializeObject(docente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync("http://127.0.0.1:54382/docentes", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new vCrud());
                    }
                    else
                    {
                        await DisplayAlert("Error", $"Código HTTP: {(int)response.StatusCode} - {response.ReasonPhrase}", "Cerrar");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cerrar");
            }
        }
    }
}