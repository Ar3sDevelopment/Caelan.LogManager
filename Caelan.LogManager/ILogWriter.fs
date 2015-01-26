namespace Caelan.LogManager

open System

type ILogWriter = 
    abstract Name : string with get
    abstract Log : LogType -> string -> unit
    abstract LogException : LogType -> string -> Exception -> unit