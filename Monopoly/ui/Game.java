package ui;

import java.io.FileNotFoundException;

import Monopoly.Player;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.Pane;

public class Game {
	private Player[] players;
	
	private Game(int playerCount)
	{
		players = new Player[playerCount];
	}
	
	public Pane GetPane()
	{
		BorderPane pane = new BorderPane();
		try {
			pane.setCenter(new ImageViewSimple("\\Assets\\MonopolyLogo.png"));
			return pane;
		} catch (FileNotFoundException e) {
			return null;
		}
	}
	
	public static Pane GetPane(int playerCount)
	{
		return new Game(playerCount).GetPane();
	}
}
