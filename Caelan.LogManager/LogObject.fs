namespace Caelan.LogManager

open System

type LogObject() = 
    member val Date = DateTime.Now with get, set
    member val LogType = LogType.Error with get, set
    member val Message = String.Empty with get, set
    member val Exception : Exception option = None with get, set