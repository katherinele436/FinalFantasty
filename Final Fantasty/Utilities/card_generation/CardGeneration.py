"""Handles all aspects of turning cards from mere excel spreadsheet entries and turning them into JSON data for
Final Fantasty as well as creating png-representations of the cards

Author: Rocky Petkov"""

import os
from card_generation import excelToJson, excelToPNG

DB_DIRECTORY = ".." + os.sep + ".." + os.sep + "Assets" + os.sep + "Resources" + os.sep + "DB" + os.sep
EXCEL_DATABASE = "Ingredients.xlsx"
JSON_DATABASE = "CardDB.xml"



def main():
    # Ensureing that the excel spreadsheet we are trying to read exists
    if not os.path.isfile(EXCEL_DATABASE):
        print("You are trying to call this script on a file which does not exist", file=sys.stderr)
        exit(2)

    print("Updating Database")
    inFile, outFile = EXCEL_DATABASE, JSON_DATABASE
    cardList = excelToJson.readExcelFile(inFile)        # Reading in the list of cards
    excelToJson.write_XML(cardList, outFile)        # Writing it to JSON
    print("Databases are now upto date!")
    print("Updating Pictures")
    excelToPNG.create_pictures(cardList)                 # Updating all of the pictures
    print("Picture Database now updated")
    print("Exiting!")
    exit(0)

main()