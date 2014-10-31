
using Gtk;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using PArticulo;
using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore, listStore2;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		
		Build ();
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;
		muestraArticulo ();
		muestraCategoria ();
		treeView.Selection.Changed += selectionChanged;
	
}

	void muestraArticulo()
	{
		dbConnection = App.Instance.DbConnection;
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		treeView.AppendColumn ("categoria", new CellRendererText (), "text", 2);
		treeView.AppendColumn ("precio", new CellRendererText (), "text", 3);
		listStore = new ListStore (typeof(ulong), typeof(string), typeof(string), typeof(string));
		treeView.Model = listStore;
		fillListStoreArticulo ();
	}	
	
	void muestraCategoria ()
	{
		dbConnection = App.Instance.DbConnection;
		treeview2.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeview2.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		listStore2 = new ListStore (typeof(ulong), typeof(string));
		treeview2.Model = listStore2;
		fillListStoreCategoria ();
	}
	
	private void selectionChanged (object sender, EventArgs e)
	{
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeview2.Selection.CountSelectedRows () > 0;
		deleteAction.Sensitive = hasSelected;
		editAction.Sensitive = hasSelected;
	}
	
	private void fillListStoreCategoria() {
		
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
		object id = dataReader ["id"];
		object nombre = dataReader ["nombre"];
		listStore2.AppendValues (id, nombre);
		}
		dataReader.Close ();
	}
	
		private void fillListStoreArticulo() {
		
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object categoria = dataReader ["categoria"];
			object precio = dataReader ["precio"].ToString();
			listStore.AppendValues (id, nombre, categoria, precio);
		}
	dataReader.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnAddActionActivated (object sender, System.EventArgs e)
	{
		throw new System.NotImplementedException ();
	}


	protected void OnDeleteActionActivated (object sender, System.EventArgs e)
	{
		 MessageDialog messageDialog = new MessageDialog (
            this,
            DialogFlags.Modal,
            MessageType.Question,
            ButtonsType.YesNo,
            "¿Quieres eliminar el registro?"
            );
        messageDialog.Title = Title;
        ResponseType response = (ResponseType) messageDialog.Run ();
        messageDialog.Destroy ();
        TreeIter treeIter;
        if (response != ResponseType.Yes)
            return;

        if (treeView.Selection.GetSelected (out treeIter)) {
            object id = listStore.GetValue (treeIter, 0);
            string deleteSql = string.Format ("delete from articulo where id={0}", id);
            IDbCommand dbCommand = dbConnection.CreateCommand ();
            dbCommand.CommandText = deleteSql;
            dbCommand.ExecuteNonQuery ();

        } else {treeview2.Selection.GetSelected (out treeIter);
                object id = listStore2.GetValue (treeIter, 0);
                string deleteSql = string.Format ("delete from categoria where id={0}", id);
                IDbCommand dbCommand = dbConnection.CreateCommand ();
                dbCommand.CommandText = deleteSql;

		}

	}

	protected void OnEditActionActivated (object sender, System.EventArgs e)
	{
		TreeIter treeIter;
        treeView.Selection.GetSelected (out treeIter);
        object id = listStore.GetValue (treeIter, 0);
        EditarAriculo articulo = new EditarArticulo (id);
	}


	protected void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		listStore.Clear();
		fillListStoreArticulo();
		listStore2.Clear();
		fillListStoreCategoria();
	}
	}