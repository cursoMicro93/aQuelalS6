using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using aQuelalS6.Models;
using Newtonsoft.Json;

namespace aQuelalS6.Views;

public partial class vCrud : ContentPage
{
	private const string URL = "http://127.0.0.1:54382/docentes";
	private HttpClient cliente = new HttpClient();
	private ObservableCollection<Docente> _docenteTem;
	public vCrud()
	{
		InitializeComponent();
		mostrarDocentes();
	}

	public async void mostrarDocentes()
	{
		var content = await cliente.GetStringAsync(URL);
		List<Docente> lista = JsonConvert.DeserializeObject<List<Docente>>(content);
		_docenteTem = new ObservableCollection<Docente>(lista);
		lvDocentes.ItemsSource = _docenteTem;
	}

    private void btnAnadir_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new vAnadirDocente());
    }

    private void lvDocentes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var objDocente = (Docente)e.SelectedItem;
		Navigation.PushAsync(new vActionElim(objDocente));
    }
}