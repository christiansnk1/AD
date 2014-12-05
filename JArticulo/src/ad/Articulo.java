package ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;

public class Articulo {
	 static int opcion=0;
	 static Scanner teclado = new Scanner(System.in);
	
	public static void main(String [] args) throws SQLException {
		
		
		Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/bdproductos?user=root&password=sistemas"
		);
		Statement statement = connection.createStatement();
		ResultSet resultSet = statement.executeQuery("select * from categoria");
		
		System.out.println("Seleccione una opcción--> " +
				"0-salir / 1-nuevo / 2-editar / 3-eliminar / 4-ver . ");
		
		switch(opcion){
		
		case 1:
			System.out.println("Introduzca un nuevo articulo");
			teclado.nextLine();
			

		while (resultSet.next()) 
			System.out.printf("id=%4s nombre=%s\n", 
				resultSet.getObject("id"), resultSet.getObject("nombre")); 
		
		
		resultSet.close();
		statement.close();
		connection.close();
		
		}	
	}
}
