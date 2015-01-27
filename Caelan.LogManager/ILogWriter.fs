namespace Caelan.LogManager

open System

type ILogWriter = 
    abstract Log : LogType -> string -> unit
    abstract LogException : LogType -> string -> Exception -> unit
    abstract Source : string with get, set
    abstract Assign : WriterElement -> unit
