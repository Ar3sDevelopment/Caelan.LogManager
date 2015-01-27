namespace Caelan.LogManager

open System
open System.IO

type FileLogWriter(path : string) = 
    
    interface ILogWriter with
        member val Source = path with get, set
        member this.Log logType message = File.AppendAllText((this :> ILogWriter).Source, message + Environment.NewLine)
        member this.LogException logType message ex = 
            File.AppendAllText
                ((this :> ILogWriter).Source, "msg: " + message + ", ex: " + ex.Message + Environment.NewLine)
        member this.Assign element = 
            if element <> null then (this :> ILogWriter).Source <- element.Source
    
    new() = FileLogWriter(String.Empty)
    new(element) as this = 
        FileLogWriter()
        then (this :> ILogWriter).Assign element
