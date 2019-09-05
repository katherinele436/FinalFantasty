"""This module creates card images for Final Fantasty in a modular fashion using a dictionary that has been
read from an excel file containing information about the cards. It uses the Pillow library to do some simple text
and image overlay!

Version 0.1

Author: Rocky Petkov"""

from PIL import ImageDraw, Image, ImageFont
import os
import copy

# Below are some global constants regarding different colours, fonts and file locations to be used with this module.
# Please do not tamper with these constants with out the express approval of the author.

CARD_ART_DIRECTORY = ".." + os.sep + ".." + os.sep + "Assets" + os.sep + "Resources" + os.sep + "Art"                # Path to Card Art!
CARD_DIRECTORY = CARD_ART_DIRECTORY os.sep + "Cards"                                                                 # Path to Final Card Images
STAT_IMAGE_DIRECTORY = CARD_ART_DIRECTORY + os.sep + "Stats"
STAT_IMAGES = ["Sweet.png", "Sour.png", "Bitter.png", "Spicy.png", "Salty.png", "Umami.png"]

MECHANICS_WRITEUPS = ".." + os.sep + ".." + os.sep + "Docs" + os.sep + "Docs" + "mechanics.txt"

CARD_FONT = ImageFont.truetype("Bitter-Regular.otf", size=18)
CARD_FONT_BOLD = ImageFont.truetype("Bitter-Bold.otf", size=50)
CARD_FONT_ITALIC = ImageFont.truetype("Bitter-Italic.otf", size=16)

BLACK = (0, 0, 0)

PICTURE_TOP_LEFT = (80, 77)                                                # Top left corner of the picture area
PICTURE_SIZE = (576, 577)                                                  # Size of picture area
NAME_LOCATION = (368, 675)                                                 # Boundaries of card name
NAME_BOUNDARIES = (515, 70)                                                # Size of card area alotted to name
TYPE_BOUNDARIES = ((0, 0), (0, 0), (0, 0), (0, 0))                         # Boundaries of card type descriptors
MECHANICS_TOP_LEFT = (87, 812)                                             # Boundaries of Mechanic Description
FLAVOUR_TEXT_LOCATION = (332, 930)                                         # Centre of flavour text area
PICTOGRAMME_CENTRE = (368, 738)                                             # Centre of Stats Row
PICTOGRAMME_SIZE = (41, 41)                                                 # Image + 5 pixel buffer

"Simple little function. Loops around and makes images for all of the cards!"
def create_pictures(card_list):
    for card in card_list:
        construct_card_image(card)

"""Creates a PNG image for the card described by the dictionary card_description and returns that image
to the calling environment"""
def construct_card_image(card_description):
    # Opening our image and overlay the card art
    card_template = Image.open(".." + os.sep + ".." + os.sep + "Sketches" + os.sep + "CardTemplateTEST.png", 'r')
    card_art = Image.open(CARD_ART_DIRECTORY + os.sep + card_description["Type"] + os.sep +
                          card_description["Art"], 'r')
    card_image = overlay_image(card_template, card_art, PICTURE_TOP_LEFT)    # Overlay the card art on to template

    # If our card is an ingredient, meal or condiment add the stats
    if card_description['Type'] != "Tool":
        card_image = overlay_pictogrammes(card_image, card_description["Stats"])

    # Now we overlay the title, mechanics and flavour text on the image
    image_draw = ImageDraw.Draw(card_image)
    resized_font, width, null = fit_text(NAME_BOUNDARIES, CARD_FONT_BOLD, card_description["Name"], image_draw)
    centered_top_left = (NAME_LOCATION[0] - (width/2), NAME_LOCATION[1])
    image_draw.multiline_text(xy=centered_top_left, text=card_description["Name"].upper(), font=resized_font,
                                fill=BLACK, align="left")
    # Empty strings will be filled in with brief descriptions read from a text file
    mechanics_message = card_description["Mechanics"][0] + " \n" + card_description["Mechanics"][1] + " "
    image_draw.multiline_text(xy=MECHANICS_TOP_LEFT, text=mechanics_message, font=CARD_FONT, fill=BLACK, spacing=5)

    # Centering and Drawing Flavour Test
    flavour_text = '"{0}"'.format(card_description["Description"])
    width, height = image_draw.textsize(flavour_text, CARD_FONT_ITALIC)           # Calculating size of the message
    centered_top_left = (FLAVOUR_TEXT_LOCATION[0] - (width/2), FLAVOUR_TEXT_LOCATION[1])
    image_draw.multiline_text(xy=centered_top_left, text=flavour_text, font=CARD_FONT_ITALIC, fill=BLACK)

    card_image.save(card_description["Picture Location"] + ".png")                     # Write to disk



"""The following function uses Pillow to overlay a foreground image overtop of a background image
The foreground image must be smaller than the background image Location in this case is an (X, Y) tuple
describing the location ofv where the centre of the new image shall be"""
def overlay_image(background_image, foreground_image, location):
    # Check to ensure the foreground image is of correct size. If it is not resize it.
    if (foreground_image.width != PICTURE_SIZE[0]) or (foreground_image.height != PICTURE_SIZE[1]):
        foreground_image = foreground_image.resize(PICTURE_SIZE)

    # We must do two quick conversions to ensure that the paste will work correctly and tere will be no artefacts
    # TODO put this into a conditional
    background_image = background_image.convert(mode="RGB")             # Gettting rid of transparency on template
    foreground_image = foreground_image.convert(mode="RGBA")            # Ensuring the image has an alpha channel

    background_image.paste(foreground_image, PICTURE_TOP_LEFT, foreground_image)
    return background_image

""""Overlays pictogrammes on a card corresponding to the 6-tuple passed in as stats.
it is assumed that all stats are in a 6 tuple of form """
def overlay_pictogrammes(card, stats):
    # If the length of stats is improper throw an error
    if len(stats) != 6:
        raise ValueError("You have supplied an improper list of stats.")

    # Getting rid of transparency on background
    if card.mode != "RGB":
        card = card.convert(mode="RGB")

    pictogramme_top_left = [PICTOGRAMME_CENTRE[0] - int((sum(stats) * PICTOGRAMME_SIZE[0]) // 2),
                            PICTOGRAMME_CENTRE[1]]
    # Loop through and overlay each of the images
    for stat in range(6):
        stat_pic = Image.open(STAT_IMAGE_DIRECTORY + os.sep + STAT_IMAGES[stat])
        if stat_pic.mode != "RGBA":
            stat_pic = stat_pic.convert(mode="RGBA")          # Ensuring we're in the right mode!
        for i in range(int(stats[stat])):
            card.paste(stat_pic, tuple(pictogramme_top_left))
            pictogramme_top_left[0] += PICTOGRAMME_SIZE[0] # Moving to where we draw the next pictogramme
    return card

"""Scales down a font until a message will fit in the desired area. The image_draw object must be supplied as well
Returns:
    font - a font object with an altered size. This is a modified deep copy of the original, so there will be no
    unintended sideeffects
    width, height - The width and height of the text block, used in aligning text"""
def fit_text(area, font, message, image):
    new_font = clone_font(font)                                    # Cloning the font so we can safely rescale
    width, height = image.textsize(message.upper(), new_font)      # Calculating size of the message
    while width > area[0] and height > area[1]:
        new_font.size -= 5                                         # 5 Seems like a good thing to decrement by
        width, height = image.textsize(message.upper(), new_font)  # Calculating size of font
    return new_font, width, height

"Returns a clone of the TrueType or OpenType font supplied"
def clone_font(font):
    return ImageFont.truetype(font.path, font.size)

