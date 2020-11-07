package ui;

import java.io.FileInputStream;
import java.io.FileNotFoundException;

import javafx.scene.image.Image;
import javafx.scene.image.ImageView;

public class ImageViewSimple extends ImageView{
	public ImageViewSimple(String file) throws FileNotFoundException
	{
		super(new Image(new FileInputStream(System.getProperty("user.dir")+file)));
	}
}
