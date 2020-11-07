package Monopoly;

import java.io.File;
import java.io.FileInputStream;

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;
import ui.ImageViewSimple;

public class JavaFXTest extends Application {


    @Override
    public void start(Stage stage) throws Exception {

    	BorderPane pane = new BorderPane();
        
        Button bTest = new Button("Test");
        ImageView logo = new ImageView(new File("\\Assets\\MonopolyLogo.jpg").toURI().toString());
        //System.out.println(new File(System.getProperty("user.dir")+"\\Assets\\MonopolyLogo.jpg").exists());
        logo.setPreserveRatio(true);
        
        HBox hbox = new HBox(logo);
        
        pane.setCenter(new ImageViewSimple("\\Assets\\MonopolyLogo.png"));
        pane.setBottom(bTest);
        logo.autosize();
        
        Scene sceneStart = new Scene(pane, 800, 600);
        
        // Set the stage title
        stage.setTitle("Monopoly");
        //Set Scene
        stage.setScene(sceneStart);
        //display the stage
        stage.show();

        stage.getScene().getRoot().requestFocus();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
