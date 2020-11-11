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
    private Pane currentScene;

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage stage) throws Exception {
        // size change listener
        stage.heightProperty().addListener(e ->{
            SizeChanged();
        });

        stage.widthProperty().addListener(e ->{
            SizeChanged();
        });

        // Create pane
        VBox  main = new VBox();
        main.setAlignment(Pos.CENTER);
        this.main = main;
        main.autosize();

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
        SizeChanged();
    }

    private void ChangePane(Pane newPane)
    {
        main.getChildren().clear();
        main.getChildren().add(newPane);
        currentScene = newPane;
        newPane.setPrefSize(main.getHeight(), main.getWidth());
        newPane.setMinSize(100, 100);
        newPane.autosize();

    }

    private void ChangeScene(Scene scene)
    {
        //Set Scene
        stage.setScene(scene);
        //display the stage
        stage.show();

        stage.getScene().getRoot().requestFocus();
    }

    private void SizeChanged()
    {
        main.setPrefSize(stage.getHeight(), stage.getWidth());
        currentScene.setPrefSize(stage.getHeight(), stage.getWidth());
        currentScene.autosize();
    }
}
