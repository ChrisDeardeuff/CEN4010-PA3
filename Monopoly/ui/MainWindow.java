package ui;

import java.io.FileNotFoundException;
import java.util.function.Consumer;

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
    	
    	// ChangePane method
    	Consumer<Pane> method = p -> this.ChangePane(p);
    	
    	// Get and Set StartupPane
    	ChangePane(Startup.GetStartupPane(method));
    	
    	// Create scene
    	Scene scene = new Scene(main, 800, 600);
    	
        // Set the stage title
        stage.setTitle("Monopoly");
        this.stage = stage;
        
        ChangeScene(scene);
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
