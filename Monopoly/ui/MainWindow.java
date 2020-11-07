package ui;

import java.io.FileNotFoundException;

import javafx.application.Application;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Pane;
import javafx.scene.layout.Priority;
import javafx.scene.layout.Region;
import javafx.stage.Stage;

public class MainWindow extends Application{
	private Stage stage; 
	
    public static void main(String[] args) {
        launch(args);
    }
    
    @Override
    public void start(Stage stage) throws Exception {
    	//get pane
    	Scene scene = new Scene(GetStartupPane(), 800, 600);
    	
        // Set the stage title
        stage.setTitle("Monopoly");
        this.stage = stage;
        
        ChangeScene(scene);
    }
    
    public Pane GetStartupPane() throws FileNotFoundException
    {
    	BorderPane pane = new BorderPane();
    	pane.setCenter(new ImageViewSimple("\\Assets\\MonopolyLogo.png"));
    	
    	Button TwoPlayers = new Button("2 Players");
    	Button ThreePlayers = new Button("3 Players");
    	Button FourPlayers = new Button("4 Players");
    	
    	HBox buttonPane = new HBox();
    	buttonPane.getChildren().addAll(TwoPlayers, ThreePlayers, FourPlayers);
    	pane.setBottom(buttonPane);
    	buttonPane.setAlignment(Pos.BOTTOM_CENTER);
    	
    	return pane;
    	
    }
    
    public void ChangeScene(Scene scene)
    {
        //Set Scene
        stage.setScene(scene);
        //display the stage
        stage.show();

        stage.getScene().getRoot().requestFocus();
    }
    
}
