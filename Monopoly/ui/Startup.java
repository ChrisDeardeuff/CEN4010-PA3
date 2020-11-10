package ui;

import java.io.FileNotFoundException;
import java.util.function.Consumer;

import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Pane;

public class Startup {
    public static Pane GetStartupPane(Consumer<Pane> changePane) throws FileNotFoundException
    {
    	BorderPane pane = new BorderPane();
    	pane.setCenter(new ImageViewSimple("\\Assets\\MonopolyLogo.png"));
    	
    	Button twoPlayers = new Button("2 Players");
    	twoPlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	try {
					changePane.accept(Game.GetPane(2, changePane));
				} catch (FileNotFoundException e1) {

				}
    	    }
    	});
    	
    	Button threePlayers = new Button("3 Players");
    	threePlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	try {
					changePane.accept(Game.GetPane(3, changePane));
				} catch (FileNotFoundException e1) {

				}
    	    }
    	});
    	
    	Button fourPlayers = new Button("4 Players");
    	fourPlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	try {
					changePane.accept(Game.GetPane(3, changePane));
				} catch (FileNotFoundException e1) {

				}
    	    }
    	});
    	
    	HBox buttonPane = new HBox();
    	buttonPane.getChildren().addAll(twoPlayers, threePlayers, fourPlayers);
    	pane.setBottom(buttonPane);
    	buttonPane.setAlignment(Pos.BOTTOM_CENTER);
    	
    	return pane;
    	
    }
}
