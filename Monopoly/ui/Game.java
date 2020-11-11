package ui;

import java.io.FileNotFoundException;
import java.util.function.Consumer;

import Monopoly.Player;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
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

		
		GridPane board = new GridPane();
		img.setPreserveRatio(true);
		img.fitWidthProperty().bind(pane.widthProperty());
		img.fitHeightProperty().bind(pane.heightProperty());
		board.add(img, 0, 0);
		board.setPrefHeight(400);
		pane.setCenter(board);
		board.setAlignment(Pos.CENTER);

		//HBox topBar = new HBox();
		//topBar.setMinHeight(100);
		//topBar.setPrefHeight(100);
		//pane.setTop(topBar);
		
		HBox bottomBar = new HBox();
		bottomBar.setMinHeight(100);
		bottomBar.setPrefHeight(100);
		pane.setBottom(bottomBar);
		bottomBar.setAlignment(Pos.BOTTOM_CENTER);
		
		return pane;
	}
	
	public static Pane GetPane(int playerCount, Consumer<Pane> changePane) throws FileNotFoundException
	{
		return new Game(playerCount).GetPane(changePane);
	}
}
