from collections import OrderedDict
import xlrd
import sys
import os

"""This module reads an .xlsx file and converts each entry (row wise) into a JSON object
storing each object in a database.

ASSUMES that all data is seperated by columns

(CC) 2017 Rocky Raccoon"""

# TODO Ensure it works propely for each kind of card

# Reads an excel file into a propely formatted list of dictionaries
# TODO Make more general in any case
def readExcelFile(inFile):
	# Opening our excel file
	workBook = xlrd.open_workbook(inFile)
	workSheet = workBook.sheet_by_index(0)
	cardList = []						# A list of dictionaries. One entry for each card

	# Iterating through our spreadsheet
	for row in range(1, workSheet.nrows):
		card = OrderedDict()
		rowValues = workSheet.row_values(row)
		card["Type"] = rowValues[0]
		card["Name"] = rowValues[1]
		card["Description"] = rowValues[2]
		card["Stats"] = (rowValues[3], rowValues[4], rowValues[5], rowValues[6], rowValues[7], rowValues[8])	 # Sweet, Sour, Bitter, Spicy, Salty, Umami (Not final)
		card["Mechanics"] = (rowValues[9], rowValues[10])	# Stored as a two-tuple
		card["Ingredient Tag"] = rowValues[11]
		card["Tag"] = rowValues[12]							# Tag must be a unique value
		card["Art"] = rowValues[13]
		card["Picture Location"] = rowValues[14]
		cardList.append(card)

	return cardList

def write_XML(card_list, out_file):
	with open(out_file, 'w') as f:
		f.write(build_xml_string(card_list))		# Write our built string

# We're taking the naive approach and manually building an XML file (At this time, I do not believe any good libraries exist for this
def build_xml_string(card_list):
	XML_string = ""		# Initialise the XML string to be an empty string
	for card_info in card_list:
		XML_string += construct_XML_entry(card_info)
	return XML_string

# Builds the entry for a given card. 
# Currently there is no error checking as it should ALWAYS
# correct input.
def construct_XML_entry(card_info):
	XML_entry = "<Card>\n"							# Opening the space for a new card to be defined
	XML_tag_base = "\t<{0}>{1}</{0}>\n"				# Basic Structure of an XML tag
	for key, data in card_info.items():
		XML_entry += XML_tag_base.format(key, data)
	XML_entry += "</Card>\n"							# End of card info
	return XML_entry