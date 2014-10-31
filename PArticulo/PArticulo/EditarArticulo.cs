using System;
using System.Data;
using SerpisAd;
namespace PCategoria
	
{
public partial class EditarArticulo : Gtk.Window
{
	private object id;
	public EditarArticulo () : base(Gtk.WindowType.Toplevel)	{
	this.Build ();
	}
	public EditarArticulo(object id) : this() {
		this.id = id;
		IDbCommand dbCommand =
		App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = String.Format (
		"select * from articulo where id={0}", id
		);
		IDataReader dataReader = dbCommand.ExecuteReader ();
		dataReader.Read ();
		entry2.Text = dataReader ["id"].ToString ();
        entry3.Text = dataReader ["nombre"].ToString ();
        entry4.Text = dataReader ["categoria"].ToString ();
        entry5.Text = dataReader ["precio"].ToString ();
		dataReader.Close ();
	}
	protected void OnSaveActionActivated (object sender, EventArgs e)
	{
		IDbCommand dbCommand =
		App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = String.Format (
		"update categoria set nombre=@nombre where id={0}", id
		);

		dbCommand.AddParameter ("id", entry2.Text);
		dbCommand.AddParameter ("nombre", entry3.Text);
		dbCommand.AddParameter ("categoria", entry4.Text);
		dbCommand.AddParameter ("precio", entry5.Text);
		dbCommand.ExecuteNonQuery ();
		Destroy ();
		}
	}
}

