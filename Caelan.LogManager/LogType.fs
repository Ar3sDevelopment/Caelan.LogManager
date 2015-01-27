namespace Caelan.LogManager

type LogType = 
    | Debug
    | Trace
    | Warning
    | Error
    | Fatal
    override t.ToString() = 
        match t with
        | Debug -> "DEBUG"
        | Trace -> "TRACE"
        | Warning -> "WARNING"
        | Error -> "ERROR"
        | Fatal -> "FATAL"
