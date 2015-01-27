namespace Caelan.LogManager

open System
open System.IO

type FileLogWriter(path : string) = 
    
    interface ILogWriter with
        member val Source = path with get, set
        member this.Log logType message = (logType, message, None) |||> this.Write
        member this.LogException logType message ex = (logType, message, Some(ex)) |||> this.Write
        member this.Assign element = 
            if element <> null then 
                if not (String.IsNullOrWhiteSpace(element.Source)) then (this :> ILogWriter).Source <- element.Source
                if not (String.IsNullOrWhiteSpace(element.Format)) then this.Format <- element.Format
    
    member this.Write (logType : LogType) (message : string) (ex : Exception option) = 
        let exMessage = 
            match ex with
            | None -> String.Empty
            | Some(e) when e = null -> String.Empty
            | Some(e) -> e.Message
        
        let content = 
            this.Format.ToString().Replace("%date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                .Replace("%type", logType.ToString()).Replace("%message", message).Replace("%exception", exMessage)
                .Replace("%nl", Environment.NewLine)
        File.AppendAllText((this :> ILogWriter).Source, content)
    
    member val Format = "[%date] %type - %message %exception%nl" with get, set
    new() = FileLogWriter(String.Empty)
    new(element) as this = 
        FileLogWriter()
        then (this :> ILogWriter).Assign element
