
using System.Collections.Generic;

public static class DialogueParser
{
    public static List<DialogueCommand> GetCommands(string text)
    {
        List<DialogueCommand> commands = new List<DialogueCommand>();
        int i=0;
        while(i < text.Length)
        {
            if(text[i] == '{')
            {
                int end = text.IndexOf('}', i);
                if(end ==-1) break;
                commands.Add(ParseCommand(text.Substring(i+1, end-i-1)));
                i=end+1;
            }
            else
            {
                int j = text.IndexOf('{', i);
                if(j == -1) j = text.Length;
                commands.Add(new DialogueCommand {
                    commandType = DialogueCommandType.Text, 
                    stringInput = text.Substring(i, j-i)
                });
                i = j;
            }
        }
        return commands;
    }
    private static DialogueCommand ParseCommand(string command)
    {
        string[] parts = command.Split(' ');
        switch (parts[0])
        {
            case "color":   
                return new DialogueCommand {commandType = DialogueCommandType.Color, stringInput = parts[1]};
            case "wait": 
                return new DialogueCommand {commandType = DialogueCommandType.Wait, floatInput = float.Parse(parts[1])};
            case "speed": 
                return new DialogueCommand {commandType = DialogueCommandType.Speed, floatInput = float.Parse(parts[1])};
            case "shake": 
                bool end = parts.Length > 1 && parts[1] == "end";
                string intensity = "2";
                if(parts.Length > 2) intensity = parts[2]; 
                return new DialogueCommand {commandType = DialogueCommandType.Shake, stringInput = end ? "end" : "start", floatInput = float.Parse(intensity)};
            case "page":
                return new DialogueCommand {commandType = DialogueCommandType.NextPage};
            default: 
                return new DialogueCommand {commandType = DialogueCommandType.Text, stringInput = ""};
        }
    }

    public enum DialogueCommandType
    {
        Color, Text, Wait, Speed, Shake, NextPage
    }
    public struct DialogueCommand
    {
        public DialogueCommandType commandType;
        public float floatInput;
        public string stringInput;
    }
}