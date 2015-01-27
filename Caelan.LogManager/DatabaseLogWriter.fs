namespace Caelan.LogManager
open System

type DatabaseLogWriter(connectionString) = 
    interface ILogWriter with
        member val Source = connectionString with get, set
        member this.Log logType message = (logType, message, None) |||> this.Write
        member this.LogException logType message ex = (logType, message, Some(ex)) |||> this.Write
        member this.Assign element = 
            if element <> null then 
                if not (String.IsNullOrWhiteSpace(element.Source)) then (this :> ILogWriter).Source <- element.Source
    
    member private this.Write logType message ex =
        let content = LogObject()
        content.LogType <- logType
        content.Exception <- ex
        content.Message <- message
        //TODO: Inserire codice per scrivere su db
    
    new() = DatabaseLogWriter(String.Empty)