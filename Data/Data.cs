using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventureLanguage.Data
{
    public class DataItems
    {
        public Collection<Message> messageList = new Collection<Message>();
        public Collection<Verb> verbList = new Collection<Verb>();
        public Collection<Adverb> adverbList = new Collection<Adverb>();
        public Collection<Variable> varList = new Collection<Variable>();                          //globals are retained inbetween Sections
        public Collection<Location> locationList = new Collection<Location>();
        public Collection<ObjectItem> objectList = new Collection<ObjectItem>();                   //objects are global as can be carried from Section to Section
        public Collection<Noun> nounList = new Collection<Noun>();
        public Collection<BBCBasicLine> SourceBBCBasicProgram = new Collection<BBCBasicLine>();
        public Collection<BBCBasicLine> UserBBCBasicProgram = new Collection<BBCBasicLine>();
        public Collection<BBCBasicLine> TargetBBCBasicProgram = new Collection<BBCBasicLine>();
        public Collection<EventLog> eventList = new Collection<EventLog>();
        public string folderLocation;
        public String sourceFile;
        public String outputFile;
        public String tokeniser;
        public String tokenisedFileName;
        public String gameTitle;
        public string SSDBuilder;
        public string SSDName;
        public string BeebEmPath;
        public int screenWidth;
        public int screenMode;
        public bool debugMode;
        public bool crunchTokenisedFile;
        public string folderDivider;
        public long roomIndexLength;
        public long roomDataLength;
    }
}
