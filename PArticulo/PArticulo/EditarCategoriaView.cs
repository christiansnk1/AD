using System;
using System.Data;
using SerpisAd;
namespace PCategoria
	
{
	public partial class EditarCategoria : Gtk.Window
	{
		private object id;
		public EditarCategoria () : base(Gtk.WindowType.Toplevel)	{
		this.Build ();
	}
	public EditarCategoria(object id) : this() {
		this.id = id;
		IDbCommand dbCommand =
		App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = String.Format (
		"select * from categoria where id={0}", id
		);
		IDataReader dataReader = dbCommand.ExecuteReader ();
		dataReader.Read ();
		entryId.Text = dataReader ["id"].ToString ();
            entryNombre.Text = dataReader ["nombre"].ToString ();
		dataReader.Close ();
	}
	protected void OnSaveActionActivated (object sender, EventArgs e)
	{
		IDbCommand dbCommand =
		App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = String.Format (
		"update categoria set nombre=@nombre where id={0}", id
		);
			
		dbCommand.AddParameter ("id", entryId.Text);
		dbCommand.AddParameter ("nombre", entryNombre.Text);
		dbCommand.ExecuteNonQuery ();
		Destroy ();
		}
	}
}

