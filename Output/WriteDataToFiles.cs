using System;
using System.Collections.Generic;
using System.Text;
using AdventureLanguage.Data;
using System.IO;
using AdventureLanguage.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AdventureLanguage.Output
{
    public static class WriteDataToFiles
    {
        //powershell for hexdump
        //Format-Hex data-r



        public static bool WriteWordsToFile(DataItems gameData)
        {
            //write out:
            // item number (single byte)
            // 0 byte (start of record marker)
            // all word bytes in reverse order
            string fd = gameData.folderDivider;
            string fileOutput = gameData.folderLocation + fd + "fileOutput";

            try
            {

                BinaryWriter nounWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-N", FileMode.Create));

                try
                {
                    //string itemText = "";
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Nouns"));
                    for (int i = 0; i < gameData.nounList.Count; i++)
                    {

                        gameData.eventList.Add(new EventLog(" " + gameData.nounList[i].IDNumber().ToString() + " " + gameData.nounList[i].NounText()));
                        nounWriter.Write((byte)gameData.nounList[i].IDNumber());    //write a zero before the string
                        nounWriter.Write((byte)0);
                        //itemText = DataHelpers.ReverseString(gameData.nounList[i].NounText());
                        nounWriter.Write((byte)gameData.nounList[i].NounText().Length);
                        nounWriter.Write(DataHelpers.ReverseString(gameData.nounList[i].NounText()));
                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Nouns"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    nounWriter.Dispose();
                    return false;
                }
                nounWriter.Dispose();

            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Nouns"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }

            try
            {

                BinaryWriter verbWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-V", FileMode.Create));

                try
                {
                    //string itemText = "";
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Verbs"));
                    for (int i = 0; i < gameData.verbList.Count; i++)
                    {
                        gameData.eventList.Add(new EventLog(" " + gameData.verbList[i].IDNumber().ToString() + " " + gameData.verbList[i].VerbText()));
                        verbWriter.Write((byte)gameData.verbList[i].IDNumber());    //write a zero before the string
                        verbWriter.Write((byte)0);
                        verbWriter.Write((byte)gameData.verbList[i].VerbText().Length);
                        verbWriter.Write(DataHelpers.ReverseString(gameData.verbList[i].VerbText()));
                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Verbs"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    verbWriter.Dispose();
                    return false;
                }
                verbWriter.Dispose();
            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Verbs"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }

            try
            {

                BinaryWriter dataWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-A", FileMode.Create));

                try
                {
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Adverbs"));
                    for (int i = 0; i < gameData.adverbList.Count; i++)
                    {
                        gameData.eventList.Add(new EventLog(" " + gameData.adverbList[i].IDNumber().ToString() + " " + gameData.adverbList[i].AdverbText()));
                        dataWriter.Write((byte)gameData.adverbList[i].IDNumber());    //write a zero before the string
                        dataWriter.Write((byte)0);
                        dataWriter.Write((byte)gameData.adverbList[i].AdverbText().Length);
                        dataWriter.Write(DataHelpers.ReverseString(gameData.adverbList[i].AdverbText()));
                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Adverbs"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    dataWriter.Dispose();
                    return false;
                }
                dataWriter.Dispose();

            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Adverbs"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }

            return true;

        }

        public static bool WriteVariablesToOutput(DataItems gameData)
        {

            gameData.eventList.Add(new EventLog());
            gameData.eventList.Add(new EventLog("Writing Variables"));

            for (int i = 0; i < gameData.varList.Count; i++)

            {
                gameData.eventList.Add(new EventLog(" " + gameData.varList[i].IDNumber() + " " + gameData.varList[i].IDString() + " - " + gameData.varList[i].VariableText()));

            }

            return true;
        }

        public static bool WriteObjectsToFile(DataItems gameData)
        {

            //write out:
            //object number
            //message number
            //location number
            //weight
            //value

            try
            {
                string fd = gameData.folderDivider;
                string fileOutput = gameData.folderLocation + fd + "fileOutput";

                BinaryWriter dataWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-O", FileMode.Create));

                try
                {
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Objects"));
                    for (int i = 0; i < gameData.objectList.Count; i++)

                    {
                        int number = 0;

                        gameData.eventList.Add(new EventLog(" " + gameData.objectList[i].IDString() + " " + gameData.objectList[i].IDNumber() + " - " + gameData.messageList[gameData.objectList[i].MessageNumber() - 1].MessageText()));

                        //id number
                        dataWriter.Write((byte)gameData.objectList[i].IDNumber());

                        //message number
                        number = gameData.objectList[i].MessageNumber();
                        dataWriter.Write((byte)(number / 256));
                        dataWriter.Write((byte)(number - ((number / 256) * 256)));

                        //location number
                        number = gameData.objectList[i].LocationNumber();
                        dataWriter.Write((byte)(number / 256));
                        dataWriter.Write((byte)(number - ((number / 256) * 256)));

                        //weight
                        dataWriter.Write((byte)gameData.objectList[i].Weight());

                        //value
                        number = gameData.objectList[i].Value();
                        dataWriter.Write((byte)(number / 256));
                        dataWriter.Write((byte)(number - ((number / 256) * 256)));
                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Objects"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    dataWriter.Dispose();
                    return false;
                }
                dataWriter.Dispose();

            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Objects"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }


            return true;   //change to true
        }

        public static bool WriteLocationsToFile(DataItems gameData)
        {

            //location file contents
            //messages
            //  number of message records (single byte)
            //  MSB of message number
            //  LSB of message number
            //location links
            //  number of links (single byte)
            //  repeat:
            //      verb number
            //      MSB of location link
            //      LSB of location link

            //index file contains:
            //64 start of record marker (int)
            //0
            //0
            //second byte of number (256s)
            //first byte of number (1s)
            //this equates to the position (BBC Basic INT) of the location in the file

            try
            {
                string fd = gameData.folderDivider;
                string fileOutput = gameData.folderLocation + fd + "fileOutput";

                BinaryWriter locationWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-R", FileMode.Create));
                BinaryWriter indexWriter = new BinaryWriter(File.Open(fileOutput + fd + "IDX-R", FileMode.Create));

                try
                {
                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Locations"));
                    for (int i = 1; i < gameData.locationList.Count; i++)
                    {

                        gameData.eventList.Add(new EventLog("Location : " + gameData.locationList[i].IDString()));


                        //write out message data
                        long iLen = locationWriter.BaseStream.Length;
                        int LocationTo;

                        //write out message information     
                        int numberOfItems = gameData.locationList[i].Messages().Count;

                        if (numberOfItems == 0)
                        {
                            gameData.eventList.Add(new EventLog("Processing Locations"));
                            gameData.eventList.Add(new EventLog("Each location must have at least one message string."));
                            return false;
                        }

                        locationWriter.Write((byte)numberOfItems);   //number of message records

                        for (int NumMessages = 1; NumMessages <= numberOfItems; NumMessages++)
                        {
                            int messageNumber = gameData.locationList[i].Message(NumMessages);
                            locationWriter.Write((byte)(messageNumber / 256));
                            locationWriter.Write((byte)(messageNumber - ((messageNumber / 256) * 256)));
                            gameData.eventList.Add(new EventLog(" " + gameData.messageList[gameData.locationList[i].Message(NumMessages) - 1].MessageText()));
                        }

                        //write out location links
                        numberOfItems = gameData.locationList[i].Exits().Count;
                        locationWriter.Write((byte)numberOfItems);   //number of location link records

                        if (numberOfItems > 0)
                        {
                            gameData.eventList.Add(new EventLog());
                            for (int NumLocations = 0; NumLocations < numberOfItems; NumLocations++)
                            {
                                ExitTo item = gameData.locationList[i].exitList[NumLocations];

                                locationWriter.Write((byte)(item.Verb()));
                                LocationTo = item.LocationID();
                                locationWriter.Write((byte)(LocationTo / 256));
                                locationWriter.Write((byte)(LocationTo - ((LocationTo / 256) * 256)));
                                gameData.eventList.Add(new EventLog("  Exit " + DataHelpers.VerbText(gameData.verbList, item.Verb()) + " to " + item.LocationID()));
                            }
                        }

                        //write out index data
                        indexWriter.Write((byte)64);
                        indexWriter.Write((byte)0);
                        indexWriter.Write((byte)0);
                        indexWriter.Write((byte)(iLen / 256));
                        indexWriter.Write((byte)(iLen - ((iLen / 256) * 256)));
                        gameData.eventList.Add(new EventLog());
                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Locations"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    locationWriter.Dispose();
                    indexWriter.Dispose();
                    return false;
                }

                locationWriter.Dispose();
                indexWriter.Dispose();

            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Locations"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }

            return true;   //change to true
        }

        public static bool WriteMessagesToFile(DataItems gameData)
        {

            //message file contents:
            //0 start of record marker byte (string)
            //length of string
            //bytesof string in reverse order

            //index file contains:
            //64 start of record marker (int)
            //0
            //0
            //second byte of number (256s)
            //first byte of number (1s)
            //this equates to the position (BBC Basic INT) of the message in the file

            try
            {
                string fd = gameData.folderDivider;
                string fileOutput = gameData.folderLocation + fd + "fileOutput";

                BinaryWriter messageWriter = new BinaryWriter(File.Open(fileOutput + fd + "DATA-M", FileMode.Create));
                BinaryWriter indexWriter = new BinaryWriter(File.Open(fileOutput + fd + "IDX-M", FileMode.Create));

                try
                {
                    // string messageText = "";
                    long byte1;
                    long byte2;

                    gameData.eventList.Add(new EventLog());
                    gameData.eventList.Add(new EventLog("Writing Messages"));
                    for (int i = 0; i < gameData.messageList.Count; i++)

                    {
                        gameData.eventList.Add(new EventLog(" " + gameData.messageList[i].IDNumber().ToString() + " " + gameData.messageList[i].MessageText()));
                        //write out message data
                        long iLen = messageWriter.BaseStream.Length;
                        // messageText = DataHelpers.ReverseString(gameData.messageList[i].MessageText());

                        messageWriter.Write((byte)0);   //start of record marker
                        messageWriter.Write((byte)gameData.messageList[i].MessageText().Length);   //length of string

                        //char[] arr = messageText.ToCharArray();

                        messageWriter.Write(DataHelpers.ReverseString(gameData.messageList[i].MessageText()));

                        byte2 = iLen / 256;
                        byte1 = iLen - (byte2 * 256);

                        //write out index data
                        indexWriter.Write((byte)64);
                        indexWriter.Write((byte)0);
                        indexWriter.Write((byte)0);
                        indexWriter.Write((byte)(byte2));
                        indexWriter.Write((byte)(byte1));

                    }
                }
                catch (Exception c)
                {
                    gameData.eventList.Add(new EventLog("Processing Messages"));
                    gameData.eventList.Add(new EventLog(c.Message));
                    messageWriter.Dispose();
                    indexWriter.Dispose();
                    return false;
                }

                messageWriter.Dispose();
                indexWriter.Dispose();

            }
            catch (Exception c)
            {
                gameData.eventList.Add(new EventLog("Processing Messages"));
                gameData.eventList.Add(new EventLog(c.Message));
                return false;
            }

            return true;
        }

    }

}