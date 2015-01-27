namespace Caelan.LogManager

open System
open System.IO

type FileLogWriter(path) = 
    interface ILogWriter with
        member __.Name with get() = "FileLogWriter"

        member __.Log logType message =
            File.AppendAllText(path, message + Environment.NewLine)
        
        member __.LogException logType message ex =
            File.AppendAllText(path, "msg: " + message + ", ex: " + ex.Message + Environment.NewLine)

    member val Path = path with get, set