# Adventure Language

I had previously developed a framework for creating BBC Micro text adventure games using Excel to hold the game data and build the output files which were then manually added to an SSD disc image. The details can be found here - https://stardot.org.uk/forums/viewtopic.php?f=65&t=14770#p197797

The data files were consumed by the BBC Basic program which was in effect the game engine - this program also included the appropriate game specific logic.

This approach had a number of issues:

1 - Excel was very clunky to use for this and not everybody has it

2 - The files it created (e.g. message files, object and location files) had to be manually added to the SSD image each time they were changed

3 - The game engine program also directly included the game specific code so if any bugs were fixed in the game engine or improvements made a new game engine program had to be created (effectively by deleting all game logic)

All of which is a pain. 

The new approach is to do away with Excel and hold all the game objects and logic in an XML file. The game logic is combined into a BBC Basic source file (held in plain text) at the appropriate points and then output a combination of game engine + logic and data files.

The BBC Basic source file can be amended and managed under souce control and any fixes applied can be retained for future versions as it is not amended during the game development process (and even if it is the source and logic are separate).

Each new game to be created is created as a project in a folder structure which is created by calling the "-new" command line with a path and project name (preferably kept to 7 letters - the reasoning behind this should be obvious to DFS users). 

Source.txt and AdventureData.xml are amended and then the "-b" (build) command line is used to 'compile' the project into the appropriate files.

Thanks to the presence of various programs for Windows and Mac (and the fact that the main project is written in .Net Core 3.1) it is possible to establish a full tool chain for development. So long as eveything is configured correctly once compiled the files are tokenised (Windows only), placed into an SSD file and BeebEm is started up with the SSD loaded and ready to go. On the Mac unfortunately the BASIC is not tokenised so the user has to press Command + W (to unlock drive 0) and then press 'F0' (which is mapped to F10 on my Mac) to call an *EXEC process to tokenise the program within the Beeb itself. Once tokenised the program is saved and run.

A build .bat or .command file are generated by the project creation process in order to simplify the build / compile process.


I have a number of immediate plans for the game enginer and compiler (in no specifc order):

1) Hold all location data in RAM rather than load from file each time a room is entered.
This will take up more memory but speed things up a lot.

2) Have flags to show whether an entry in the location link table is enabled (i.e. door is locked / unlocked) rather than handle this in custom code using a variable.
This will also mean saving the state of these flags in the save game.
Doing this will enable wandering monsters to be included (i.e. they would be able to only go through doors that were unlocked) more easily.

3) Have flags in the location data to determine whether a location is dark or has some other environmental factor of note (underwater, hot or cold etc.)
It is dark, you cannot see. Ah those wonderful words...
You are in space, you cannot breath. You are in the desert, it is hot and you are getting thirsty.
All wonderfully irritating uses of such flags.

4) Include wandering monsters / NPCs.
They will have a basic hostility rating towards the player (1 = worst enemy, attack on sight while 100 = best buddies). The initial value will vary by NPC.
The monsters will of course be able to carry stuff and attack.
Unlike some other games if they run away it will tell you which direction they went in. :)

5) Make the games multi-part.
Loading more stuff into RAM will mean less space for game logic. By making the game split over several separate programs large games can still be created which run quickly (once loaded).
This will need to include things like global variables (that are retained between sections), global objects and probably a single menu program that controls which section is called when.
The save games will get a lot more complex...
