package Monopoly;

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;

public class JavaFXTest extends Application {


    @Override
    public void start(Stage stage) throws Exception {

        BorderPane pane = new BorderPane();
        Scene sceneStart = new Scene(pane, 780, 177);

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
