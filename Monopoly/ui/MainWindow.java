package ui;

import java.io.FileNotFoundException;

import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Pane;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class MainWindow extends Application{
	private Stage stage;
	private Pane main;
	
    public static void main(String[] args) {
        launch(args);
    }
    
    @Override
    public void start(Stage stage) throws Exception {
    	// Create pane
    	VBox  main = new VBox();
    	main.setAlignment(Pos.CENTER);
    	this.main = main;
    	
    	// Get and Set StartupPane
    	ChangePane(GetStartupPane());
    	
    	// Create scene
    	Scene scene = new Scene(main, 800, 600);
    	
        // Set the stage title
        stage.setTitle("Monopoly");
        this.stage = stage;
        
        ChangeScene(scene);
    }
    
    private Pane GetStartupPane() throws FileNotFoundException
    {
    	BorderPane pane = new BorderPane();
    	pane.setCenter(new ImageViewSimple("\\Assets\\MonopolyLogo.png"));
    	
    	Button twoPlayers = new Button("2 Players");
    	twoPlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	ChangePane(Game.GetPane(2));
    	    }
    	});
    	
    	Button threePlayers = new Button("3 Players");
    	threePlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	ChangePane(Game.GetPane(3));
    	    }
    	});
    	
    	Button fourPlayers = new Button("4 Players");
    	fourPlayers.setOnAction(new EventHandler<ActionEvent>() {
    	    @Override public void handle(ActionEvent e) {
    	    	ChangePane(Game.GetPane(3));
    	    }
    	});
    	
    	HBox buttonPane = new HBox();
    	buttonPane.getChildren().addAll(twoPlayers, threePlayers, fourPlayers);
    	pane.setBottom(buttonPane);
    	buttonPane.setAlignment(Pos.BOTTOM_CENTER);
    	
    	return pane;
    	
    }
    
    private void ChangePane(Pane newPane)
    {
    	main.getChildren().clear();
    	main.getChildren().add(newPane);

    }
    
    private void ChangeScene(Scene scene)
    {
        //Set Scene
        stage.setScene(scene);
        //display the stage
        stage.show();

        stage.getScene().getRoot().requestFocus();
    }
    
}
