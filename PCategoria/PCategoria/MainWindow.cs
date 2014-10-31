
using Gtk;
using System;
using System.Data;
using SerpisAd;
using PArticulo;
public partial class MainWindow: Gtk.Window
{	
	private ListStore listStore;
	private MySqlConnection mySqlConnection;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		treeView.AppendColumn("id",new CellRendererText(),"text",0);
		treeView.AppendColumn("nombre",new CellRendererText(),"text",1);
		
		listStore = new ListStore (typeof(string),typeof(string));
		
		treeView.Model=listStore;
		
		mySqlConnection= new MySqlConnection(
			"server = localhost; Database=bdproductos; user id = root; password=sistemas");
		mySqlConnection.Open();
		
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
		mySqlCommand.CommandText = "select * from categoria";
		
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		
		while(mySqlDataReader.Read()){
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues(id,nombre);
	
		}
	}
	
	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		listStore.AppendValues("1",DateTime.Now.ToString());
	}
	
	protected void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		listStore.Clear();
		
		listStore = new ListStore (typeof(string),typeof(string));
		
		treeView.Model=listStore;
		
		mySqlConnection= new MySqlConnection(
			"server = localhost; Database=bdproductos; user id = root; password=sistemas");
		mySqlConnection.Open();
		
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
		mySqlCommand.CommandText = "select * from categoria";
		
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		
		while(mySqlDataReader.Read()){
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues(id,nombre);
	
		}
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	

	

}
