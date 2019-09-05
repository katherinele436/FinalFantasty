using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;


/* 
 * This class contains all methods
 * pertaining to on demand image processing
 * needed by Final Fantasty.
 *
 * For the time being it contains some 
 * stub methods so we can get to testing A LOT sooner
 *
 * Author: Rocky Petkov
 * Version: Stubby
 */
public class ImageProcessing{

	private static string TEMP_DIRECTORY = "Art" + Path.DirectorySeparatorChar 
							+ "PlaceHolder" + Path.DirectorySeparatorChar;	// A relative file path

	/*
	 * Combines various card arts into one hybrid image.
	 * saves image to disk and returns the location of the
	 * saved image.
	 */
	public static string hybridCardArt(List<Card> cardList) {
		return (TEMP_DIRECTORY + "Ramsay.png"); 			// Stub implementation
	}

	/*
	 * Creates a fully fleged card image in a similar manner
	 * to the Python Scripts. Saves image to disk and returns
	 * the location of the file on DISK
	 */
	 public static string createMealCard(DatabaseEntry cardInfo) {
	 	return (TEMP_DIRECTORY + "Timewizard.png"); 		 	// Stub implementation
	 }
}