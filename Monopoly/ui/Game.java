package ui;

import java.io.FileNotFoundException;
import java.util.function.Consumer;

import Monopoly.Player;
import javafx.event.EventHandler;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.Pane;
import javafx.stage.WindowEvent;

public class Game {
	private Player[] players;
	
	private Game(int playerCount)
	{
		players = new Player[playerCount];
	}
	
	public Pane GetPane(Consumer<Pane> changePane) throws FileNotFoundException
	{
		BorderPane pane = new BorderPane();
		ImageViewSimple img = new ImageViewSimple("\\Assets\\Board.jpg");

		img.setPreserveRatio(true);
		img.fitWidthProperty().bind(pane.widthProperty());
		img.fitHeightProperty().bind(pane.heightProperty());
		pane.setCenter(img);


		return pane;
	}
	
	public static Pane GetPane(int playerCount, Consumer<Pane> changePane) throws FileNotFoundException
	{
		return new Game(playerCount).GetPane(changePane);
	}
}
