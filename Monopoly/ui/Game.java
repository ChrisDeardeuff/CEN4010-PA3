package ui;

import java.io.FileNotFoundException;
import java.util.function.Consumer;

import Monopoly.Player;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Pane;

public class Game {
	private Player[] players;
	
	private Game(int playerCount)
	{
		players = new Player[playerCount];
	}
	
	public Pane GetPane(Consumer<Pane> changePane) throws FileNotFoundException
	{
		GridPane pane = new GridPane();
		
		pane.add(new ImageViewSimple("\\Assets\\Board.jpg"), 0, 0);

		return pane;
	}
	
	public static Pane GetPane(int playerCount, Consumer<Pane> changePane) throws FileNotFoundException
	{
		return new Game(playerCount).GetPane(changePane);
	}
}
