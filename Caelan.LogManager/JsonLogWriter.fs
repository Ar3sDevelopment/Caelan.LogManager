namespace Caelan.LogManager

open System
open System.IO
open Newtonsoft.Json

type JsonLogObject() = 
    member val Date = DateTime.Now with get, set
    member val LogType = LogType.Error with get, set
    member val Message = String.Empty with get, set
    member val Exception : Exception option = None with get, set

type JsonLogWriter(path) = 
    
    interface ILogWriter with
        member val Source = path with get, set
        member this.Log logType message = (logType, message, None) |||> this.Write
        member this.LogException logType message ex = (logType, message, Some(ex)) |||> this.Write
        member this.Assign element = 
            if element <> null then 
                if not (String.IsNullOrWhiteSpace(element.Source)) then (this :> ILogWriter).Source <- element.Source
    
    member this.Write logType message ex = 
        let content = JsonLogObject()
        content.LogType <- logType
        content.Exception <- ex
        content.Message <- message
        File.AppendAllText
            ((this :> ILogWriter).Source, 
             JsonConvert.SerializeObject(content, Formatting.Indented) + Environment.NewLine)
    
    new() = JsonLogWriter(String.Empty)
